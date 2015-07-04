using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboriginalHeroes.Data.DataModels.Daa
{
    public class Item
    {
        public string name { get; set; }
        public string rank { get; set; }
        public string placeOfBirth { get; set; }
        public string died { get; set; }
        public string serviceDate { get; set; }
        public string photo { get; set; }
        public string article { get; set; }
    }

    public class Group
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }

    public class RootObject
    {
        public List<Group> Groups { get; set; }
    }
}
