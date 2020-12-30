using MySql.Data.MySqlClient;
using System;

namespace TestHelloWorld
{
    public class MysqlDbUtility
    {
        //private static string _mConString = "Server=59.152.61.37;Port=11082;Database=IDTPServerDB;User=root;Password=Techvision123@;Pooling=true;Max Pool Size=200;";
        private static string _mConString = "Server=192.168.1.21;Database=IDTPServerDB;User=root;Password=Techvision123@;Pooling=true;Max Pool Size=200;";

        public static string ConnectionString {

            set {
                _mConString = value;
            }
            get {
                return _mConString;
            }
        }

        public static void WriteData(string transId, string message, string dockerName, string clientRequestTime, string apiRequestStartTime) {
            try {
                string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                String query = "INSERT INTO loadbalancer " + "(TransactionId, Message, DockerName, ClientRequestTime, ApiRequestStartTime, CreateTime) " +
                    "VALUES('" + transId + "', '" + message + "', '" + dockerName +
                    "', '" + clientRequestTime +
                    "', '" + apiRequestStartTime +
                    "', '" + createTime +
                    "');";
                using (MySqlConnection connection = new MySqlConnection(ConnectionString)) {
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public static bool InsertExceptionLogTable(string transId, string expMessage, string dockerName, string createTime) {
            try {
                String query = "INSERT INTO exceptionlog " + "(TransactionId, ExceptionText, DockerName, CreateTime) " +
                    "VALUES('" + transId + "', '" + expMessage + "', '" + dockerName +
                    "', '" + createTime + "');";

                using (MySqlConnection connection = new MySqlConnection(ConnectionString)) {
                    using (MySqlCommand command = new MySqlCommand(query, connection)) {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception) {
                throw;
            }
            return true;
        }
    }
}
