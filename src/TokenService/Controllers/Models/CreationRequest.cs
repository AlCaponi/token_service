using System;

namespace TokenService.Controllers.Models
{
    public class CreationRequest 
    {
        public DateTimeOffset Expires { get; set; }

        public Object Content { get; set; }
    }
}
    