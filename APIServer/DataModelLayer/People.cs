using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class People
    {
        public long id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public long? father_id { get; set; }
        public long? mother_id { get; set; }
        public long place_id { get; set; }
        public int level { get; set; }

    }
}
