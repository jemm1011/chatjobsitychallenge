using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Shared.Models
{
    public class QuoteRequest
    {
        public Guid UserId { get; set; }
        public string QuoteCode { get; set; }
    }
}
