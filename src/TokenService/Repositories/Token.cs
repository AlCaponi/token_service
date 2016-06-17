
using System;
using System.Linq;

namespace TokenService.Repositories
{
    public class Token 
    {
        private static readonly string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static readonly uint TOKEN_SIZE = 64; 

        public string ID { get; set; }

        public DateTimeOffset Expires { get; set; }

        public Object Content { get; set; }

        public static string GenerateID()
        {
            var random = new Random();
            return new string(Enumerable.Repeat(CHARS, (int)TOKEN_SIZE).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }    
}
