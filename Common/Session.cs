using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// Session is supposed to work as a way to give every user a unique ID for every session (a new session is made whenever a user establish a connection to the server)
    /// </summary>
    class Session
    {
        public int Id { get; set; } // Randomized 4-byte ID

        public Session()
        {
            
        }

        public void CreateId()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] value = new byte[4];
            provider.GetBytes(value);
            Id = BitConverter.ToInt32(value, 0);
            if(Id < 0 )
            {
                Id *= -1;
            }
        }

    }
}
