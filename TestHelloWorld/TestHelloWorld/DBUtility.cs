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
        private static string _mConString = @"Server=NESAR-2019\SQL2017;Database=TestHelloWorld;User ID=sa;password=onirban123@;Pooling=true;Max Pool Size=300;";
        //private static string _mConString = @"Server=DESKTOP-QS1VJGL\SQLEXPRESS;Database=TestHelloWorld;User ID=sa;password=bs23;Pooling=true;Max Pool Size=300;";
        //private static string _mConString = "Server=192.168.1.32;Database=TestHelloWorld;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";
        //private static string _mConString = "Server=192.168.0.31;Database=TestHelloWorld;User ID=sa;password=Techvision123?;Pooling=true;Max Pool Size=300;";
        public static string ConnectionString {
            set {
                _mConString = value;
            }
            get {
                return _mConString;
            }
        }

        public static void TransferFundInsert(string transId, string spname) {
            try {

                string SenderVID = "fahmid1@user.idtp", ReceiverVID = "parvez@user.idtp", SenderBankSwiftCode = "SBL1BDDH", ChannelName = "Online"
                , DeviceID = "", IDTPPIN = "123456", SenderAccNo = "123456789abcdefgijh", ReceiverAccNo = "123456789abcdefgijh", SendingBankRoutingNo = "098764123",
                ReceivingBankRoutingNo = "987543211";

                int senderId = 1, receiverId = 2, SenderBankId = 1, ReceiverBankId = 2, ReferenceTypeId = 1;

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand(spname, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    #region params
                    sql_cmnd.Parameters.AddWithValue("@SenderAccNo", SqlDbType.VarChar).Value = SenderAccNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverAccNo", SqlDbType.VarChar).Value = ReceiverAccNo;
                    sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Decimal).Value = 1.1;
                    sql_cmnd.Parameters.AddWithValue("@PaymentNote", SqlDbType.VarChar).Value = "Test";
                    sql_cmnd.Parameters.AddWithValue("@EndToEndID", SqlDbType.VarChar).Value = transId;
                    sql_cmnd.Parameters.AddWithValue("@MessageID", SqlDbType.VarChar).Value = transId;
                    sql_cmnd.Parameters.AddWithValue("@TransactionId", SqlDbType.VarChar).Value = transId;
                    sql_cmnd.Parameters.AddWithValue("@SendingBankRoutingNo", SqlDbType.VarChar).Value = SendingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankRoutingNo", SqlDbType.VarChar).Value = ReceivingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReferenceSendingBANK", SqlDbType.VarChar).Value = "SBL1REF";
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = transId;
                    sql_cmnd.Parameters.AddWithValue("@IPAddress", SqlDbType.VarChar).Value = "192.156.98.71";
                    sql_cmnd.Parameters.AddWithValue("@LatLong", SqlDbType.VarChar).Value = "23.8103° N, 90.4125° E";
                    sql_cmnd.Parameters.AddWithValue("@Location", SqlDbType.VarChar).Value = "Dhaka";
                    sql_cmnd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = "+8801987456789";
                    sql_cmnd.Parameters.AddWithValue("@TransactionTypeId", SqlDbType.Int).Value = 1;
                    sql_cmnd.Parameters.AddWithValue("@ReferernceNumber", SqlDbType.VarChar).Value = "REF12364564675876";
                    sql_cmnd.Parameters.AddWithValue("@InvoiceNumber", SqlDbType.VarChar).Value = "INV123456789123456";
                    sql_cmnd.Parameters.AddWithValue("@IEMEIID", SqlDbType.VarChar).Value = "IMEI12345678fgdfgd";
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankReference", SqlDbType.VarChar).Value = "1234567890";
                    sql_cmnd.Parameters.AddWithValue("@senderId", SqlDbType.BigInt).Value = senderId;
                    sql_cmnd.Parameters.AddWithValue("@receiverId", SqlDbType.BigInt).Value = receiverId;
                    sql_cmnd.Parameters.AddWithValue("@SenderBankId", SqlDbType.Int).Value = SenderBankId;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverBankId", SqlDbType.Int).Value = ReceiverBankId;
                    #endregion
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception) {
                throw;
            }
        }

        public static void TransferFundFinalSp(string spname)
        {
            try
            {

                string SenderAccNo = "22245612345678912", ReceiverAccNo = "33345612345678912", SendingBankRoutingNo = "098764123",
                ReceivingBankRoutingNo = "987543211";

                int senderId = 1, receiverId = 2, SenderBankId = 1, ReceiverBankId = 2;
                string IdtpRef = "IDTP" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand(spname, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    #region params
                    sql_cmnd.Parameters.AddWithValue("@SenderAccNo", SqlDbType.VarChar).Value = SenderAccNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverAccNo", SqlDbType.VarChar).Value = ReceiverAccNo;
                    sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Decimal).Value = 1.1;
                    sql_cmnd.Parameters.AddWithValue("@PaymentNote", SqlDbType.VarChar).Value = "Test";
                    sql_cmnd.Parameters.AddWithValue("@EndToEndID", SqlDbType.VarChar).Value = "E2Esblasd1231422";
                    sql_cmnd.Parameters.AddWithValue("@MessageID", SqlDbType.VarChar).Value = "REFSENBsblasd1231422";
                    sql_cmnd.Parameters.AddWithValue("@SendingBankRoutingNo", SqlDbType.VarChar).Value = SendingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankRoutingNo", SqlDbType.VarChar).Value = ReceivingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = IdtpRef;
                    sql_cmnd.Parameters.AddWithValue("@IPAddress", SqlDbType.VarChar).Value = "192.156.98.71";
                    sql_cmnd.Parameters.AddWithValue("@LatLong", SqlDbType.VarChar).Value = "23.8103° N, 90.4125° E";
                    sql_cmnd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = "+8801987456789";
                    sql_cmnd.Parameters.AddWithValue("@TransactionTypeId", SqlDbType.Int).Value = 1;
                    sql_cmnd.Parameters.AddWithValue("@ReferernceNumber", SqlDbType.VarChar).Value = "REF12364564675876";
                    sql_cmnd.Parameters.AddWithValue("@senderId", SqlDbType.BigInt).Value = senderId;
                    sql_cmnd.Parameters.AddWithValue("@receiverId", SqlDbType.BigInt).Value = receiverId;
                    sql_cmnd.Parameters.AddWithValue("@SenderBankId", SqlDbType.Int).Value = SenderBankId;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverBankId", SqlDbType.Int).Value = ReceiverBankId;
                    sql_cmnd.Parameters.AddWithValue("@FeeAmount", SqlDbType.Decimal).Value = 0;
                    sql_cmnd.Parameters.AddWithValue("@VATAmount", SqlDbType.Decimal).Value = 0;
                    sql_cmnd.Parameters.AddWithValue("@SendingBankReference", SqlDbType.VarChar).Value = "SBL1REF";
                    sql_cmnd.Parameters.AddWithValue("@SendingPSPReference", SqlDbType.VarChar).Value = "";
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankReference", SqlDbType.VarChar).Value = "1234567890";
                    sql_cmnd.Parameters.AddWithValue("@ReceivingPSPReference", SqlDbType.VarChar).Value = "";
                    #endregion
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
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

        public static async Task WriteDataAsync(string transId, string message, string dockerName, string clientRequestTime, string apiRequestStartTime) {
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
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                    }
                }
            }
            catch (Exception) {
                throw;
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
            catch (Exception) {
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
            catch (Exception) {
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
            catch (Exception) {
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
            catch (Exception) {
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
            catch (Exception) {
                throw;
            }
        }

        public static void TransactionSp(TransactionDTO transactionDTO)
        {
            try
            {

                string spname = "AddTransaction_V2";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand(spname, connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    #region params
                    sql_cmnd.Parameters.AddWithValue("@SenderAccNo", SqlDbType.VarChar).Value = transactionDTO.SenderAccNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverAccNo", SqlDbType.VarChar).Value = transactionDTO.ReceiverAccNo;
                    sql_cmnd.Parameters.AddWithValue("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(transactionDTO.Amount);
                    sql_cmnd.Parameters.AddWithValue("@PaymentNote", SqlDbType.VarChar).Value = transactionDTO.PaymentNote;
                    sql_cmnd.Parameters.AddWithValue("@EndToEndID", SqlDbType.VarChar).Value = transactionDTO.EndToEndID;
                    sql_cmnd.Parameters.AddWithValue("@MessageID", SqlDbType.VarChar).Value = transactionDTO.MessageID;
                    sql_cmnd.Parameters.AddWithValue("@SendingBankRoutingNo", SqlDbType.VarChar).Value = transactionDTO.SendingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankRoutingNo", SqlDbType.VarChar).Value = transactionDTO.ReceivingBankRoutingNo;
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = transactionDTO.ReferenceIDTP;
                    sql_cmnd.Parameters.AddWithValue("@IPAddress", SqlDbType.VarChar).Value = transactionDTO.IPAddress;
                    sql_cmnd.Parameters.AddWithValue("@LatLong", SqlDbType.VarChar).Value = transactionDTO.LatLong;
                    sql_cmnd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = transactionDTO.MobileNumber;
                    sql_cmnd.Parameters.AddWithValue("@TransactionTypeId", SqlDbType.Int).Value = Convert.ToInt32(transactionDTO.TransactionTypeId);
                    sql_cmnd.Parameters.AddWithValue("@ReferernceNumber", SqlDbType.VarChar).Value = transactionDTO.IDTPPIN;
                    sql_cmnd.Parameters.AddWithValue("@senderId", SqlDbType.BigInt).Value = transactionDTO.SenderId;
                    sql_cmnd.Parameters.AddWithValue("@receiverId", SqlDbType.BigInt).Value = transactionDTO.ReceiverId;
                    sql_cmnd.Parameters.AddWithValue("@SenderBankId", SqlDbType.Int).Value = transactionDTO.SenderBankId;
                    sql_cmnd.Parameters.AddWithValue("@ReceiverBankId", SqlDbType.Int).Value = transactionDTO.ReceiverBankId;
                    sql_cmnd.Parameters.AddWithValue("@FeeAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(transactionDTO.FeeAmount);
                    sql_cmnd.Parameters.AddWithValue("@VATAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(transactionDTO.VATAmount);
                    sql_cmnd.Parameters.AddWithValue("@SendingBankReference", SqlDbType.VarChar).Value = transactionDTO.SendingBankReference;
                    sql_cmnd.Parameters.AddWithValue("@SendingPSPReference", SqlDbType.VarChar).Value = transactionDTO.SendingPSPReference;
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankReference", SqlDbType.VarChar).Value = transactionDTO.ReceivingBankReference;
                    sql_cmnd.Parameters.AddWithValue("@ReceivingPSPReference", SqlDbType.VarChar).Value = transactionDTO.ReceivingPSPReference;
                    #endregion
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
