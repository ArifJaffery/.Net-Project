using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using BusinessModels;
using BusinessLayer;

namespace APIServer.Controllers
{
    public class SearchController : ApiController
    {
        private BusinessLayer.Search search = new BusinessLayer.Search();
        private int rowscount = 10;

        public IEnumerable<string> Get()
        {
            return new string[] { "Hello from AppsCore"};
        }


        public IEnumerable<BusinessModels.Results> Post([FromBody] BusinessModels.Search searchparam)
        {
            if (searchparam.type == "Simple")
                return this.search.SimpleSearch(searchparam).Take(this.rowscount);
            else if (searchparam.type == "Advance")
                return this.search.AdvanceSearch(searchparam).Take(this.rowscount);
            else
                return new List<BusinessModels.Results>();
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }

    }
}   
