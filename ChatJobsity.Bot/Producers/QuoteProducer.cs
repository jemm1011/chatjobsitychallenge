using ChatJobsity.Shared.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatJobsity.Bot.Producers
{
    public class QuoteProducer : IProducer
    {
        private readonly IBus _bus;
        private readonly IRequestClient<QuoteResponse> _requestClient;
        public QuoteProducer(IBus bus, IRequestClient<QuoteResponse> requestClient)
        {
            _bus = bus;
            _requestClient = requestClient;
        }

        public async Task<int> SendQuoteRequest(QuoteRequest quoteRequest)
        {
            HttpClient client = new HttpClient();
            var csv = await client.GetStringAsync($"https://stooq.com/q/l/?s={quoteRequest.QuoteCode}&f=sd2t2ohlcv&h&e=csv");
            if (string.IsNullOrEmpty(csv))
            {
                return -1;
            }
            var csvValues = csv
            .Split(Environment.NewLine.ToCharArray())
            .Skip(1)
            .ToArray()
            .Except(new List<string> { string.Empty })
            .First()
            .Split(",");
            var quote = $"{csvValues[0]} quote is ${csvValues[4]} per share";

            var response = await _requestClient.GetResponse<QuoteAccepted, QuoteRejected>(
                new QuoteResponse
                {
                    UserId = quoteRequest.UserId,
                    Quote = quote
                });

            Response<QuoteAccepted> acceptedResponse;
            if (response.Is(out acceptedResponse))
            {
                if (acceptedResponse.Message.IsSuccess)
                {
                    return 1;
                }
            }
            return -1;

        }


    }
}
