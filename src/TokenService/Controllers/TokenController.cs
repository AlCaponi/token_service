using System;
using Microsoft.AspNetCore.Mvc;
using TokenService.Repositories;
using TokenService.Tokens;
using TokenService.Controllers.Models;
using Microsoft.Extensions.Logging;

namespace TokenService.Controllers
{
    [Route("v1/tokens")]
    public class TokenController : Controller
    {
        private readonly ITokenRepository _repository;

        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenRepository repo,  ILogger<TokenController> logger){
            _repository = repo;
            _logger = logger;
        }

        [HttpGet("{tokenid}")]
        public IActionResult GetToken(string tokenid)
        {
            if(_repository.ContainsKey(tokenid)){
                var token = _repository[tokenid];
                return Ok(new TokenResponse { ID = token.ID, Expires = token.Expires, Content = token.Content});
            }

            return NotFound();
        }

        [HttpHead("{tokenid}")]
        public IActionResult CheckToken(string tokenid)
        {
            if(_repository.ContainsKey(tokenid)
                &&  _repository[tokenid].Expires > DateTimeOffset.UtcNow){
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public IActionResult CreateToken([FromBody] CreationRequest request)
        { 
            _logger.LogInformation($"Entered Method to create a Token", "Token Creation");

            var newToken = new Token
            {
                ID = Token.GenerateID(),
                Expires = DateTimeOffset.Now.AddHours(1),
                Content = request.Content  
            };

            _logger.LogInformation($"Created a new Token with ID {newToken.ID}", "Token Creation");
            
            _repository.Add(newToken.ID, newToken);

            return Ok(
                        new TokenResponse
                        {
                            ID = newToken.ID,
                            Expires = newToken.Expires
                        }
                    );
        }

        [HttpPost("{tokenid}")]
        public IActionResult ValidateToken(string tokenid, [FromBody] ValidationRequest request)
        {
            Token subject = null;
            if(_repository.TryGetValue(tokenid, out subject)){
                subject.Prolong();
                subject.Content = request.Content;
                return Ok();
            }

            return NotFound();
        }
    }
}