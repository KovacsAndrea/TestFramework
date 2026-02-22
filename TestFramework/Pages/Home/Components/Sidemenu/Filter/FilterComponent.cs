using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;

namespace TestFramework.Pages.Home
{
    public class FilterComponent : BasePage
    {
        private readonly By _lowestPriceBullet = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='0']/input");
        private readonly By _lowestPriceLabel = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='0']/span");
        private readonly By _highestPriceBullet = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='1']/input");
        private readonly By _highestPriceLabel = By.XPath("//span[@id='filter-price-slide-bar']//span[@data-index='1']/span");
        private readonly By _onlyStockCheckbox = By.XPath("//label[@id=\"filter-stock-section\"]/span[1]");
        public FilterComponent(DriverManager driver) : base(driver) { }

        public string GetLowestPrice()
        {
            return DriverMgr.GetText(_lowestPriceLabel);
        }

        public string GetHighestPrice()
        {
            return DriverMgr.GetText(_highestPriceLabel);
        }

        public void IncreateLowestPriceByNPositions(int positions)
        {
            DriverMgr.MoveSliderRight(_lowestPriceBullet, positions);
        }

        public void DecreaseHighestPriceByNPositions(int positions)
        {
            DriverMgr.MoveSliderLeft(_highestPriceBullet, positions);
        }

        public void SetLowestPriceTo(int number)
        {
            DriverMgr.MoveSliderToValue(_lowestPriceBullet, _lowestPriceLabel, number, Keys.ArrowRight);
        }
        public void SetHighestPriceTo(int number)
        {
            DriverMgr.MoveSliderToValue(_highestPriceBullet, _highestPriceLabel, number, Keys.ArrowLeft);
        }

        public void CheckOnlyStock()
        {
            DriverMgr.Click(_onlyStockCheckbox);
        }
    }
}
