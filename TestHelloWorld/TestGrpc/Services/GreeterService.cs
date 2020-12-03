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
        public GreeterService() {
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context) {
            DBUtility.WriteData(Guid.NewGuid().ToString(), request.Name, "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply {
                Message = "Return Message From Server"
            });
        }

        public override Task<Custom> SayHelloWithDbOperation(Custom request, ServerCallContext context) {

            DBUtility.WriteData(Guid.NewGuid().ToString(), request.Stringvalue, "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new Custom {
                Stringvalue = "Return Message From Server"
            });
        }
    }
}
