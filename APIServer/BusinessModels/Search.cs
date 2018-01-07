using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class Search
    {
        public string type { get; set; }
        public string name { get; set; }
        public bool male { get; set; }
        public bool female { get; set; }
        public string direction { get; set; }
        public List<Results> results { get; set; }

    }
}
