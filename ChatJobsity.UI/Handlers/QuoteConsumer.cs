using ChatJobsity.Shared.Models;
using ChatJobsity.UI.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Handlers
{
    public class QuoteConsumer : IConsumer<QuoteResponse>
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public QuoteConsumer(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<QuoteResponse> context)
        {
            if (!string.IsNullOrEmpty(context.Message?.Quote))
            {
                await _hubContext.Clients.User(context.Message.UserId.ToString()).SendAsync("Receive", context.Message.Quote, true);
                await context.RespondAsync<QuoteAccepted>(new QuoteAccepted
                {
                    IsSuccess = true
                });
                return;
            }
            await context.RespondAsync<QuoteRejected>(new QuoteRejected
            {
            });
            return;

        }
    }
}
