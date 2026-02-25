using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Drivers;

namespace TestFramework.Pages.Faves
{
    public class FavePage(DriverManager driver) : BasePage(driver)
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.FavePageRoute;

    }
}
