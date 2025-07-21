using LoginAndina2;
using OpenQA.Selenium;


namespace LoginAndina2.Models;

public class LoginPage : DriverChrome
{

    private LoginL loginL = null!; // Indica que será inicializado después

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        this.loginL = new LoginL(driver);

    }

    public async Task LoginSequentialAsync()
    {
        try
        {

            //Ingresar credenciales (esto debería triggerar el envío del OTP)
            Console.WriteLine("🔑  credenciales.Ingresando..");
            loginL.IngresarUser();

            //Ahora SÍ iniciar el monitoreo y procesamiento del OTP
            Console.WriteLine("🔍 Procesando OTP...");
            await loginL.IngresarOtp();

            Console.WriteLine("✅ Login completado exitosamente");

            //Verificar que el login fue exitoso
            ///Thread.Sleep(500);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error durante el login: {ex.Message}");
            throw;
        }
    }
}
