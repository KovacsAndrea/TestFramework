using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Constants
{
    public static class Emails
    {
        public const string InvalidFormat = "mock-email";
        public const string MissingTopLevelDomain = "mock@something";
        public const string InvalidDomainFormat = "mock.something@anythingelse";
    }
}
