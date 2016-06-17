using System;

namespace TokenService.Controllers.Models
{
    public class TokenResponse
    {
        public string ID { get; set; }

        public DateTimeOffset Expires { get; set; } 
    }
}