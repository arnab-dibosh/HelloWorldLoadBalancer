using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestHelloWorld.Controllers;
using TestHelloWorld.Model;

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

        public static bool InsertTransaction(Transaction transaction) {
            try {
                string query = "INSERT INTO Transactions " + "(TransactionId,SenderVid,SenderAccNo,SenderBankId,ReceicerVid,ReceiverAccNo,ReceicerBankId,Amount,TranDate,ClientRequestTime) " +
                    "VALUES('" + transaction.TransactionId + "', '" + transaction.SenderVid + "', '" + transaction.SenderAccNo +
                    "', '" + transaction.SenderBankId + "', '" + transaction.ReceicerVid +
                    "', '" + transaction.ReceiverAccNo + "', '" + transaction.ReceicerBankId +
                    "', '" + transaction.Amount + "', '" + transaction.TranDate +
                    "', '" + transaction.ClientRequestTime + "');";

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex) {
                throw;
            }
            return true;
        }

        public static User GetUser(string vid) {
            var user = new User();

            string query = $"select * from Users where VID='{vid}'";
            try {                
                
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();                        

                        while (reader.Read()) {
                            user.AccountNo=reader["AccountNo"].ToString();
                            user.BankId = Convert.ToInt32(reader["BankId"]);
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex) {
                throw;
            }
            return user;
        }

        public static Bank GetBank(int bankId) {
            var bank = new Bank();

            string query = $"select * from Banks where id={bankId}";
            try {

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            bank.Name = reader["Name"].ToString();
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex) {
                throw;
            }
            return bank;
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

        public async static void ProcessDirectPay(string senderVid, string receiverVid, decimal amount, string transactionId, string clientRequestTime) {
            try {
                

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("USP_DIRECT_PAY", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SENDER_VID", SqlDbType.NVarChar).Value = senderVid;
                    sql_cmnd.Parameters.AddWithValue("@RECEIVER_VID", SqlDbType.NVarChar).Value = receiverVid;
                    sql_cmnd.Parameters.AddWithValue("@AMOUNT", SqlDbType.Decimal).Value = amount;
                    sql_cmnd.Parameters.AddWithValue("@TRANSACTION_ID", SqlDbType.NVarChar).Value = transactionId;
                    sql_cmnd.Parameters.AddWithValue("@CLIENT_REQUEST_TIME", SqlDbType.DateTime2).Value = clientRequestTime;
                    sql_cmnd.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            catch (Exception Ex) {
                throw;
            }
        }

    }
}
