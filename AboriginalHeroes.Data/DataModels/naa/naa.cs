using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboriginalHeroes.Data.DataModels.Naa
{
    public class ResultSet
    {
        public int person_id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string family_name { get; set; }
        public List<object> alias_names { get; set; }
        public List<string> service_numbers { get; set; }
        public List<object> place_of_birth { get; set; }
        public List<object> place_of_enlistment { get; set; }
    }

    public class RootObject
    {
        public List<ResultSet> ResultSet { get; set; }
        public int result_count { get; set; }
    }
}
