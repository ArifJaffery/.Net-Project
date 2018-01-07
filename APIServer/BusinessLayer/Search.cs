using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.IO;
using Newtonsoft.Json;



namespace BusinessLayer
{
    public class Search
    {
        private DataAccessLayer.Mockdata data = JsonConvert.DeserializeObject<DataAccessLayer.Mockdata>(File.ReadAllText("data_small.json"));
        /*   List<BusinessModels.Gender> genders =   new List<BusinessModels.Gender>() {
                                                           new BusinessModels.Gender() {
                                                               id = "M", title = "Male"
                                                           },
                                                           new BusinessModels.Gender() {
                                                               id = "F", title = "Female"
                                                           }
                                                   };

          */

        private IEnumerable<People> getDescendents(People person,bool isRoot)
        {
            List<DataAccessLayer.People> childrens = new List<People>();
            if (person.gender.ToLower() == "m")
                childrens = data.people.FindAll(p => p.father_id == person.id);
            else if (person.gender.ToLower() == "f")
                childrens = data.people.FindAll(p => p.mother_id == person.id);
            else
                return childrens;

            if (isRoot && childrens.Count == 0)
            {
                return childrens;
            }
            else
            {
                List<DataAccessLayer.People> grandchildrens = new List<DataAccessLayer.People>();
                childrens.ForEach(p => grandchildrens.AddRange(this.getDescendents(p, false)));
                childrens.AddRange(grandchildrens);
                return childrens;
            }


        }



        private IEnumerable<People> getAncestors(People person)
        {
            List<People> ancestors = new List<People>();
            if (person.father_id == null && person.mother_id == null)
                return ancestors;

            People father = data.people.Single(p=>p.id==person.father_id);
            People mother = data.people.Single(p => p.id == person.mother_id);

            if (father != null)
            {
                ancestors.Add(father);
                ancestors.AddRange(this.getAncestors(father));
            }

            if (mother != null)
            {
                ancestors.Add(mother);
                ancestors.AddRange(this.getAncestors(mother));
            }

            return ancestors;
        }



        public IEnumerable<BusinessModels.Results> AdvanceSearch(BusinessModels.Search searchparam)
        {
            List<DataAccessLayer.People> peoples = this.SearchByNameAndGender(searchparam);
            //            List<DataAccessLayer.People> ancestors = new List<DataAccessLayer.People>();
            //            peoples.ForEach(person => ancestors.AddRange(this.getAncestors(person)));            
            List<DataAccessLayer.People> descendents = new List<DataAccessLayer.People>();
            peoples.ForEach(person => descendents.AddRange(this.getDescendents(person,true)));
            IEnumerable<BusinessModels.Results> results = this.mapFromPeopleToResult(descendents);
            return results;
        }



        private List<People> getPeoplesByNameAndGenderFilter(string name,string gender)
        {
            return data.people.FindAll(p => p.name.ToLower() == name.ToLower() && p.gender.ToLower()==gender.ToLower());
        }

        public IEnumerable<BusinessModels.Results> SimpleSearch(BusinessModels.Search searchparam)
        {
            IEnumerable<DataAccessLayer.People> peoples = this.SearchByNameAndGender(searchparam);
            IEnumerable<BusinessModels.Results> results= this.mapFromPeopleToResult(peoples);
            return results;

        }

        private  List<DataAccessLayer.People> SearchByNameAndGender(BusinessModels.Search searchparam)
        {
            List<DataAccessLayer.People> peoples;

            if (searchparam.male)
                peoples=getPeoplesByNameAndGenderFilter(searchparam.name, "M");
            else if (searchparam.female)
                peoples=getPeoplesByNameAndGenderFilter(searchparam.name, "F");
            else
                peoples=data.people.FindAll(p => p.name.ToLower() == searchparam.name.ToLower());

            return peoples;

        }

        IEnumerable<BusinessModels.Results> mapFromPeopleToResult(IEnumerable<DataAccessLayer.People> peoples)
        {
            return peoples.Select(result => new BusinessModels.Results()
            {
                id = result.id,
                name = result.name,
                gender = result.gender,
                birthplace = this.data.places.Single(place => place.id == result.place_id).name
            });


        }





    }
}
