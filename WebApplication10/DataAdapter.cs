using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlQueryRunner;

namespace WebApplication10
{
    public class DataAdapter
    {
        public DataTable GetEmployee()
        {
            using (ExecuteQuery da = new ExecuteQuery())
            {
                return da.GetDataTableFromQuery("select * from Person.Person");
            }
        }
    }
}