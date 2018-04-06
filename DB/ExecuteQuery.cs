using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Data.Common;

namespace SqlQueryRunner
{   
    public class ExecuteQuery : IDisposable
    {
        public SqlConnection _connection { get; private set; }
        SqlDataAdapter _da { get; set; }

        public ExecuteQuery()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings["dblocal"].ConnectionString;
            _da = new SqlDataAdapter();
        }

        public ExecuteQuery(string configNameOfConnectionString = "dblocal")
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = ConfigurationManager.ConnectionStrings[configNameOfConnectionString].ConnectionString;
            _da = new SqlDataAdapter();
        }

        public DataTable GetDataTableFromQuery(string query)
        {
            //SqlDataAdapter da = new SqlDataAdapter(query, _connection);
            SqlCommand command = new SqlCommand(query, _connection);
            _da.SelectCommand = command;

            DataTable dt = new DataTable();
            _da.Fill(dt);

            return dt;

        }

        public void InsertDatabaseWithDataTable(DataTable dt, string srcTable, Dictionary<string, string> param)
        {

            _da.InsertCommand = new SqlCommand();
            _da.InsertCommand.Connection = _connection;
            _da.InsertCommand.CommandType = CommandType.Text;
            _da.InsertCommand.CommandText = QueryBuilder.InsertQueryBuilder(srcTable, param);

            Console.Write(_da.InsertCommand.CommandText);

            //_da.Update(dt.Select(null, null, DataViewRowState.Deleted));
            //_da.Update(dt.Select(null, null, DataViewRowState.ModifiedOriginal)); 
            _da.Update(dt.Select(null, null, DataViewRowState.Added));

        }

        public void UpdateDatabaseWithDataTable(DataTable dt, string srcTable, Dictionary<string, string> param, long? id)
        {

            _da.UpdateCommand = new SqlCommand();
            _da.UpdateCommand.Connection = _connection;
            _da.UpdateCommand.CommandType = CommandType.Text;
            _da.UpdateCommand.CommandText = QueryBuilder.UpdateQueryBuilder(srcTable, param, id);

            Console.Write(_da.UpdateCommand.CommandText);

            //_da.Update(dt.Select(null, null, DataViewRowState.Deleted));
            _da.Update(dt.Select(null, null, DataViewRowState.ModifiedOriginal)); 
            
        }

        public void DeleteDatabaseWithDataTable(DataTable dt, string srcTable, long id)
        {

            _da.DeleteCommand = new SqlCommand();
            _da.DeleteCommand.Connection = _connection;
            _da.DeleteCommand.CommandType = CommandType.Text;
            _da.DeleteCommand.CommandText = QueryBuilder.DeleteQueryBuilder(srcTable, id);

            Console.Write(_da.DeleteCommand.CommandText);

            _da.Update(dt.Select(null, null, DataViewRowState.Deleted));
            //_da.Update(dt.Select(null, null, DataViewRowState.ModifiedOriginal)); 
            
        }


        public void Dispose()
        {
            _da.Dispose();
            _connection.Dispose();
        }

       
    }
}
