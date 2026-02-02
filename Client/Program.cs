using Client.Extensions;
using Grpc.Net.Client;
using GrpcRandomQuotesService;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WriteHeader();
            WriteMenu();

            string input = Console.ReadLine();

            while (input.ToUpper() != ConsoleKey.X.ToString())
            {
                var isValidNumber = true;
                var number = default(int);

                if (input.IsNumber())
                {
                    number = Convert.ToInt32(input);
                    if (number.IsBetween(1, 10) == false)
                    {
                        WriteMassage("ERROR: You must write a number between 1 and 10.");
                        isValidNumber = false;
                    }
                }
                else
                {
                    WriteMassage("ERROR: You must write a number.");
                    isValidNumber = false;
                }

                if (isValidNumber)
                {
                    var quotes = await GetRandomQuotesAsync(number);

                    WriteMassage("Random Quotes:");
                    WriteMassage("==============");
                    for (int i = 0; i < quotes.Length; i++)
                    {
                        WriteMassage($"{i+1}. {quotes[i]}");
                    }
                }

                WriteMenu();

                input = Console.ReadLine();
            };

            WriteShutDown();
        }

        private async static Task<string[]> GetRandomQuotesAsync(int number)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Quoter.QuoterClient(channel);

            var httpClient = new HttpClient();
            // The port number(5001) must match the port of the gRPC server.
            httpClient.BaseAddress = new Uri("http://localhost:5001");
            var reply = await client.AskForQuotesAsync(new QuotesRequest { Number = number });

            return reply.Quotes
                        .Select(quote => quote.Message)
                        .ToArray();
        }

        private static void WriteHeader()
        {
            Console.WriteLine("+-----------------------+");
            Console.WriteLine("|  gRPC Random Quotes   |");
            Console.WriteLine("+-----------------------+");
            Console.WriteLine("");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.WriteLine("");
        }

        private static void WriteMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("> Write a number between 1 and 10");
            Console.WriteLine("> Or");
            Console.WriteLine("> Press x to Exit.");
        }

        private static void WriteShutDown()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Shutting Down");
        }

        private static void WriteMassage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
