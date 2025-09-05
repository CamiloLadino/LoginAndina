using LoginAndina2.Object;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WaitHerlpers.Helpers;

namespace LoginAndina2.Models
{
    public class CotMasiModel
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly WebDriverWait longWait;
        private readonly WaitHelper waitHelper;

        // Timeouts
        private const int TIMEOUT_PREDETERMINADO_SEGUNDOS = 15;
        private const int TIMEOUT_LARGO_SEGUNDOS = 30;

        public CotMasiModel(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_PREDETERMINADO_SEGUNDOS));
            this.longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_LARGO_SEGUNDOS));
            this.waitHelper = new WaitHelper(driver);
        }
        public void IngresarCotizacion()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(cotizacionLocalizador.HomeLocator));
                driver.FindElement(cotizacionLocalizador.MenuCotLocator).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(cotizacionLocalizador.BtonCrearCot));
                driver.FindElement(cotizacionLocalizador.BtonCrearCot).Click();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ingresar cotización: " + ex.Message);
            }

        }


    }


}