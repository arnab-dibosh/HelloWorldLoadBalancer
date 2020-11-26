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

        private static string _mConString = @"Server=DESKTOP-QS1VJGL\SQLEXPRESS;Database=TestHelloWorld;User ID=sa;password=bs23;Pooling=true;Max Pool Size=300;";
        //private static string _mConString = "Server=192.168.1.33;Database=TestHelloWorld;User ID=sa;password=Techvision123?;Pooling=true;Max Pool Size=300;";
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
             

        public static bool OneInsertDynamicSqlNoLog(string transactionId, string senderVid, string senderAccNo, int senderBankId,
            string receicerVid, string receiverAccNo, int receicerBankId, decimal amount, DateTime tranDate, string clientRequestTime, string tableName) {
            try {
                string query = "INSERT INTO " + tableName + "(TransactionId,SenderVid,SenderAccNo,SenderBankId,ReceicerVid,ReceiverAccNo,ReceicerBankId,Amount,TranDate,ClientRequestTime) " +
                    "VALUES('" + transactionId + "', '" + senderVid + "', '" + senderAccNo +
                    "', '" + senderBankId + "', '" + receicerVid +
                    "', '" + receiverAccNo + "', '" + receicerBankId +
                    "', '" + amount + "', '" + tranDate +
                    "', '" + clientRequestTime + "');";

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

        public static bool OneInsertDynamicSqlWithLog(string transactionId, string senderVid, string senderAccNo, int senderBankId,
            string receicerVid, string receiverAccNo, int receicerBankId, decimal amount, DateTime tranDate, string clientRequestTime, string tableName, ILogger<DynamicSqlController> _logger) {
            try {
                string query = "INSERT INTO " + tableName + "(TransactionId,SenderVid,SenderAccNo,SenderBankId,ReceicerVid,ReceiverAccNo,ReceicerBankId,Amount,TranDate,ClientRequestTime) " +
                    "VALUES('" + transactionId + "', '" + senderVid + "', '" + senderAccNo +
                    "', '" + senderBankId + "', '" + receicerVid +
                    "', '" + receiverAccNo + "', '" + receicerBankId +
                    "', '" + amount + "', '" + tranDate +
                    "', '" + clientRequestTime + "');";

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        _logger.LogDebug(1, transactionId + "|B4OPEN- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        connection.Open();
                        _logger.LogDebug(2, transactionId + "|AOPEN-B4EX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                        command.ExecuteNonQuery();
                        connection.Close();
                        _logger.LogDebug(3, transactionId + "|AEX- " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
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
                            user.AccountNo = reader["AccountNo"].ToString();
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

        public static void OneInsertSpTran(string transactionId, string clientRequestTime, string spName) {
            try {

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand(spName, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@TRANSACTION_ID", SqlDbType.NVarChar).Value = transactionId;
                    sql_cmnd.Parameters.AddWithValue("@CLIENT_REQUEST_TIME", SqlDbType.DateTime2).Value = clientRequestTime;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception Ex) {
                throw;
            }
        }
        public static void TwoGetOneInsertSpTran(string senderVid, string receiverVid, decimal amount, string transactionId, string clientRequestTime, string spName) {
            try {                

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand(spName, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SENDER_VID", SqlDbType.NVarChar).Value = senderVid;
                    sql_cmnd.Parameters.AddWithValue("@RECEIVER_VID", SqlDbType.NVarChar).Value = receiverVid;
                    sql_cmnd.Parameters.AddWithValue("@AMOUNT", SqlDbType.Decimal).Value = amount;
                    sql_cmnd.Parameters.AddWithValue("@TRANSACTION_ID", SqlDbType.NVarChar).Value = transactionId;
                    sql_cmnd.Parameters.AddWithValue("@CLIENT_REQUEST_TIME", SqlDbType.DateTime2).Value = clientRequestTime;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception Ex) {
                throw;
            }
        }

        public static async void TwoGetOneInsertSpTranAsynch(string senderVid, string receiverVid, decimal amount, string transactionId, string clientRequestTime, string spName) {
            try {
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {                    

                    await connection.OpenAsync().ConfigureAwait(true);
                    
                    SqlCommand sql_cmnd = new SqlCommand(spName, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SENDER_VID", SqlDbType.NVarChar).Value = senderVid;
                    sql_cmnd.Parameters.AddWithValue("@RECEIVER_VID", SqlDbType.NVarChar).Value = receiverVid;
                    sql_cmnd.Parameters.AddWithValue("@AMOUNT", SqlDbType.Decimal).Value = amount;
                    sql_cmnd.Parameters.AddWithValue("@TRANSACTION_ID", SqlDbType.NVarChar).Value = transactionId;
                    sql_cmnd.Parameters.AddWithValue("@CLIENT_REQUEST_TIME", SqlDbType.DateTime2).Value = clientRequestTime;
                    await sql_cmnd.ExecuteNonQueryAsync().ConfigureAwait(true);
                    
                    connection.Close();
                }
            }
            catch (Exception Ex) {
                throw;
            }
        }

    }
}
