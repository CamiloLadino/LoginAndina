using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using LoginAndina2.Models;
using LoginAndina2.Helpers;
using System.Globalization;
using LoginAndina2;
using SeleniumExtras.WaitHelpers;


namespace CoreAndina.Tests
{
    class TestCreacionCotizacion
    {
        private IWebDriver driver;
        private DriverChrome chromeDriver;
        private WebDriverWait wait;
        private CreacionCotizacion creacionCotizacion;
        private CrearCotizacionCausante cotizacionCausante;
        private CrearCotizacionBeneficiario cotizacionBeneficiario;
        private LoginPage loginPage;
        private DatosCausante CausanteDatos;


        [TearDown]
        public void TearDown()
        {
            chromeDriver.Driver.Quit();
        }

        [SetUp]
        public void SetUp()
        {
            chromeDriver = new DriverChrome();
            chromeDriver.SetUp();
            wait = new WebDriverWait(chromeDriver.Driver, TimeSpan.FromSeconds(10));
            creacionCotizacion = new CreacionCotizacion(chromeDriver.Driver, wait);
            cotizacionCausante = new CrearCotizacionCausante(chromeDriver.Driver, wait);
            loginPage = new LoginPage(chromeDriver.Driver);
            cotizacionBeneficiario = new CrearCotizacionBeneficiario(chromeDriver, wait);

            // Inicializar los datos del causante antes de cada test
            CausanteDatos = LoginAndina2.Models.CausanteDatos.causanteDatos();
        }
        [Test]
        public async Task Test_CrearCotizacion()
        {


            await loginPage.LoginSequentialAsync();
            Console.WriteLine("Login exitoso");
            await cotizacionCausante.Test_Fase2_CrearCotizacion();
            await cotizacionBeneficiario.EjecutarCotizacionBeneficiario();

            // ----- Buscar mensaje de error primero, si no existe buscar mensaje de éxito -----
            string xpathError = "//div[@class='errores-container']//div[contains(@class, 'error-text')]"; // <-- Cambia este XPath por el de tu modal de error
            string xpathExito = "//div[text()='Se creó la cotización correctamente']"; // <-- Cambia este XPath por el de tu mensaje de éxito

            string mensaje = "";
            bool errorEncontrado = false;

            try
            {
                var modalError = wait.Until(driver =>
                {
                    try
                    {
                        var elem = driver.FindElement(By.XPath(xpathError));
                        return elem.Displayed ? elem : null;
                    }
                    catch (NoSuchElementException) { return null; }
                });
                if (modalError != null)
                {
                    mensaje = modalError.Text;
                    errorEncontrado = true;
                    Console.WriteLine("❌ Error: " + mensaje);
                }
            }
            catch (WebDriverTimeoutException)
            {
                // Si no hay error, buscar el mensaje de éxito
               string causanteID = cotizacionCausante.idusado;
                var idCausante = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(
                    $"//tbody/tr/td[count(//thead/tr/th[normalize-space()='Número id. causante']/preceding-sibling::th) + 1][normalize-space() = '{causanteID}']")));
                Console.WriteLine($"ID esperado: {causanteID} | ID en tabla: {idCausante.Text}");
                StringAssert.Contains(causanteID, idCausante.Text);
                Console.WriteLine("Cotizacion creada exitosamente");

            }
            catch (NoSuchElementException) { }
            System.Threading.Thread.Sleep(200);

        }

    }
}


            




        
    

