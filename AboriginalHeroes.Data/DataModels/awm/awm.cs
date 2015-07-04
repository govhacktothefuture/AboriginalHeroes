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
        public string url { get; set; }
        public string accession_number { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public List<string> related_subjects { get; set; }
        public List<string> object_type { get; set; }
        public List<string> maker { get; set; }
        public List<string> maker_text { get; set; }
        public List<string> makers_details { get; set; }
        public List<string> related_units { get; set; }
        public List<string> related_units_id { get; set; }
        public List<string> related_units_label { get; set; }
        public List<string> related_unit_all_ids { get; set; }
        public List<string> related_units_details { get; set; }
        public List<string> related_conflicts { get; set; }
        public List<string> conflict_category { get; set; }
        public List<string> related_conflict_sort { get; set; }
        public List<string> conflict_search { get; set; }
        public List<string> copyright_status { get; set; }
        public List<string> date_made { get; set; }
        public List<string> descriptor { get; set; }
        public List<string> object_type_main { get; set; }
        public List<string> other_numbers { get; set; }
        public List<string> related_places { get; set; }
        public List<string> related_places_id { get; set; }
        public List<string> related_places_label { get; set; }
        public List<string> related_people { get; set; }
        public List<string> object_type_sub { get; set; }
        public List<string> place_made { get; set; }
        public List<string> secondary_image { get; set; }

        public string base_rank { get; set; }
        public string birth_place { get; set; }
        public string birth_date { get; set; }
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

