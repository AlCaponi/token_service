using System;
using System.Net;
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

        [HttpGet("{tokenId}")]
        public IActionResult GetToken(string tokenId)
        {
            if(_repository.ContainsKey(tokenId)){
                var token = _repository[tokenId];
                return Ok(new TokenResponse { ID = token.ID, Expires = token.Expires, Content = token.Content});
            }

            return NotFound();
        }

        [HttpHead("{tokenId}")]
        public IActionResult CheckToken(string tokenId)
        {
            if(_repository.ContainsKey(tokenId)
                &&  _repository[tokenId].Expires > DateTimeOffset.UtcNow){
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

        [HttpPost("{tokenId}")]
        public IActionResult ValidateToken(string tokenId, [FromBody] ValidationRequest request)
        {
            Token subject = null;
            if(_repository.TryGetValue(tokenId, out subject)){
                subject.Prolong();
                subject.Content = request.Content;
                return Ok();
            }

            return NotFound();
        }


        [HttpDelete("{tokenId}")]
        public IActionResult DeleteToken(string tokenId)
        {
            if(_repository.ContainsKey(tokenId)){
                _repository.Remove(tokenId);
                return Ok();
            }
            
            return StatusCode((int)HttpStatusCode.Gone);
        }
    }
}