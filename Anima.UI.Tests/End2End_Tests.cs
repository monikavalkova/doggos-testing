using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

/* HOW TO DO E2E:
cd Anima.UI.Tests
dotnet add package Selenium.WebDriver
//write test
cd ../
dotnet run --project Anima... (all related to the functionality)
dotnet test Anima.UI.Tests
//write implementation
*/

namespace Anima.UI.Tests
{
    public class E2E_Tests : IDisposable
    {
        IWebDriver WebDriver = null;

        public E2E_Tests()
        {
            WebDriver = new ChromeDriver();
        }
        
        
        public void Dispose()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
                WebDriver = null;
            }
        }

        [Fact]
        public void should_show_pets_page()
        {
            //Act
            WebDriver.Navigate().GoToUrl($"http://localhost:6001/pets");

            //Assert
            var mainHeader = WebDriver.FindElement(By.Id("mainHeader"));
            Assert.Equal("All Pets", mainHeader.Text);            
        }
    }
}
