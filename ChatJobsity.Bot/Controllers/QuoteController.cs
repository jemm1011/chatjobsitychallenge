using ChatJobsity.Bot.Producers;
using ChatJobsity.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IProducer _producer;
        public QuoteController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessQuote(QuoteRequest quoteRequest)
        {
            var resonse = await _producer.SendQuoteRequest(quoteRequest);
            if (resonse == -1)
            {
                return new BadRequestResult();
            }
            return Ok();
        }
    }
}
