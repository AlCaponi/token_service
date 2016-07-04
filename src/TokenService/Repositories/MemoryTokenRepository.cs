using System.Collections.Generic;

namespace TokenService.Repositories
{
    public class MemoryTokenRepository : Dictionary<string, Token>,  ITokenRepository
    {
        
    }
}