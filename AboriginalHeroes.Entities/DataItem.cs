﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboriginalHeroes.Entities
{
    public class DataItem
    {
        public DataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
        }

        public string UniqueId { get; private set; }
        public string Title { get;  set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public string VideoUrl { get; set; }

        public GroupType GroupType { get; set; }

        public double PlaceOfBirthLat { get; set; }
        public double PlaceOfBirthLong { get; set; }
        public double PlaceOfDeathLat { get; set; }
        public double PlaceOfDeathLong { get; set; }


        public override string ToString()
        {
            return this.Title;
        }
    }

    public enum GroupType
    {
        Person,
        PersonWithMap,
        Document,
        Video
    }

}
