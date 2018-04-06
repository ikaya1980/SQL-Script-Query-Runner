using System;
using SqlQueryRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections.Generic;

namespace DBTest
{
    [TestClass]
    public class GetData
    {
        [TestMethod]
        public void GetDataMethodReturnNotNull()
        {
            using (ExecuteQuery da = new ExecuteQuery())
            {
                DataTable dt = da.GetDataTableFromQuery("select * from Person.Person");

                Assert.IsNotNull(dt);

            }
        }

        [TestMethod]
        public void InsetData()
        {
            using (ExecuteQuery da = new ExecuteQuery())
            {
                DataTable dt = da.GetDataTableFromQuery("select * from Person.Person");
                int count1 = dt.Rows.Count;


                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("BusinessEntityID", "20781");
                dictionary.Add("PersonType", "GC");
                dictionary.Add("FirstName", "Kemal");
                dictionary.Add("LastName", "Ata");
                dictionary.Add("EmailPromotion", "0");

                DataRow dr = dt.NewRow();
                dr["BusinessEntityID"] = dictionary["BusinessEntityID"];
                dr["PersonType"] = dictionary["PersonType"];
                dr["FirstName"] = dictionary["FirstName"];
                dr["LastName"] = dictionary["LastName"];
                dr["EmailPromotion"] = dictionary["EmailPromotion"];



                //dr["BusinessEntityID"] = 20779;
                //dr["PersonType"] = "GC";
                //dr["FirstName"] = "Kemal";
                //dr["LastName"] = "Ata";
                //dr["EmailPromotion"] = 0;

                dt.Rows.Add(dr);



                da.InsertDatabaseWithDataTable(dt, "Person.Person", dictionary);

                DataTable dt2 = da.GetDataTableFromQuery("select * from Person.Person");
                int count2 = dt2.Rows.Count;

                Assert.AreEqual(count2, count1 + 1);
            }
        }
    }
}
