using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Pages.Auth;
using TestFramework.Pages.Home;

namespace TestFramework.Tests
{
    public class HomeTests : BaseTest
    {
        [Test]
        [TestCase("Harry Potter")]
        public void TrialTest(string searchText)
        {
            var homePage = new HomePage(DriverMgr);
            homePage.Open();

            //homePage.SearchBar.TypeSearchText(searchText);
            //homePage.SearchBar.ClickSearch();

            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnFavorites();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnHomeIcon();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnCart();
            //DriverMgr.Wait(2);
            //homePage.NavBar.clickOnLoggedOutUserAvatar();
            //DriverMgr.Wait(2);
            //homePage.Open();
            //DriverMgr.Wait(2);

            homePage.SideMenu.Filter.DecreaseHighestPriceByNPositions(4);
            DriverMgr.Wait(2);
            homePage.SideMenu.Filter.IncreateLowestPriceByNPositions(5);
            DriverMgr.Wait(2);

            homePage.SideMenu.Filter.CheckOnlyStock();
            DriverMgr.Wait(2);
            homePage.SideMenu.Sorting.Order.CheckAscending();
            DriverMgr.Wait(2);
            homePage.SideMenu.Sorting.Order.CheckDescending();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckYear();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckAuthor();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckPrice();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckTitle();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Criteria.CheckNone();
            DriverMgr.Wait(2);

            homePage.SideMenu.Sorting.Order.CheckNone();
            DriverMgr.Wait(2);
        }

        [Test]
        public void DebugProductList()
        {
            var homePage = new HomePage(DriverMgr);
            homePage.Open();
            var books = homePage.ProductGrid.GetAllBooks();

            foreach (var book in books)
            {
                // TestContext.WriteLine e "sfânt" pentru NUnit
                TestContext.Out.WriteLine($"Carte: {book.Title} | Autor: {book.Author} | An: {book.Year} | Pret: {book.Price} EURO");
            }

            Assert.That(books.Count, Is.GreaterThan(0), "Lista de carti e goala, ceva nu e bine la locatori!");

            homePage.ProductGrid.AddBookToCart("The Name of the Wind");

            DriverMgr.Wait(2);

            homePage.DismissAlert();

            DriverMgr.Wait(2);

            

            var faveNr = homePage.NavBar.GetFaveBadgeCount();
            var cartNr = homePage.NavBar.GetCartBadgeCount();

            TestContext.Out.WriteLine($"FAVE: {faveNr}");
            TestContext.Out.WriteLine($"CART: {cartNr}");

            // Verificăm favoritele
            Assert.That(faveNr, Is.EqualTo(0), $"Eroare: Ne așteptam la 0 favorite, dar am găsit {faveNr}.");

            // Verificăm coșul
            Assert.That(cartNr, Is.EqualTo(0), $"Eroare: Ne așteptam la 0 produse în coș, dar am găsit {cartNr}.");

            homePage.ProductGrid.AddBookToCart("The Name of the Wind");

            DriverMgr.Wait(2);

            homePage.AcceptAlert();

            DriverMgr.Wait(2);
        }

        [Test]
        public void LoginSmokeTest()
        {
            // Instanțiem componenta Login
            AuthPage authPage = new AuthPage(DriverMgr);
            authPage.Open();


            // ===== Typing =====
            authPage.Login.TypeEmail("test@yopmail.com");
            DriverMgr.Wait(1);
            authPage.Login.TypePassword("BB55%%bbanle");
            DriverMgr.Wait(1);

            // ===== Show password =====
            authPage.Login.ClickShowPassword();
            DriverMgr.Wait(1);

            //// ===== Navigation links =====
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);

            authPage.Register.ClickLoginLink();
            DriverMgr.Wait(1);

            authPage.Login.ClickGuestLink();
            DriverMgr.Wait(1);

            HomePage homePage = new HomePage(DriverMgr);
            homePage.NavBar.clickOnLoggedOutUserAvatar();
            DriverMgr.Wait(3);
            authPage.Login.ClickLogin();
            DriverMgr.Wait(3);


            // ===== Verificăm mesajele de eroare =====
            string expectedError = "Acest câmp este obligatoriu!";
            string emailError = authPage.Login.GetEmailErrorMessage();
            string passwordError = authPage.Login.GetPasswordErrorMessage();

            TestContext.Out.WriteLine($"Email Error: {emailError}");
            TestContext.Out.WriteLine($"Password Error: {passwordError}");

            Assert.That(emailError, Is.EqualTo(expectedError), "Mesajul de eroare la email nu corespunde!");
            Assert.That(passwordError, Is.EqualTo(expectedError), "Mesajul de eroare la parola nu corespunde!");

            // ===== Typing =====
            authPage.Login.TypeEmail("test@yopmail.com");
            DriverMgr.Wait(1);
            authPage.Login.TypePassword("BB55%");
            DriverMgr.Wait(1);


            authPage.Login.ClickLogin();
            DriverMgr.Wait(3);

            // ===== Capturăm mesajul global de eroare (dacă există) =====
            string globalError = authPage.Login.GetGlobalErrorMessage();
            if (!string.IsNullOrEmpty(globalError))
            {
                TestContext.Out.WriteLine($"Global Error Message: {globalError}");
            }
        }

        [Test]
        public void RegisterSmokeTest()
        {
            AuthPage authPage = new AuthPage(DriverMgr);
            authPage.Open();
            authPage.Login.ClickRegisterLink();
            DriverMgr.Wait(1);
            authPage.Register.TypePassword("aaa");
            DriverMgr.Wait(3);
            var failMessages = authPage.Register.GetPasswordFailMessages();
            TestContext.Out.WriteLine("Password Fail Messages:");
            foreach (var msg in failMessages)
            {
                TestContext.Out.WriteLine(msg);
            }

            var passMessages = authPage.Register.GetPasswordPassMessages();
            TestContext.Out.WriteLine("Password Pass Messages:");
            foreach (var msg in passMessages)
            {
                TestContext.Out.WriteLine(msg);
            }
        }
    }
}
