using LoginAndina2.Models;
using LoginAndina2.Object;

namespace LoginAndina2
{
    [TestFixture]
    public class EmisionTest
    {
        private DriverChrome chromeDriver;
        private EmisionModel emisionPage;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            chromeDriver = new DriverChrome();
            chromeDriver.SetUp();
            loginPage = new LoginPage(chromeDriver.Driver);
            emisionPage = new EmisionModel(chromeDriver.Driver);
        }

        [Test]
        public async Task TestEmisionPolizaIndividual()
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
                emisionPage.EmisionPol();

                // // Emitir póliza individual con reintentos
                // Console.WriteLine("📄 Emitiendo póliza individual...");
                // emisionPage.EmisionPolOneConReintento();

                Console.WriteLine("🎉 Test completado exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Test falló: {ex.Message}");
                Console.WriteLine($"📋 Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        // [Test]
        // public async Task TestEmisionPolizaMultiple()
        // {
        //     try
        //     {
        //         Console.WriteLine("🚀 Iniciando test de emisión de múltiples pólizas...");

        //         // Login secuencial
        //         await loginPage.LoginSequentialAsync();

        //         // Navegar al menú de emisión
        //         emisionPage.EmisionPol();

        //         // Emitir múltiples pólizas
        //         emisionPage.EmisionPolMas();

        //         Console.WriteLine("🎉 Test de múltiples pólizas completado exitosamente!");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"❌ Test falló: {ex.Message}");
        //         throw;
        //     }
        // }

        // [Test]
        // [TestCase("COT-00002026")]
        // [TestCase("COT-00002027")]
        // [TestCase("COT-00002028")]
        // public async Task TestEmisionPolizaParametrizada(string numeroCotizacion)
        // {
        //     try
        //     {
        //         Console.WriteLine($"🚀 Iniciando test para cotización: {numeroCotizacion}");

        //         await loginPage.LoginSequentialAsync();
        //         emisionPage.EmisionPol();
        //         emisionPage.EmisionPolOne(numeroCotizacion);

        //         Console.WriteLine($"🎉 Test completado para cotización: {numeroCotizacion}");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"❌ Test falló para cotización {numeroCotizacion}: {ex.Message}");
        //         throw;
        //     }
        // }

        // [TearDown]
        // public void TearDown()
        // {
        //     try
        //     {
        //         chromeDriver?.TearDown();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"❌ Error durante el TearDown: {ex.Message}");
        //     }
        // }
    }
}