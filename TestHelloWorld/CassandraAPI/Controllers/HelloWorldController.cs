using Cassandra;
using CassandraAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.Controllers
{
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpPost("/WriteDataPayloadCassandra", Name = "WriteDataPayloadCassandra")]
        public string WriteDataPayloadCassandra([FromBody] SimplePayload payload)
        {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                string createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                var cluster = Cluster.Builder()
                     .AddContactPoints("192.168.1.13") //159.89.207.171
                     .WithPort(9042)
                     .Build();
                // Connect to the nodes using a keyspace
                var session = cluster.Connect("idtp");
                // Execute a query on a connection synchronously
                //session.Execute("INSERT INTO customer (name, email, phone) VALUES('maruf', 'maruf@gmail.com', '01877188432')");
                string sql = "INSERT INTO LoadBalancer (TransactionId, Message, DockerName, ClientRequestTime, ApiRequestStartTime, CreateTime)" +
                    "VALUES('" + payload.transactionId + "', '" + "Hello-World-Cassandra" + "', '" + strDockerName +
                "', '" + payload.clientRequestTime +
                "', '" + apiRequestStartTime +
                "', '" + createTime +
                "');";
                session.Execute(sql);
                //var rs = session.Execute("select * from customer");
                //// Iterate through the RowSet
                //Console.WriteLine("Reading data from Cassandra");
                //foreach (var row in rs)
                //{
                //    var value = row.GetValue<string>("name");
                //    Console.WriteLine(row.GetValue<string>(0) + "|" + row.GetValue<string>(1) + "|" + row.GetValue<string>(2));
                //    // Do something with the value
                //}


                return "Insert Successfull";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
