using Microsoft.VisualStudio.TestTools.UnitTesting;
using CbrFT.DbModel;
using System.Data.Common;

namespace UnitTest.DbModel
{
    [TestClass]
    public class Test_DbModelContext
    {
        [TestMethod]
        public void DbModelContext_ConnectionOpen()
        {
            using (DbModelContext context = new DbModelContext())
            {
                DbConnection conn = context.Database.Connection;
                try
                {
                    conn.Open();   // check the database connection
                }
                catch
                {
                    Assert.Fail();
                }
            }
        }
    }
}
