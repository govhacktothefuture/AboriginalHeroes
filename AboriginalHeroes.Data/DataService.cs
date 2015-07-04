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

        public async Task<RootObject> GetAwmData(string queryString)
        {
            string jsonString = await GetJsonStream(string.Format(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q={0}&start=40&count=20",queryString));

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonString);
            return rootObject;             
        }

        public async Task<DataGroup> GetDataGroup1()
        {
            RootObject rootObject = await GetAwmData("indigenous");
            DataGroup group = new DataGroup("1", "Heroes 1", "World War 1", "http://resources2.news.com.au/images/2014/04/18/1226889/222218-35ad41f8-c533-11e3-8bab-a811fb5e7a27.jpg", "Here is the group description");
            foreach (Result result in rootObject.results.Take(100))
            {
                string id = result.id;
                string title = result.base_rank;
                string subtitle = result.birth_place;
                //string imagePath = string.Format(@"https://static.awm.gov.au/images/collection/items/ACCNUM_SCREEN/{0}.JPG",result.id);
                string imagePath = @"http://www.cv.vic.gov.au/existingmedia/10583/AboriginalServicemen1.jpg";
                string description = result.birth_date;
                string content = "TODO: Create some content based on the result;";
                DataItem item = new DataItem(result.id,title,subtitle,imagePath,description,content);
                group.Items.Add(item);
            }
            return group;
        }

    }

}
