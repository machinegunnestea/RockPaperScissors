using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Crypto
    {
        public static byte[] GenerateKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return key;
            }
        }

        public static byte[] GenerateHMAC(byte[] key, byte[] message)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(message);
            }
        }
    }
}
