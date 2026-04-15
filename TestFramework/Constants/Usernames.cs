using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Constants
{
    public static class Usernames
    {
        public const string UsernameTooShort = "ab"; // 2 caractere

        public const string UsernameMinValid = "abc"; // 3 caractere

        public const string UsernameMaxValid =
            "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVB"; // 50 caractere

        public const string UsernameTooLong =
            "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBN"; // 51 caractere
    }
}
