using ChatJobsity.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Bot.Producers
{
    public interface IProducer
    {
        Task<int> SendQuoteRequest(QuoteRequest quoteRequest);
    }
}
