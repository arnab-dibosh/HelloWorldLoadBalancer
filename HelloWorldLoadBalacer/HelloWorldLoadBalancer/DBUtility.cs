using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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

        public static bool InsertTable(string transId, string message, string dockerName, string clientRequestTime, string apiRequestStartTime)
        {
            try
            {
                string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                String query = "INSERT INTO LoadBalancer " + "(TransactionId, Message, DockerName, ClientRequestTime, ApiRequestStartTime, CreateTime) " +
                    "VALUES('" + transId + "', '"  + message + "', '" + dockerName +
                    "', '" + clientRequestTime + 
                    "', '" + apiRequestStartTime +
                    "', '" + createTime +
                    "');";

                //TIME STAMP SQL1
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                //TIME STAMP SQL2
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InsertExceptionLogTable(string transId, string expMessage, string dockerName, string createTime)
        {
            try
            {
                String query = "INSERT INTO ExceptionLog " + "(TransactionId, ExceptionText, DockerName, CreateTime) " +
                    "VALUES('" + transId + "', '" + expMessage + "', '" + dockerName +
                    "', '" + createTime + "');";

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
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static bool InsertApiResponseTimeTable(string transId, string apiResponseTime)
        {
            try
            {
                String query = "INSERT INTO ApiResponseTime " + "(TransactionId, ApiResponseTime) " +
                    "VALUES('" + transId + "', '" + apiResponseTime + "');";

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
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    if(connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
