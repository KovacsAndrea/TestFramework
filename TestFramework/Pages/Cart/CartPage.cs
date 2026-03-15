using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Constants;
using TestFramework.Drivers;
using TestFramework.Pages.Cart.Components;

namespace TestFramework.Pages.Cart
{
    public class CartPage(DriverManager driver): BasePage(driver)
    {
        private readonly string _basePath = AppRoutes.LocalPath + AppRoutes.CartPageRoute;
        private CartGridComponent _cartGrid = null!;
        private SummaryComponent _summary = null!;

        public CartGridComponent CartGrid => _cartGrid ??= new CartGridComponent(DriverMgr);
        public SummaryComponent Summary => _summary ??= new SummaryComponent(DriverMgr);
        public void Open()
        {
            DriverMgr.GoToUrl(_basePath);
        }
    }
}
