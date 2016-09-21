using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrFT.DbModel;
using CbrFT.DbModel.Entity;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnitTest.DbModel
{
    [TestClass]
    public class Test_DbManager
    {
        [TestMethod]
        public void DbManager_GetCurrencies()
        {
            using (DbModelContext ctx = new DbModelContext())
            {
                Currency c = new Currency();
                c.Vname = "Test currency";
                c.Vcode = 123;
                c.Vnom = 100500M;
                c.VchCode = "XYZ";

                ctx.Currencies.Add(c);
                ctx.SaveChanges();
            }

            DbManager man = new DbManager();
            List<Currency> cs = man.GetCurrencies();

            Assert.IsNotNull(cs);
            Assert.IsTrue(cs.Count > 0);
        }

        [TestMethod]
        public void DbManager_ClearTable()
        {
            DbManager man = new DbManager();
            man.ClearTable(DateTime.UtcNow, DateTime.UtcNow);

            using (DbModelContext ctx = new DbModelContext())
            {
                var vals = from v in ctx.CurrencyValues
                           select v;

                Assert.IsNotNull(vals);
                Assert.IsTrue(vals.ToList().Count == 0);
            }
        }
    }
}
