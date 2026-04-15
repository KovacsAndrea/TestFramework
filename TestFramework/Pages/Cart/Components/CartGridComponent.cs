using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Models;

namespace TestFramework.Pages.Cart.Components
{
    public class CartGridComponent(DriverManager driver): BasePage(driver)
    {
        private readonly By _cartBooks = By.XPath("//div[@id='cart-grid']/div");
        private readonly By _titleLocator = By.Id("cart-card-title");
        private readonly By _authorLocator = By.Id("cart-card-author");
        private readonly By _stockLocator = By.Id("cart-card-stock");
        private readonly By _priceLocator = By.Id("cart-card-price");
        private readonly By _favoriteButton = By.CssSelector("#cart-card-favorite-button");
        private readonly By _deleteButton = By.Id("cart-card-delete-button");
        private readonly By _qtIncreaseButton = By.Id("cart-card-qt-increase-button");
        private readonly By _qtDecreaseButton = By.Id("cart-card-qt-decrease-button");
        private const string AddToFavoritesBtnTemplate =
            "//p[text()='{0}']/ancestor::div[contains(@id,'cart-card')]//button[contains(@id,'cart-card-favorite-button')]";

        public List<Book> GetAllBooks()
        {
            var cardElements = DriverMgr.FindElements(_cartBooks);
            var booksList = new List<Book>();

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
                    booksList.Add(new Book
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

        public string ClickFavoriteIconOnProduct(string bookTitle)
        {
            string finalXpath = string.Format(AddToFavoritesBtnTemplate, bookTitle);
            DriverMgr.Click(By.XPath(finalXpath));
            return $"[ACTION] Added book to favorites: {bookTitle}";
        }

        public string ClickFavoriteOnNthProduct(int n)
        {
            var cards = DriverMgr.FindElements(_cartBooks).ToList();
            TestContext.Out.WriteLine(cards);
            TestContext.Out.WriteLine(cards.Count);
            if (n < 1 || n > cards.Count)
                throw new ArgumentOutOfRangeException(nameof(n), $"There are only {cards.Count} books in the grid.");

            var card = cards[n - 1];

            card.FindElement(_favoriteButton).Click();
            return $"[ACTION] Added book #{n} to favorites: {card.FindElement(_titleLocator).Text}";
        }
    }
}
