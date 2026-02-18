using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Homepage.HompageComponents.SidemenuComponent.FilterComponent
{
    internal class FilterComponent : BasePage
    {
        private readonly By _priceSlideBar = By.Id("filter-price-slide-bar");
        private readonly By _lowestPriceBullet = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='0']/input");
        private readonly By _lowestPriceLabel = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='0']/span");
        private readonly By _highestPriceBullet = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='1']/input");
        private readonly By _highestPriceLabel = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='1']/span");
        private readonly By _onlyStockCheckbox = By.Id("filter-only-stock-checkbox");
        public FilterComponent(DriverManager driver) : base(driver) { }

        public string GetLowestPrice()
        {
            return "";
        }

        public string GetHighestPrice()
        {
            return "";
        }

        public void DragLowestPrice()
        {
            //nu stiu cum sa fac aici. practic la lowest price bullet se poate da drag la dreapta
            //any way in care o putem face per unitate? sliderul in sine are unitati de inturi 
            //sau any way in care putem face sa il tragem la o anumita valoare?
        }

        public void DragHighestPrice()
        {
            //la fel se poate trage doar catre stanga 
        }

        public void CheckOnlyStock()
        {
            DriverMgr.Click(_onlyStockCheckbox);
        }
    }
}
