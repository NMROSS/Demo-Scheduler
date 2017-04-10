using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    class DatabaseQuery
    {
        static private string connectionParams = "Data Source=cairo;Initial Catalog=VirtualChair;Integrated Security=True";


        /// <summary>
        /// /Execute SQL query and return datatable of result
        /// </summary>
        /// <param name="query">SQL Query e.g. select * from paper</param>
        /// <returns>DataTable of returned SQL table result</returns>
        static public DataTable DBQuery(String query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionParams))
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                    query, conn);
                    adapter.Fill(ds);
                    conn.Close();
                    return ds.Tables[0];
                } 

            }
            catch (Exception e)
            {
                Console.WriteLine("SQL Error (DBQuery): " + e);
                return null;
            }
        }

        /// <summary>
        /// Executes SQL query on DB 
        /// </summary>
        /// <param name="query">String Query e.g. select * from papers</param>
        static public void DBInsert(String query){
            using (SqlConnection connection = new SqlConnection(connectionParams))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;           
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        Console.Error.WriteLine("Error inserting '" + query + "' into database");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
