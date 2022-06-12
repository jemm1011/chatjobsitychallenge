using ChatJobsity.Shared.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Apis
{
    public interface IBotApi
    {
        #region QuoteApi
        [Post("/quote")]
        Task ProcessQuote(QuoteRequest quoteRequest);
        #endregion
    }
}
