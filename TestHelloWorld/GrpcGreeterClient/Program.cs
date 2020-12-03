using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args) {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:5005", new GrpcChannelOptions {
                HttpHandler = new SocketsHttpHandler {
                    EnableMultipleHttp2Connections = true,
                }

            });
            var client = new Greeter.GreeterClient(channel);

            Console.WriteLine("Client Started");

            for (int i = 0; i < 5000; i++) {

                try {
                    var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "Hello" });
                    Console.WriteLine(reply.Message);

                   await client.SayHelloWithDbOperationAsync( new HelloRequest() { Name = "" });
                }
                catch (Exception) { }
                
            }
        }
    }
}
