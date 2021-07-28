using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult SampleInsert(dynamic Payload)
        {
            var isInserted = OneInsertDynamicSql(Payload.ToString());
            return Ok(isInserted);
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
}
