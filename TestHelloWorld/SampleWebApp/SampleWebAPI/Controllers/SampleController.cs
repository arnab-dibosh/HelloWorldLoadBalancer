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

        private bool OneInsertDynamicSql(string PortalServer)
        {
            try
            {
                string query = "INSERT INTO Sample" + "(Id,Message,APIServer,PortalServer,CreatedOn) " +
                    "VALUES('" + Guid.NewGuid().ToString() + "', '" + "Success" + "', '" + System.Environment.MachineName +
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
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
