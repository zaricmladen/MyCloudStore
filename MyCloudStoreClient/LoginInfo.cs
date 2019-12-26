using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Algorithms;

namespace MyCloudStoreClient
{
    class LoginInfo
    {

        private static string userID = null;

        public static string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public static string computeHash()
        {
            TigerHash hash = new TigerHash();
            string hashValue = null;
            hashValue = Convert.ToBase64String(hash.Crypt(Encoding.Unicode.GetBytes(UserID)));
            return hashValue;
        }
    }
}
