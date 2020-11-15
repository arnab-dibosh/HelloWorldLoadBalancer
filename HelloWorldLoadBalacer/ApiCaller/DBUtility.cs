using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaller
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

        public static bool InsertTable(string transID, string dockerName, string message, string dtTime)
        {
            try
            {
                //string createTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
                String query = "INSERT INTO ClientLoadBalancer " + "(TransactionId, Message, DockerName, CreateTime) VALUES('" +
                    transID + "', '" + message + "', '" + dockerName + "', '" + dtTime + "')";

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

        public static bool UpdateTable(string transID, string dockerName, string dtTime, string apiResponseTime, string apiStartTime)
        {
            try
            {
                String query = "UPDATE ClientLoadBalancer SET ReturnTime = '" + dtTime + "', " +
                    "ApiResponseTime = '" + apiResponseTime + "', " +
                    "RoundTripTime = DATEDIFF(MILLISECOND, CreateTime, '" + dtTime + "'), " +
                    "ApiToClientTime = DATEDIFF(MILLISECOND, '" + apiResponseTime + "', '" + dtTime + "'), " +
                    "ApiToServerTime = DATEDIFF(MILLISECOND, '" + apiStartTime + "', '" + apiResponseTime + "') " +
                    "WHERE TransactionId = '" + transID + "' AND DockerName = '" + dockerName + "'";

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
