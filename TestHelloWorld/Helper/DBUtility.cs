﻿using Helper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Helper
{
    public class DBUtility
    {

        //private static string ConnectionString = @"Server=DESKTOP-QS1VJGL\SQLEXPRESS;Database=TestHelloWorld;User ID=sa;password=bs23;Pooling=true;Max Pool Size=300;";
        public static string ConnectionString = "Server=192.168.100.12;Database=TestHelloWorld;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";

        public static User GetUserByVid(string vid) {
            var user = new User();

            string query = $"select UserId, FullName from Users where VID='{vid}'";
            try {

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            user.UserId = Convert.ToInt64(reader["UserId"]);
                            user.FullName = reader["FullName"].ToString();
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

        public static List<UserAccountInformationDTO> GetAllUserAccountInfo(bool takeModifiedOnly = false) {

            //InsertInLog(0, "Getting Acc Info form db");
            var allAccInfo = new List<UserAccountInformationDTO>();
            //string whereClause = takeModifiedOnly ? $"where IsLoaded=0" : "";
            //string query = $"select Id, DeviceID, FinancialInstitutionId, AccountNumber, IsLoaded from UserAccountInformation {whereClause}";
            //string updateQuery = $"update UserAccountInformation set IsLoaded=1 {whereClause}";

            try {
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SyncModifiedAndNewUserAccount", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        #region params
                        command.Parameters.AddWithValue("@TakeModifiedOnly", SqlDbType.Bit).Value = takeModifiedOnly;
                        #endregion
                        command.CommandTimeout = 6000;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            var accInfo = new UserAccountInformationDTO();
                            accInfo.Id = Convert.ToInt64(reader["Id"]);
                            accInfo.DeviceID = reader["DeviceID"].ToString();
                            accInfo.FinancialInstitutionId = Convert.ToInt32(reader["FinancialInstitutionId"]);
                            accInfo.AccountNumber = reader["AccountNumber"].ToString();
                            accInfo.IsLoaded = Convert.ToInt32(reader["IsLoaded"]);
                            allAccInfo.Add(accInfo);
                        }
                        reader.Close();

                        //command.CommandText = updateQuery;
                        //if (takeModifiedOnly) command.ExecuteNonQuery();
                        connection.Close();
                }
            }
            catch (Exception ex) {
                InsertInLog(1, $"Error while getting AccInfo form db : {ex.Message}");
                throw;
            }
            return allAccInfo;
        }

        public static List<User> GetAllUser(bool takeModifiedOnly = false) {

            //InsertInLog(0, "Getting user form db");

            var allUsers = new List<User>();
            //string whereClause = takeModifiedOnly ? $"where IsLoaded=0" : "";
            //string query = $"select UserId, DefaultFI, VirtualID, IsLoaded, IDTP_PIN, SecretSalt from IDTPUsers {whereClause}";
            //string updateQuery = $"update IDTPUsers set IsLoaded=1 {whereClause}";

            try {

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SyncModifiedAndNewIdtpUsers", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        #region params
                        command.Parameters.AddWithValue("@TakeModifiedOnly", SqlDbType.Bit).Value = takeModifiedOnly;
                        #endregion
                        command.CommandTimeout = 6000;
                        SqlDataReader reader = command.ExecuteReader();             

                        while (reader.Read()) {
                            var user = new User();
                            user.UserId = Convert.ToInt64(reader["UserId"]);
                            user.DefaultFI = Convert.ToInt64(reader["DefaultFI"]);
                            user.VirtualID = reader["VirtualID"].ToString();
                            user.IsLoaded = Convert.ToInt32(reader["IsLoaded"]);
                            user.IDTP_PIN = reader["IDTP_PIN"].ToString();
                            user.SecretSalt = reader["SecretSalt"].ToString();
                            allUsers.Add(user);
                        }
                        reader.Close();

                        //command.CommandText = updateQuery;
                        //if (takeModifiedOnly) command.ExecuteNonQuery();
                        connection.Close();
                }
            }
            catch (Exception ex) {
                InsertInLog(1, $"Error while getting user form db : {ex.Message}");
                throw;
            }
            return allUsers;
        }

        public static void TransferFundFinalSp(TransactionDTOReq transactionDTO) {
            try {

                string spname = "AddTransaction_V2";
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
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
            catch (Exception ex) {
                throw ex;
            }
        }

        public static void InsertInLog(int isError, string msg) {

            string query = $"INSERT INTO IDTPErrorLog VALUES (1,1 ,null ,'{msg}' ,'{DateTime.Now}' ,1 ,{isError})";

            try {

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

        public static void TransferFundFinalSp(string csvData) {
            try {
                string spname = "AddTransaction_V2";
                var serviceRequestDTO = new TransactionDTOReq();
                var payLoads = csvData.Split(",").Select(x => x.Trim()).ToArray();
                serviceRequestDTO.SenderBankSwiftCode = payLoads[0];
                serviceRequestDTO.SenderVID = payLoads[1];
                serviceRequestDTO.ReceiverVID = payLoads[2];
                serviceRequestDTO.Amount = payLoads[3];
                serviceRequestDTO.ChannelName = payLoads[4];
                serviceRequestDTO.DeviceID = payLoads[5];
                serviceRequestDTO.MobileNumber = payLoads[6];
                serviceRequestDTO.LatLong = payLoads[7];
                serviceRequestDTO.IPAddress = payLoads[8];
                serviceRequestDTO.IDTPPIN = payLoads[9];
                serviceRequestDTO.EndToEndID = payLoads[10];
                serviceRequestDTO.SendingBankTxId = payLoads[11];
                serviceRequestDTO.SendingBankReference = payLoads[12];
                serviceRequestDTO.MessageID = payLoads[13];

                string SenderAccNo = "22245612345678912", ReceiverAccNo = "33345612345678912", SendingBankRoutingNo = "098764123",
                ReceivingBankRoutingNo = "987543211";
                int senderId = 1, receiverId = 2, SenderBankId = 1, ReceiverBankId = 2;



                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
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
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = serviceRequestDTO.ReferenceIDTP;
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
                    sql_cmnd.Parameters.AddWithValue("@SendingBankReference", SqlDbType.VarChar).Value = "SBL1REF"; ;
                    sql_cmnd.Parameters.AddWithValue("@SendingPSPReference", SqlDbType.VarChar).Value = "";
                    sql_cmnd.Parameters.AddWithValue("@ReceivingBankReference", SqlDbType.VarChar).Value = "1234567890";
                    sql_cmnd.Parameters.AddWithValue("@ReceivingPSPReference", SqlDbType.VarChar).Value = "";
                    #endregion
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public static void SimpleInsert(string item) {

            using (SqlConnection con = new SqlConnection(ConnectionString)) {
                con.Open();
                var commandString = $"INSERT INTO TxTable (Value1) VALUES ('{item}')";
                SqlCommand cmd = new SqlCommand(commandString, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void WriteData() {
            try {
                string query = $"INSERT INTO TestHelloApp (Id, AppServer, Message, CreatedOn)VALUES('{Guid.NewGuid().ToString()}'," +
                    $" '{Environment.MachineName}', 'Hello', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")}');";


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
