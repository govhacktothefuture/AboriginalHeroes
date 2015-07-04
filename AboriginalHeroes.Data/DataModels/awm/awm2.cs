using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboriginalHeroes.Data.DataModels.Awm
{
    public class Item
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }

    public class Group
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }

    public class RootObject2
    {
        public List<Group> Groups { get; set; }
    }
}
