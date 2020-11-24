using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestHelloWorld.Controllers;

namespace TestHelloWorld
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

        public static void InsertTable(string transId, string message, string dockerName, string clientRequestTime, string apiRequestStartTime, ILogger<BaseController> _logger)
        {
            try
            {
                string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                string query = "INSERT INTO LoadBalancer " + "(TransactionId, Message, DockerName, ClientRequestTime, ApiRequestStartTime, CreateTime) " +
                    "VALUES('" + transId + "', '" + message + "', '" + dockerName +
                    "', '" + clientRequestTime +
                    "', '" + apiRequestStartTime +
                    "', '" + createTime +
                    "');";
                _logger.LogDebug(2, transId + "|B4CON- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                //TIME STAMP SQL1
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    _logger.LogDebug(3, transId + "|B4COM- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        _logger.LogDebug(4, transId + "|B4OPEN- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        connection.Open();
                        _logger.LogDebug(5, transId + "|AOPEN-B4EX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        command.ExecuteNonQuery();
                        _logger.LogDebug(6, transId + "|AEX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        connection.Close();
                        _logger.LogDebug(7, transId + "|ACLOSE- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    }
                }
                //TIME STAMP SQL2
            }
            catch (Exception)
            {
                throw;
            }
            //return true;
        }

        public static bool InsertExceptionLogTable(string transId, string expMessage, string dockerName, string createTime)
        {
            try
            {
                string query = "INSERT INTO ExceptionLog " + "(TransactionId, ExceptionText, DockerName, CreateTime) " +
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
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public static bool InsertApiResponseTimeTable(string transId, string apiResponseTime)
        {
            try
            {
                string query = "INSERT INTO ApiResponseTime " + "(TransactionId, ApiResponseTime) " +
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
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public static void InsertTransactionsTable(string accountNumber, string amount, string transId, string clientRequestTime, ILogger<BaseController> _logger)
        {
            try
            {
                string query = "INSERT INTO Transactions " + "(AccountNumber, Amount, TransactionId, ClientRequestTime) " +
                    "VALUES('" + accountNumber + "', '" + amount + "', '" + transId + "', '" + clientRequestTime + "');";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        _logger.LogDebug(4, transId + "|B4OPEN- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        connection.Open();
                        _logger.LogDebug(5, transId + "|AOPEN-B4EX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        command.ExecuteNonQuery();
                        _logger.LogDebug(6, transId + "|AEX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        connection.Close();
                        _logger.LogDebug(7, transId + "|ACLOSE- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            //return true;
        }

        public static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
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
