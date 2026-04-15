using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Models;

namespace TestFramework.Utilities
{
    public static class RandomIdentityGenerator
    {
        private static readonly Random _random = new Random();

        private static readonly HashSet<string> _generatedUsernames = new HashSet<string>();
        private static readonly HashSet<string> _generatedEmails = new HashSet<string>();

        private static readonly string[] Adjectives =
        {
            "brisk","rapid","silent","fuzzy","wild","blue","bright","dark",
            "swift","lucky","magic","solar","icy","frozen","flying","red",
            "green","golden","silver","stormy"
        };

        private static readonly string[] Nouns =
        {
            "tiger","ocean","falcon","river","mountain","forest","shadow",
            "wolf","eagle","comet","galaxy","planet","star","cloud",
            "sun","moon","storm","dragon","phoenix","wind"
        };

        public static string GenerateUsername()
        {
            string username;

            do
            {
                var adj = Adjectives[_random.Next(Adjectives.Length)];
                var noun = Nouns[_random.Next(Nouns.Length)];
                var number = _random.Next(1000, 9999999);

                username = $"{adj}-{noun}-{number}";
            }
            while (_generatedUsernames.Contains(username));

            _generatedUsernames.Add(username);

            return username;
        }

        public static string GenerateEmail()
        {
            string email;

            do
            {
                var adj = Adjectives[_random.Next(Adjectives.Length)];
                var noun = Nouns[_random.Next(Nouns.Length)];
                var number = _random.Next(10000, 99999999);

                email = $"{adj}.{noun}.{number}@gmail.com";
            }
            while (_generatedEmails.Contains(email));

            _generatedEmails.Add(email);

            return email;
        }

        public static User GenerateValidUser()
        {
            return new User
            {
                Email = GenerateEmail(),
                Username = GenerateUsername(),
                Password = Passwords.ValidPassword
            };
        }
    }
}
