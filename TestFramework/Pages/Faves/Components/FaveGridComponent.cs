using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Models;

namespace TestFramework.Pages.Faves
{
    public class FaveGridComponent(DriverManager driver) : BasePage(driver)
    {
        private readonly By _bookCards = By.XPath("//div[@id='card-grid']/div");
        private readonly By _titleLocator = By.ClassName("book-title");
        private readonly By _authorLocator = By.ClassName("book-author");
        private readonly By _priceLocator = By.ClassName("book-price");
        private readonly By _emptyFavesBrowseButton = By.Id("empty-faves-button");
        private const string AddToCartBtnTemplate = "//p[text()=\"{0}\"]/ancestor::div[contains(@class, ' book-card ')]//button[contains(text(), 'Add to cart')]";
        
        private readonly By _emptyFavesTitleByCss = By.CssSelector("h2.empty-faves-title");
        private readonly By _emptyFavesTextByCss = By.CssSelector("p.empty-faves-text");
        private readonly By _emptyFavesIconByCss = By.CssSelector("svg.empty-faves-icon");
        
        private readonly By _emptyFavesTitleByXPath = By.XPath("//h2[@class='empty-faves-title']");
        private readonly By _emptyFavesTextByXPath = By.XPath("//p[@class='empty-faves-text']");
        private readonly By _emptyFavesIconByXPath = By.XPath("//svg[@class='FavoriteBorderIcon']");
        public List<BookModel> GetAllFaveBooks()
        {
            var cardElements = DriverMgr.FindElements(_bookCards);
            var booksList = new List<BookModel>();
            foreach (var card in cardElements)
            {
                string title = card.FindElement(_titleLocator).Text.Trim();
                string authorRaw = card.FindElement(_authorLocator).Text.Trim();
                string priceRaw = card.FindElement(_priceLocator).Text.Trim();

                string author = authorRaw;
                int year = 0;
                if (authorRaw.Contains('•'))
                {
                    var parts = authorRaw.Split('•');
                    author = parts[0].Trim();
                    int.TryParse(parts[1].Trim(), out year);
                }

                string priceClean = priceRaw.Split(' ')[0].Replace("€", "").Replace(",", ".").Trim();

                if (double.TryParse(priceClean, NumberStyles.Any, CultureInfo.InvariantCulture, out double priceValue))
                {
                    booksList.Add(new BookModel
                    {
                        Title = title,
                        Author = author,
                        Year = year,
                        Price = priceValue
                    });
                }
            }
            return booksList;
        }

        public void AddBookToCart(string bookTitle)
        {
            string finalXpath = string.Format(AddToCartBtnTemplate, bookTitle);
            DriverMgr.Click(By.XPath(finalXpath));
            TestContext.Out.WriteLine($"[ACTION] Clicked 'Add to cart' for book: {bookTitle}");
        }

        public void ClickOnBrowse()
        {
            DriverMgr.Click(_emptyFavesBrowseButton);
        }

        public string GetEmptyListMessageTitle()
        {
            return DriverMgr.GetText(_emptyFavesTitleByCss);
        }

        public string GetEmptyListMessageText()
        {
            return DriverMgr.GetText(_emptyFavesTextByCss);
        }

        public bool IsFavesIconVisible()
        {
            return DriverMgr.IsElementVisible(_emptyFavesIconByCss);
        }
    }
   
}
