using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using CbrFT.DbModel.Entity;

namespace UnitTest.DbModel
{
    [TestClass]
    public class Test_CurrencyBuilder
    {
        [TestMethod]
        public void CurrencyBuilder_Create()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Vname", typeof(string));
            table.Columns.Add("Vcode", typeof(int));
            table.Columns.Add("VchCode", typeof(string));
            table.Columns.Add("Vnom", typeof(decimal));

            DataRow row = table.NewRow();
            row["Vname"] = "Test row";
            row["Vcode"] = 123;
            row["VchCode"] = "XYZ";
            row["Vnom"] = 100500M;

            Currency exp = CurrencyBuilder.Create(row);

            Assert.AreEqual(exp.Vcode, 123);
            Assert.AreEqual(exp.Vnom, 100500M);
            Assert.AreEqual(exp.VchCode, "XYZ");
            Assert.AreEqual(exp.Vname, "Test row");
        }
    }
}
