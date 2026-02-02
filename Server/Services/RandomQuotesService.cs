using Grpc.Core;
using Microsoft.Extensions.Logging;
using Server.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcRandomQuotesService
{
    public class RandomQuotesService : Quoter.QuoterBase
    {
        public override Task<QuotesReply> AskForQuotes(QuotesRequest request, ServerCallContext context)
        {
            var rng = new Random();
            var results = Enumerable.Range(1, request.Number).Select(index => new QuoteMessage
            {
                Message = Data.Quotes[rng.Next(Data.Quotes.Length)]
            }).ToArray();

            var response = new QuotesReply();
            response.Quotes.AddRange(results);

            return Task.FromResult(response);
        }
    }
}
