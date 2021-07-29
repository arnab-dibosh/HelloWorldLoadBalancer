using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Helper;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SampleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("/SampleInsert", Name = "SampleInsert")]
        public string SampleInsert([FromBody]Payload payload)
        {
            try {
                string recordIdAndHostMachineName = OneInsertSpTran(payload.id, payload.portalServerName);
                return recordIdAndHostMachineName;
            }
            catch (Exception ex) {

                return ex.Message;
            }           
            
        }

        private string OneInsertSpTran(string id, string portalServer) {
            try {

                string apiServerName = Environment.MachineName;
                string recordIdAndHostMachineName = id + "," + apiServerName;

                string ipString = _configuration["ServerIps"].Trim();
                string[] ipListFromAppSettings = ipString.Split(",");
                string connStringName = string.Empty;

                if (Utility.IsServerActive(ipListFromAppSettings[0])) connStringName = "DB1";
                else if (Utility.IsServerActive(ipListFromAppSettings[0])) connStringName = "DB2";
                else if (Utility.IsServerActive(ipListFromAppSettings[0])) connStringName = "DB3";

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(connStringName))) {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("spInsertIntoDB", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = id;
                    sql_cmnd.Parameters.AddWithValue("@portalServer", SqlDbType.VarChar).Value = portalServer;
                    sql_cmnd.Parameters.AddWithValue("@apiServerName", SqlDbType.VarChar).Value = apiServerName;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();
                }
                return recordIdAndHostMachineName;
            }
            catch (Exception) {
                throw;
            }
        }

        private string OneInsertDynamicSql(string PortalServer)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string machineName = Environment.MachineName;

                string recordIdAndHostMachineName = guid + "," + machineName;

                string query = "INSERT INTO Sample" + "(Id,Message,APIServer,PortalServer,CreatedOn) " +
                    "VALUES('" + guid + "', '" + "Success" + "', '" + System.Environment.MachineName +
                    "', '" + PortalServer + "', GetDate() )";

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PrimaryDB")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return recordIdAndHostMachineName;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class Payload
    {
        public string id { get; set; }
        public string portalServerName { get; set; }
    }
}
