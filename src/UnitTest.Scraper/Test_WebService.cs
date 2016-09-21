using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrFT.Scraper.CbrService;
using System.Data;

namespace UnitTest.Scraper
{
    [TestClass]
    public class Test_WebService
    {
        [TestMethod]
        public void DailyInfo_GetCursOnDate_WorkingDay()
        {
            DailyInfo di = new DailyInfo();
            Assert.IsNotNull(di);

            DateTime onDate = new DateTime(2016, 9, 20);

            DataSet dataSet = di.GetCursOnDate(onDate);
            Assert.IsNotNull(dataSet.Tables);
            Assert.IsTrue(dataSet.Tables.Count == 1);
            Assert.IsNotNull(dataSet.Tables[0]);
            Assert.IsTrue(dataSet.Tables[0].Rows.Count > 0);

            Assert.IsNotNull(dataSet.Tables[0].Rows[0]["Vname"]);
            Assert.IsNotNull(dataSet.Tables[0].Rows[0]["Vcode"]);
            Assert.IsNotNull(dataSet.Tables[0].Rows[0]["VchCode"]);
            Assert.IsNotNull(dataSet.Tables[0].Rows[0]["Vcurs"]);
            Assert.IsNotNull(dataSet.Tables[0].Rows[0]["Vnom"]);

            Assert.IsTrue(dataSet.ExtendedProperties.ContainsKey("OnDate"));
        }
    }
}
