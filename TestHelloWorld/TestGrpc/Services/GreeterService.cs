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
           // DBUtility.WriteData(Guid.NewGuid().ToString(), request.Name, "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply {
                Message = "Return Message From Server"
            });
        }

        public override Task<HelloReply> SayHelloWithDbOperation(HelloRequest request, ServerCallContext context) {

            DBUtility.WriteData(Guid.NewGuid().ToString(), request.Name, "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply {
                Message = "Return Message From Server"
            });
        }

        public override Task WriteBiDirectionalDataInDb(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream,
            ServerCallContext context)
        {
            DBUtility.WriteData(Guid.NewGuid().ToString(), "HelloBiDirect", "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply
            {
                Message = "Return Message From Chat Server"
            });
        }

        public override Task WriteBiDirectionalDataWithoutDb(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream,
            ServerCallContext context)
        {
            //DBUtility.WriteData(Guid.NewGuid().ToString(), "HelloBiDirect", "DockerName", "2020-01-01", "2020-01-01");
            return Task.FromResult(new HelloReply
            {
                Message = "Return Message From Chat Server"
            });
        }

        public override async Task WriteServerDataInDb(HelloRequest request,
           IServerStreamWriter<ReturnCount> responseStream,
           ServerCallContext context)
        {
            DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello", "DockerName", "2020-01-01", "2020-01-01");
            var count = 1;
            await responseStream.WriteAsync(new ReturnCount { Count = count });

        }

        public override async Task WriteServerWithoutDB(HelloRequest request,
           IServerStreamWriter<ReturnCount> responseStream,
           ServerCallContext context)
        {
            //DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello", "DockerName", "2020-01-01", "2020-01-01");
            var count = 1;
            await responseStream.WriteAsync(new ReturnCount { Count = count });

        }

        public override async Task TransferFundsNoperation(Custom request, IServerStreamWriter<Custom> responseStream, ServerCallContext context) {
            string response = string.Empty;
            IDTPGateway.IDTPGateway _gateway = new IDTPGateway.IDTPGateway();
            try {
                response = _gateway.TFXMLtoDTOWithSerializerAddTran(request.Stringvalue);
            }
            catch (Exception e) {
                await responseStream.WriteAsync(new Custom { Stringvalue = "Error" });
            }

            await responseStream.WriteAsync(new Custom { Stringvalue = "Sucess" });
        }
    }
}
