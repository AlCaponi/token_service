using System;
using TokenService.Repositories;


namespace TokenService.Tokens
{
    public static class TokenExtensions {
        public static void Prolong(this Token subject){
            subject.Expires = DateTimeOffset.UtcNow + new TimeSpan(1, 0, 0);
        }
    }
}