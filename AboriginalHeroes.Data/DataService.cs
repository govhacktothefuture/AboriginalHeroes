using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using AboriginalHeroes.Data.DataModels.Awm;

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

        public async void GetAwmData()
        {
            string jsonString = await GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=indigenous&start=40&count=20");

            RootObject obj = JsonConvert.DeserializeObject<RootObject>(jsonString);

            //map to group
            //map to data items
        }

    }

}
