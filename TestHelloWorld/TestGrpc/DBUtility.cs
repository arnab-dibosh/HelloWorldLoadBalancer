using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestGrpc
{
    public class DBUtility
    {

        //private static string _mConString = @"Server=DESKTOP-QS1VJGL\SQLEXPRESS;Database=TestHelloWorld;User ID=sa;password=bs23;Pooling=true;Max Pool Size=300;";
        private static string _mConString = "Server=192.168.1.31;Database=TestHelloWorld;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";
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

        public static void WriteData(string transId, string message, string dockerName, string clientRequestTime, string apiRequestStartTime) {
            try {
                string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                string query = "INSERT INTO LoadBalancer " + "(TransactionId, Message, DockerName, ClientRequestTime, ApiRequestStartTime, CreateTime) " +
                    "VALUES('" + transId + "', '" + message + "', '" + dockerName +
                    "', '" + clientRequestTime +
                    "', '" + apiRequestStartTime +
                    "', '" + createTime +
                    "');";
                
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {

                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

    }
}
