using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Shared.Models
{
    public class QuoteResponse
    {
        public Guid UserId { get; set; }
        public string Quote { get; set; }
    }
}
