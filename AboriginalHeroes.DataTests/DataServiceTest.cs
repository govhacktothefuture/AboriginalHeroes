using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using AboriginalHeroes.Data;
using System.Threading.Tasks;

namespace AboriginalHeroes.DataTests
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void DownloadJsonTest()
        {
            var service = new DataService();
            Task<string> task = service.GetJsonStream(@"https://www.awm.gov.au/direct/data.php?key=WW1HACK2015&q=aboriginal&start=40&count=20");
            task.Wait();
            Assert.IsNotNull(task.Result);
        }
    }
}
