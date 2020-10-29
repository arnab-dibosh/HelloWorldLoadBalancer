using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldLoadBalacer
{
    public class DBUtility
    {
        private static string _mConString;
        public static string ConnectionString
        {

            set
            {
                _mConString = value;
            }
            get
            {
               return _mConString;
            }
        }

        public static bool InsertTable(string message, string dockerName)
        {
            try
            {
                string createTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
                String query = "INSERT INTO LoadBalancer " + "(Message, DockerName, CreateTime) VALUES('" + message + "', '" + dockerName + "', '" +
                    createTime + "');";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
