# gRPC.RandomQuotes

A sample of basic gRPC Client that communicates with a gRPC Server with .NET Core.

In this sample, a gRPC client requests with a numeric parameter to a gRPC server in order to get several random quotes. The number of quotes received is that indicated in the numerical parameter.

If you are brand new to gRPC on .NET a good place to start is the getting started tutorial: [Create a gRPC client and server in ASP.NET Core](https://docs.microsoft.com/aspnet/core/tutorials/grpc/grpc-start)

## [Proto](./Proto)

Directory with the .proto file(s) (Protocol Buffers) shared between the gRPC Client and the gRPC Server.

## [Client](./Client)

A console client sample that uses unary (non-streaming) gRPC method in .NET Core to call the gRPC Server.

## [Server](./Server)

A gRPC server in ASP.NET Core that serves a certain number of random quotes. The server reacts to messages sent from the client.
