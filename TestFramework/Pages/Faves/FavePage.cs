using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Drivers;
using TestFramework.Pages.Faves.Components;
using TestFramework.Pages.Home;

namespace TestFramework.Pages.Faves
{
    public class FavePage(DriverManager driver) : BasePage(driver)
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.FavePageRoute;
        private FaveGridComponent _productGrid = null!;
        private SideMenuComponent _sideMenu = null!;
        public FaveGridComponent ProductGrid => _productGrid ??= new FaveGridComponent(DriverMgr);
        public SideMenuComponent SideMenu => _sideMenu ??= new SideMenuComponent(DriverMgr);  
        public void Open()
        {
            DriverMgr.GoToUrl(_basePath);
        }

    }
}
