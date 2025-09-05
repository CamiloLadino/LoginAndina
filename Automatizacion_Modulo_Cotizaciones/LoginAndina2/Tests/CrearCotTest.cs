using LoginAndina2.Models;
using LoginAndina2.Object;
using OpenQA.Selenium;

namespace LoginAndina2
{
    [TestFixture]
    public class CrearCotTests
    {
        private DriverChrome chromeDriver;
        private CotMasiModel cotizacionL;
        private LoginPage loginPage;
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            chromeDriver = new DriverChrome();
            chromeDriver.SetUp();
            loginPage = new LoginPage(chromeDriver.Driver);
            cotizacionL = new CotMasiModel(chromeDriver.Driver);
        }


        [Test]
        public async Task CrearCotMasAsync()
        {
             try
            {
                Console.WriteLine("🚀 Iniciando test de emisión de póliza individual...");

                // Login secuencial
                Console.WriteLine("🔑 Ejecutando login...");
                await loginPage.LoginSequentialAsync();
                Console.WriteLine("✅ Login completado exitosamente");

                // Navegar al menú de emisión
                Console.WriteLine("📋 Navegando al menú de emisión...");
                cotizacionL.IngresarCotizacion();

                Console.WriteLine("🎉 Test completado exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test falló: {ex.Message}");
                Console.WriteLine($"📋 Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
