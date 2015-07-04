using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using AboriginalHeroes.Data;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AboriginalHeroes.Data.DataModels.Awm;

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
        public void JsonParserTest()
        {
            var service = new DataService();
            Task<string> task = service.GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=indigenous&start=40&count=20");
            //JArray jArray = JArray.Parse(task.ToString());
           

            task.Wait();

            var obj = JsonConvert.DeserializeObject<RootObject>(task.Result);
            Assert.IsNotNull(task.Result);
        }
    }
}
