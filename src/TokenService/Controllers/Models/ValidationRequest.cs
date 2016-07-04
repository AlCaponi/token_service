using System;
namespace TokenService.Controllers.Models
{
    public class ValidationRequest 
    {
        public DateTimeOffset Expires { get; set; }

        public Object Content { get; set; }
    }
}