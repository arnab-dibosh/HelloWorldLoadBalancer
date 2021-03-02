using Helper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Helper
{
    public class SqlDBUtility
    {

        public static string ConnectionString = "Server=59.152.61.37,11072;Database=IDTPDevDB;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";
        //public static string ConnectionString = "Server=192.168.1.32;Database=IDTPDevDB;User ID=sa;password=Techvision123@;Pooling=true;Max Pool Size=300;";

        public static User GetUserByVid(string vid) {
            var user = new User();

            string query = $"select UserId, VirtualID, FullName, DefaultFI, IDTP_PIN, SecretSalt, DeviceId, ChannelName, IsRestricted, IsDeviceRestricted, FiUserData from IDTPUsersInMemory where VirtualID='{vid}'";
            try {

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(query, connection)) {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            user.UserId = Convert.ToInt64(reader["UserId"]);
                            user.VirtualID = reader["VirtualID"].ToString();
                            user.FullName = reader["FullName"].ToString();
                            user.DefaultFI = Convert.ToInt64(reader["DefaultFI"]);
                            user.IDTP_PIN = reader["IDTP_PIN"].ToString();
                            user.SecretSalt = reader["SecretSalt"].ToString();
                            user.DeviceId = reader["DeviceId"].ToString();
                            user.ChannelName = reader["ChannelName"].ToString();
                            user.IsDeviceRestricted = Convert.ToInt32(reader["IsDeviceRestricted"]);
                            user.IsRestricted = Convert.ToInt32(reader["IsRestricted"]);
                            user.FiUserData = reader["FiUserData"].ToString();
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

        public static Int64 GetBankId(string SwiftCode)
        {
            Int64 BankId = 0;
            string query = $"select FiUserId from FinancialInstitution where SwiftCode='{SwiftCode}'";

            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            BankId = Convert.ToInt64(reader["FiUserId"]);
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return BankId;
        }

        public static void InsertTransaction(TransactionDTOReqCSV transactionDTO) {
            try {
               

                using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Insert Query", connection);                   
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
