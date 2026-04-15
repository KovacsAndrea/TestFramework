using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Constants
{
    public static class Passwords
    {
        public const string MissingLengthCheck = "Qwert2#";
        public const string MissingLowercaseCheck = "QWERTY2#";
        public const string MissingUpperCaseCheck = "qwerty2#";
        public const string MissingNumberCheck = "Qwertyu#";
        public const string MissingSpecialCharacterCheck = "Qwertyu2";
        public const string ValidPassword = "Qwerty2#";
        public const string DifferentValidPassword = "Wertyu3@";
        public const string EmptyPassrowd = "";
    }
}
