using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGrpc
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger) {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {

            DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello", "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply {
                Message = "Return Message From Server"
            });
        }

    }
}
