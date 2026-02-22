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
        private const string AddToCartBtnTemplate = "//p[text()=\"{0}\"]/ancestor::div[contains(@class, ' book-card ')]//button[contains(text(), 'Add to cart')]";

        public List<BookModel> GetAllBooks()
        {
            var cardElements = DriverMgr.FindElements(_bookCards);
            var booksList = new List<BookModel>();

            foreach (var card in cardElements)
            {
                string title = card.FindElement(_titleLocator).Text.Trim();
                string authorRaw = card.FindElement(_authorLocator).Text.Trim(); // "Isaac Asimov • 1951"
                string priceRaw = card.FindElement(_priceLocator).Text.Trim();   // "17.5 €"

                // 1. Separăm Autorul de An
                string author = authorRaw;
                int year = 0;
                if (authorRaw.Contains('•'))
                {
                    var parts = authorRaw.Split('•');
                    author = parts[0].Trim();
                    int.TryParse(parts[1].Trim(), out year);
                }

                // 2. Separăm Prețul de Euro (Măsura de siguranță)
                // Luăm doar prima parte înainte de spațiu sau simbol
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

        /// <summary>
        /// Identifică un card de produs după titlu și apasă butonul "Add to cart".
        /// </summary>
        public void AddBookToCart(string bookTitle)
        {
            // Injectam titlul in template
            string finalXpath = string.Format(AddToCartBtnTemplate, bookTitle);

            // Folosim metoda noastra Click care are deja "WaitForElementToBeClickable" inclus
            DriverMgr.Click(By.XPath(finalXpath));

            TestContext.Out.WriteLine($"[ACTION] Clicked 'Add to cart' for book: {bookTitle}");
        }
    }
}
