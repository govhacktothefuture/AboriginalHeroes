using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using AboriginalHeroes.Data.DataModels.Awm;
using AboriginalHeroes.Entities;
using System.IO;
using Newtonsoft.Json.Linq;
using AboriginalHeroes.Data.DataModels.Naa;

namespace AboriginalHeroes.Data
{
    public class DataService
    {
        /// <summary>
        /// url =  http://(urlHere)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetJsonStream(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<AboriginalHeroes.Data.DataModels.Awm.RootObject> GetAwmData(string queryString)
        {
            string jsonString = await GetJsonStream(string.Format(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q={0}&start=40&count=20",queryString));                                                                    
            AboriginalHeroes.Data.DataModels.Awm.RootObject rootObject = JsonConvert.DeserializeObject<DataModels.Awm.RootObject>(jsonString);
            return rootObject;             
        }

        public async Task<AboriginalHeroes.Data.DataModels.Awm.RootObject> GetAwmFilmData()
        {
            string jsonString = await GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=related_subjects:%22Aboriginal%22%20AND%20type:%22Film%22%20");
            AboriginalHeroes.Data.DataModels.Awm.RootObject rootObject = JsonConvert.DeserializeObject<DataModels.Awm.RootObject>(jsonString);
            return rootObject;
        }

        

        public async Task<AboriginalHeroes.Data.DataModels.Naa.RootObject> GetNaaData(string queryString)
        {
            string jsonString = await GetJsonStream(string.Format(@"https://api.naa.gov.au/naa/api/v1/person/search-series-b2455?keyword={0}&rows=50&page=1&app_id=598e8f24&app_key=bf81bc01f4f7c9b74e20be0ce7527395", queryString));

            AboriginalHeroes.Data.DataModels.Naa.RootObject rootObject = JsonConvert.DeserializeObject<AboriginalHeroes.Data.DataModels.Naa.RootObject>(jsonString);
            return rootObject;
        }

        public async Task<DataGroup> GetDataGroup1()
        {
            //roll_type:%22Roll%20of%20Honour%22
                                                                                         
            AboriginalHeroes.Data.DataModels.Awm.RootObject rootObject = await GetAwmData(@"related_subjects:""Indigenous servicemen"" AND type:""Photograph"" ");         
            //RootObject rootObject = await GetAwmData(@"roll_type:""Roll of Honour"" AND related_subjects:""Indigenous"" AND type:""Photograph"" ");//indigenous            
            DataGroup group = new DataGroup("1", "Servicemen", "Their story, our pride", "http://resources2.news.com.au/images/2014/04/18/1226889/222218-35ad41f8-c533-11e3-8bab-a811fb5e7a27.jpg", "Details of indigenous personnel serving in World War conflicts.");
            foreach (Result result in rootObject.results.Take(100))
            {
                string id = result.id;
                string title = result.title;//"Photograph (" + result.id + ")";                    
                string subtitle = "Photograph"; //result.base_rank;
                string imagePath = string.Format(@"https://static.awm.gov.au/images/collection/items/ACCNUM_SCREEN/{0}.JPG",result.accession_number);
                string description = result.description;
                StringBuilder content = new StringBuilder();
                if (result.date_made != null)  content.Append("Date Made: " + result.date_made[0]);
                if (result.place_made != null) content.Append("\nPlace: " + result.place_made[0]);
                if (result.maker != null) content.Append("\nMaker: " + result.maker[0]);
                if (result.related_conflicts != null) content.Append("\nConflict(s): " + result.related_conflicts[0]);
                if (result.related_people != null) content.Append("\nRelated Peoples(s): " + string.Join(", ", result.related_people));
                if (result.accession_number != null) content.Append("\nAccess Number: " + result.accession_number);

                DataItem item = new DataItem(id, title, subtitle, imagePath, description, content.ToString());
                item.GroupType = GroupType.Person;
                group.Items.Add(item);
            }
            return group;
        }

        public async Task<DataGroup> GetDataGroup2()
        {
            AboriginalHeroes.Data.DataModels.Naa.RootObject rootobject = await GetNaaData("indigenous");
            DataGroup group = new DataGroup("2", "Heroes 2", "World War 1 - Group 2", "", "Here is the group description");
            foreach (AboriginalHeroes.Data.DataModels.Naa.ResultSet result in rootobject.ResultSet)
            {
                string id = result.person_id.ToString();
                string title = result.name;
                string subtitle = result.first_name + " " + result.family_name;
                string imagePath = await GetGroup2Images(result.name);
                string description = "TODO:  ADD Description";
                string content = "TODO: add content";
                string barcode = result.barcode;

                DataItem item = new DataItem(id, title, subtitle, imagePath, description, content);
                item.GroupType = GroupType.Document;
                group.Items.Add(item);
            }
            return group;
        }

        public DataGroup GetDataGroup3()
        {
            return null;
        }

        public async Task<string> GetGroup2Images(string name)
        {
            string jsonString = await GetJsonStream(string.Format("https://api.naa.gov.au/naa/api/v1/recorditem/search-series-b2455?keyword={0}&rows=1&page=1&app_id=598e8f24&app_key=bf81bc01f4f7c9b74e20be0ce7527395", name));
            RootObjectBarcode result = JsonConvert.DeserializeObject<RootObjectBarcode>(jsonString);

            return (string.Format("http://recordsearch.naa.gov.au/SearchNRetrieve/NAAMedia/ShowImage.aspx?B={0}&S=1&T=P", result.ResultSet.First().barcode));

        }

        public async Task<DataGroup> GetDataGroupVideos()
        {     
            AboriginalHeroes.Data.DataModels.Awm.RootObject rootObject = await GetAwmFilmData();
            //RootObject rootObject = await GetAwmData(@"roll_type:""Roll of Honour"" AND related_subjects:""Indigenous"" AND type:""Photograph"" ");//indigenous            
            DataGroup group = new DataGroup("3", "Videos", "The film and videos include many collections including oral histories, film commissions", "http://resources2.news.com.au/images/2014/04/18/1226889/222218-35ad41f8-c533-11e3-8bab-a811fb5e7a27.jpg", "Details of indigenous personnel serving in World War conflicts.");
            foreach (Result result in rootObject.results.Take(100))
            {
                string id = result.id;
                string title = result.title;
                string subtitle = result.base_rank;
                string imagePath = @"Images/video.png";
                string videoUrl = string.Format(@"http://static.awm.gov.au/video/{0}.mp4", result.accession_number);
                string description = result.description;
                string content = "TODO: Create some content based on the result;";
                DataItem item = new DataItem(id, title, subtitle, imagePath, description, content);
                item.VideoUrl = videoUrl;
                item.GroupType = GroupType.Video;
                group.Items.Add(item);
            }
            return group;
        }
        //


    }

}
