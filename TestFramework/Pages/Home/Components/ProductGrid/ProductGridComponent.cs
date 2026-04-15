using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using TestFramework.Models;

namespace TestFramework.Pages.Home
{
    public class ProductGridComponent(DriverManager driver) : BasePage(driver)
    {
        // Locatorul pentru cardul intreg (parintele)
        private readonly By _bookCards = By.XPath("//div[@id='card-grid']/div");

        // Locatori relativi (folosim clasele pe care le-ai gasit)
        private readonly By _titleLocator = By.ClassName("book-title");
        private readonly By _authorLocator = By.ClassName("book-author");
        private readonly By _priceLocator = By.ClassName("book-price");
        private const string AddToCartBtnTemplate = 
            "//p[text()=\"{0}\"]/ancestor::div[contains(@class, ' book-card ')]//button[contains(text(), 'Add to cart')]";
        private const string AddToFavoritesBtnTemplate =
            "//p[text()=\"{0}\"]/ancestor::div[contains(@class,'book-card')]//button[contains(@class,'favorite') or contains(@aria-label,'favorite')]";
        private readonly By _favoriteButton = By.XPath(".//button[contains(@class,'favorite') or contains(@aria-label,'favorite')]");
        private readonly By _addToCartButton = By.XPath(".//button[contains(text(),'Add to cart')]");
        public List<Book> GetAllBooks()
        {
            var cardElements = DriverMgr.FindElements(_bookCards);
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

        public string AddBookToCart(string bookTitle)
        {
            string finalXpath = string.Format(AddToCartBtnTemplate, bookTitle);
            DriverMgr.Click(By.XPath(finalXpath));
            return $"[ACTION] Clicked 'Add to cart' for book: {bookTitle}";
        }

        public string AddNthBookToCart(int n)
        {
            var cards = DriverMgr.FindElements(_bookCards).ToList(); 
            if (n < 1 || n > cards.Count)
                throw new ArgumentOutOfRangeException(nameof(n), $"There are only {cards.Count} books in the grid.");

            var card = cards[n - 1]; 

            card.FindElement(_addToCartButton).Click();

            return $"[ACTION] Added book #{n} to cart: {card.FindElement(_titleLocator).Text}";
        }

        public string ClickFavoriteIconOnProduct(string bookTitle)
        {
            string finalXpath = string.Format(AddToFavoritesBtnTemplate, bookTitle);
            DriverMgr.Click(By.XPath(finalXpath));

            return $"[ACTION] Added book to favorites: {bookTitle}";
        }

        public string ClickFavoriteOnNthProduct(int n)
        {
            var cards = DriverMgr.FindElements(_bookCards).ToList();

            if (n < 1 || n > cards.Count)
                throw new ArgumentOutOfRangeException(nameof(n), $"There are only {cards.Count} books in the grid.");

            var title = cards[n - 1].FindElement(_titleLocator).Text;

            cards[n - 1].FindElement(_favoriteButton).Click();

            return $"[ACTION] Added book #{n} to favorites: {title}";
        }
    }
}
