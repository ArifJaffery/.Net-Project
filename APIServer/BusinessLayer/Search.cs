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


        public List<People> getPeoplesByNameAndGenderFilter(string name,string gender)
        {
            return data.people.FindAll(p => p.name.ToLower() == name.ToLower() && p.gender.ToLower()==gender.ToLower());
        }


        public void SimpleSearch(BusinessModels.Search searchparam)
        {
            List<People> peoples;
            if (searchparam.male)
                peoples=getPeoplesByNameAndGenderFilter(searchparam.name, "M");
            else if (searchparam.female)
                peoples=getPeoplesByNameAndGenderFilter(searchparam.name, "M");
            else
                peoples=data.people;



        }

    }
}
