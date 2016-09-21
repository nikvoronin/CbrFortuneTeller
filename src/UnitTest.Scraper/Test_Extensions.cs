using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrFT.Scraper;

namespace UnitTest.Scraper
{
    [TestClass]
    public class Test_Extensions
    {
        [TestMethod]
        public void Extensions_EachDay_February()
        {
            DateTime from = new DateTime(2016, 2, 1);
            DateTime to = new DateTime(2016, 2, 29);
            int act = 0;
            int exp = 29;
            foreach (DateTime day in Extensions.EachDay(from, to))
                act++;

            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Extensions_EachDay_LeapYear()
        {
            DateTime from = new DateTime(2016, 1, 1);
            DateTime to = new DateTime(2016, 12, 31);
            int act = 0;
            int exp = 366;
            foreach (DateTime day in Extensions.EachDay(from, to))
                act++;

            Assert.AreEqual(exp, act);
        }
    }
}
