using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboriginalHeroes.Data.DataModels.Awm
{
    public class Resources
    {
        public int enabled { get; set; }
        public string name { get; set; }
        public int maxrecords { get; set; }
        public int unlimitedrecords { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
    }

    public class Result
    {
        public string record_type { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string base_rank { get; set; }
        public List<string> bio_timeline { get; set; }
        public string birth_date { get; set; }
        public string birth_place { get; set; }
        public List<string> conflict { get; set; }
        public List<string> related_conflicts { get; set; }
        public List<string> related_conflict_sort { get; set; }
        public List<string> conflict_search { get; set; }
        public List<string> conflict_category { get; set; }
        public string gender { get; set; }
        public List<string> related_subjects { get; set; }
        public List<string> name_variation { get; set; }
        public string preferred_name { get; set; }
        public List<string> service_conflict { get; set; }
        public List<string> service_number { get; set; }
        public List<string> webgroups { get; set; }
        public string web_profile { get; set; }
        public List<string> catalogue_id { get; set; }
        public List<string> related_objects { get; set; }
        public List<string> related_units_id { get; set; }
        public List<string> related_units { get; set; }
        public List<string> related_units_label { get; set; }
        public List<string> related_unit_all_ids { get; set; }
        public List<string> unit { get; set; }
        public List<string> rank { get; set; }
        public List<string> related_people_id { get; set; }
        public List<string> related_people { get; set; }
        public List<string> related_people_label { get; set; }
        public string death_date { get; set; }
        public List<string> corps { get; set; }
        public string death_place { get; set; }
    }

    public class RootObject
    {
        public double version { get; set; }
        public int version_major { get; set; }
        public int version_minor { get; set; }
        public int build { get; set; }
        public int status { get; set; }
        public string error { get; set; }
        public string accesskey { get; set; }
        public int start { get; set; }
        public int count { get; set; }
        public string clientid { get; set; }
        public Resources resources { get; set; }
        public string format { get; set; }
        public string type { get; set; }
        public string query { get; set; }
        public bool showLabels { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public int found { get; set; }
        public List<Result> results { get; set; }
        public List<object> labels { get; set; }
    }

}

