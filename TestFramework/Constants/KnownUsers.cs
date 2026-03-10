using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Models;

namespace TestFramework.Constants
{
    public static class KnownUsers
    {
        public static User UserWithFavorites = new User
        {
            Email = "favorites_user@yopmai.com",
            Username = "Favorites User",
            Password = "AAaa22@@"
        };
        public static User UserWithNoFavorites = new User
        {
            Email = "no_favorites_user@yopmail.com",
            Username = "No Favorites User",
            Password = "AAaa22@@"
        };

    }
}
