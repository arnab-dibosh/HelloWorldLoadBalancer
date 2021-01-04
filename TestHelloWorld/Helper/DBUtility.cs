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
        static string ConnectionString = "Server=192.168.1.31;Database=IDTPServerDB;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";
        //static string ConnectionString = "Server=59.152.61.37,11081;Database=IDTPServerDB;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";

        public static User GetUserByVid(string vid) {
            var user = new User();

            string query = $"select * from Users where VID='{vid}'";
            try {

                DataTable dataTable = new DataTable();
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
            catch (Exception ex) {
                throw;
            }
            return user;
        }

        public static List<User> GetAllHardCodedUser() {

            var allUsers = new List<User>();

            try {

                for (int i = 0; i < 10; i++) {
                    var user = new User() {
                        FullName = $"User{i}"
                    };
                    allUsers.Add(user);
                }
            }
            catch (Exception ex) {
                throw;
            }
            return allUsers;
        }

        public static List<User> GetAllUser() {

            var allUsers = new List<User>();

            string query = $"select * from IDTPUsers";
            //string query = $"select top 1000 * from IDTPUsers";
            try {

                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            var user = new User();
                            user.UserId = Convert.ToInt64(reader["UserId"]);
                            user.FullName = reader["FullName"].ToString();
                            user.VirtualID = reader["VirtualID"].ToString();
                            allUsers.Add(user);
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex) {
                throw;
            }
            return allUsers;
        }

        public static void TransferFundFinalSp(TransactionDTOReq transactionDTO) {
            try {

                string spname = "AddTransaction_V2";
                string SenderAccNo = "22245612345678912", ReceiverAccNo = "33345612345678912", SendingBankRoutingNo = "098764123",
                ReceivingBankRoutingNo = "987543211";
                int senderId = 1, receiverId = 2, SenderBankId = 1, ReceiverBankId = 2;

                //SenderAccNo = transactionDTO.SenderAccNo; ReceiverAccNo = transactionDTO.ReceiverAccNo;
                //SendingBankRoutingNo = "098764"; ReceivingBankRoutingNo = "987543";

                //senderId = transactionDTO.SenderId; receiverId = transactionDTO.ReceiverId;
                //SenderBankId = transactionDTO.SenderBankId; ReceiverBankId = transactionDTO.ReceiverBankId;
                //paymentNote = transactionDTO.PaymentNote; e2eId = transactionDTO.EndToEndID; msgId = transactionDTO.MessageID;
                //refSendingBank = transactionDTO.ReferenceSendingBANK; ipaddress = transactionDTO.IPAddress;
                //latlong = transactionDTO.LatLong; mobile = transactionDTO.MobileNumber;
                
                string IdtpRef = "IDTP" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

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
                    sql_cmnd.Parameters.AddWithValue("@ReferenceSendingBANK", SqlDbType.VarChar).Value = "SBL1REF";
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = IdtpRef;
                    sql_cmnd.Parameters.AddWithValue("@IPAddress", SqlDbType.VarChar).Value = "192.156.98.71";
                    sql_cmnd.Parameters.AddWithValue("@LatLong", SqlDbType.VarChar).Value = "23.8103° N, 90.4125° E";
                    sql_cmnd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = "+8801987456789";
                    sql_cmnd.Parameters.AddWithValue("@TransactionTypeId", SqlDbType.Int).Value = 1;
                    sql_cmnd.Parameters.AddWithValue("@ReferernceNumber", SqlDbType.VarChar).Value = "REF12364564675876";
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
                serviceRequestDTO.ReferenceSendingBANK = payLoads[12];
                serviceRequestDTO.MessageID = payLoads[13];

                string SenderAccNo = "22245612345678912", ReceiverAccNo = "33345612345678912", SendingBankRoutingNo = "098764123",
                ReceivingBankRoutingNo = "987543211";
                int senderId = 1, receiverId = 2, SenderBankId = 1, ReceiverBankId = 2;

                string IdtpRef = "IDTP" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

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
                    sql_cmnd.Parameters.AddWithValue("@ReferenceSendingBANK", SqlDbType.VarChar).Value = "SBL1REF";
                    sql_cmnd.Parameters.AddWithValue("@ReferenceIDTP", SqlDbType.VarChar).Value = IdtpRef;
                    sql_cmnd.Parameters.AddWithValue("@IPAddress", SqlDbType.VarChar).Value = "192.156.98.71";
                    sql_cmnd.Parameters.AddWithValue("@LatLong", SqlDbType.VarChar).Value = "23.8103° N, 90.4125° E";
                    sql_cmnd.Parameters.AddWithValue("@MobileNumber", SqlDbType.VarChar).Value = "+8801987456789";
                    sql_cmnd.Parameters.AddWithValue("@TransactionTypeId", SqlDbType.Int).Value = 1;
                    sql_cmnd.Parameters.AddWithValue("@ReferernceNumber", SqlDbType.VarChar).Value = "REF12364564675876";
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
            catch (Exception ex) {
                throw ex;
            }
        }

        public static void SimpleInsert(string item) {

            string dbConnectionString = "Server=192.168.1.31;Database=TestHelloWorld;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";

            using (SqlConnection con = new SqlConnection(dbConnectionString)) {
                con.Open();
                var commandString = "INSERT INTO TxTable (Value1) VALUES ('" + item + "')";
                SqlCommand cmd = new SqlCommand(commandString, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
