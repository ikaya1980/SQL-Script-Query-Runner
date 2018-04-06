using System.Collections.Generic;
using System.Text;

namespace SqlQueryRunner
{
    class QueryBuilder
    {
        internal static string InsertQueryBuilder(string tblName, Dictionary<string, string> columnNamesAndValues)
        {
            StringBuilder keys = new StringBuilder();
            StringBuilder values = new StringBuilder();

            foreach (var item in columnNamesAndValues)
            {
                keys.Append(item.Key);

                keys.Append(",");

                values.Append("'");
                values.Append(item.Value);
                values.Append("'");


                values.Append(",");
            }

            string strKey = keys.ToString();
            strKey = strKey.Remove(strKey.Length - 1, 1);

            string strValues = values.ToString();
            strValues = strValues.Remove(strValues.Length - 1, 1);

            return $"INSERT INTO {tblName} ({strKey}) VALUES({strValues})";
        }

        internal static string UpdateQueryBuilder(string tblName, Dictionary<string, string> columnNamesAndValues, long? Id)
        {
            //StringBuilder keys = new StringBuilder();
            StringBuilder setValues = new StringBuilder();

            foreach (var item in columnNamesAndValues)
            {
                setValues.Append(item.Key);
                if (item.Value == null)
                {
                    setValues.Append(" = ");
                    setValues.Append("NULL,");
                }
                else
                {
                    setValues.Append(" = '");
                    setValues.Append(item.Value);
                    setValues.Append("',");
                }
            }

           
            string strValues = setValues.ToString();
            strValues = strValues.Remove(strValues.Length - 1, 1);

            string strWhere = Id.HasValue ? $"WHERE Id = {Id.Value}" : "";
            return $"UPDATE {tblName} SET {strValues} {strWhere}";
        }

        internal static string DeleteQueryBuilder(string tblName, long id)
        {
            return $"DELETE FROM {tblName} WHERE Id = {id}";
        }
    }
}
