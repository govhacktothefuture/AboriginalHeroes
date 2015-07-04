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

                Uri dataUri = new Uri("ms-appx:///AboriginalHeroes.Data/DataModels/local/daa.json");

                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);
     
                Data.DataModels.Awm.RootObject2 resultObj = JsonConvert.DeserializeObject<Data.DataModels.Awm.RootObject2>(jsonText);

                Assert.IsNotNull(resultObj);

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
