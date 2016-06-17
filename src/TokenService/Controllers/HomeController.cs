using Microsoft.AspNetCore.Mvc;
using TokenService.Repositories;
using System;

namespace TokenService.Controllers
{
    public class ValidationRequest {
        public DateTime ValidThrough { get; set; }
        public Object Content { get; set; }
    }

    public class CreationRequest {
        public DateTime ValidThrough { get; set; }
        public Object Content { get; set; }
    }

    [Route("v1/tokens")]
    public class HomeController : Controller
    {
        private readonly ITokenRepository repository;
        public HomeController(ITokenRepository repo){
            repository = repo;
        }

        [HttpGet("{tokenid}")]
        public IActionResult GetToken(string tokenid)
        {
            return Ok(new {id = tokenid, content = "this could by anything!!", validThrough = DateTime.UtcNow });
        }

        [HttpHead("{tokenid}")]
        public IActionResult CheckToken()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult CreateToken([FromBody] CreationRequest request)
        {
            return Ok(new {id = "somerandomstring", validThrough = DateTime.UtcNow.AddDays(10), content_type =  request?.Content?.GetType(), content = request.Content });
        }

        [HttpPost("{tokenid}")]
        public IActionResult ValidateToken(string tokenid, [FromBody] ValidationRequest request)
        {
            return Ok(new {id = "somerandomstring", whatHappened = "Token was validated!", validThrough = request.ValidThrough.AddDays(10) });
        }
    }
}