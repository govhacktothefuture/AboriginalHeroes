using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using AboriginalHeroes.Data;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AboriginalHeroes.Data.DataModels.Awm;
using Windows.Storage;
using Windows.Data.Json;
using AboriginalHeroes.Entities;
using System.Diagnostics;
using AboriginalHeroes.Data.DataModels.Naa;

namespace AboriginalHeroes.DataTests
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void DownloadJsonTest()
        {
            var service = new DataService();
            Task<string> task = service.GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=indigenous&start=40&count=20");
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [TestMethod]
        public void LocalFileJsonTest()
        {
            Assert.IsNotNull(LoadLocalFile());
        }

        [TestMethod]
        public void JsonParserTest()
        {
            var service = new DataService();
            Task<string> task = service.GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=indigenous&start=40&count=20");
            //JArray jArray = JArray.Parse(task.ToString());


            task.Wait();

            var obj = JsonConvert.DeserializeObject<Data.DataModels.Awm.RootObject>(task.Result);
            Assert.IsNotNull(task.Result);
        }

        private async Task LoadLocalFile()
        {
            try
            {
                DataSource ds = new DataSource();
                if (ds.Groups.Count != 0)
                    return;

                Uri dataUri = new Uri("ms-appx:///AboriginalHeroes.Data/DataModels/local/soldiers.json");
                //Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);
                jsonText = jsonText.Replace("\r", "");
                jsonText = jsonText.Replace("\n", "");
                jsonText = jsonText.Replace(" ", "");
                JsonObject jsonObject = JsonObject.Parse(jsonText);
                JsonArray jsonArray = jsonObject["Groups"].GetArray();

                foreach (JsonValue groupValue in jsonArray)
                {
                    JsonObject groupObject = groupValue.GetObject();
                    DataGroup group = new DataGroup(groupObject["UniqueId"].GetString(),
                                                                groupObject["Title"].GetString(),
                                                                groupObject["Subtitle"].GetString(),
                                                                groupObject["ImagePath"].GetString(),
                                                                groupObject["Description"].GetString());

                    foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                    {
                        JsonObject itemObject = itemValue.GetObject();
                        group.Items.Add(new DataItem(itemObject["UniqueId"].GetString(),
                                                           itemObject["Title"].GetString(),
                                                           itemObject["Subtitle"].GetString(),
                                                           itemObject["ImagePath"].GetString(),
                                                           itemObject["Description"].GetString(),
                                                           itemObject["Content"].GetString()));
                    }
                    ds.Groups.Add(group);
                }


                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void GetGroup2Image()
        {
            var service = new DataService();
            Task<string> task = service.GetGroup2Images("Charles Tednee Blackman");
            task.Wait();
            Assert.IsNotNull(task.Result);
            return;
        }


    }
}
