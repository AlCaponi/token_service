using System;
namespace TokenService.Controllers.Models
{
    public class ValidationRequest 
    {
        public DateTimeOffset ValidThrough { get; set; }

        public Object Content { get; set; }
    }
}