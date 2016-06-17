using System.Collections.Generic;

namespace TokenService.Repositories
{
    public interface ITokenRepository : IDictionary<string, Token>
    {
        
    }
}