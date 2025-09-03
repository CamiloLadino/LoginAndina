using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V138.Network;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndina2
{
    public class LoginC
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginC(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public async Task obtenerOTP()
        {

        }
       
        
        
        
        
        
        public async Task Login()
        {
            Console.WriteLine("🔍 [LOGIN] Iniciando proceso de login...");
            // --- Configurar DevTools Network ---
            DevToolsSession devToolsSession = ((ChromeDriver)driver).GetDevToolsSession();
            var devToolsDomains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains>();
            await devToolsDomains.Network.Enable(new EnableCommandSettings());
            Console.WriteLine("🔍 [LOGIN] DevTools configurado");

            // Esperar el OTP en segundo plano
            var tcsOtp = new TaskCompletionSource<string>();
            var otpRequestIds = new HashSet<string>();

            devToolsDomains.Network.ResponseReceived += async (sender, e) =>
            {
                Console.WriteLine($"↪️ {e.Response.Url}");
                if (e.Response.Url.Contains("api/v1/otp/send-otp"))
                {
                    Console.WriteLine($"🎯 Response OTP detectado: {e.Response.Url} - RequestId: {e.RequestId}");
                    
                    // Intentar obtener el OTP inmediatamente
                    try
                    {
                        var body = await devToolsDomains.Network.GetResponseBody(new GetResponseBodyCommandSettings { RequestId = e.RequestId });
                        string jsonText = body.Base64Encoded ? Encoding.UTF8.GetString(Convert.FromBase64String(body.Body)) : body.Body;
                        Console.WriteLine($"📥 Respuesta OTP inmediata: {jsonText}");
                        var json = JObject.Parse(jsonText);
                        var otp = json["otp"]?.ToString();
                        if (!string.IsNullOrEmpty(otp))
                        {
                            tcsOtp.TrySetResult(otp);
                            Console.WriteLine($"🔔 OTP capturado inmediatamente: {otp}");
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ No se encontró campo 'otp' en respuesta inmediata");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error al obtener OTP inmediatamente: {ex.Message}");
                        // Si falla, agregar a la cola para procesar en LoadingFinished
                        otpRequestIds.Add(e.RequestId);
                    }
                }
            };


            devToolsDomains.Network.LoadingFinished += async (sender, e) =>
            {
                Console.WriteLine($"🔄 LoadingFinished para RequestId: {e.RequestId}");
                if (otpRequestIds.Contains(e.RequestId))
                {
                    Console.WriteLine($"🎯 Procesando response OTP para RequestId: {e.RequestId}");
                    try
                    {
                        var body = await devToolsDomains.Network.GetResponseBody(new GetResponseBodyCommandSettings { RequestId = e.RequestId });
                        string jsonText = body.Base64Encoded ? Encoding.UTF8.GetString(Convert.FromBase64String(body.Body)) : body.Body;
                        Console.WriteLine($"📥 Respuesta OTP recibida: {jsonText}");
                        var json = JObject.Parse(jsonText);
                        var otp = json["otp"]?.ToString();
                        if (!string.IsNullOrEmpty(otp))
                        {
                            tcsOtp.TrySetResult(otp);
                            Console.WriteLine($"🔔 OTP capturado exitosamente: {otp}");
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ No se encontró campo 'otp' en la respuesta");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error al obtener cuerpo OTP: {ex.Message}");
                    }
                }
            };

            // --- Flujo UI ---
            Console.WriteLine("🔍 [LOGIN] Navegando a la URL...");
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            Console.WriteLine("🔍 [LOGIN] Llenando credenciales...");
            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2025*");

            Console.WriteLine("🔍 [LOGIN] Haciendo click en Ingresar...");
            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();
            Thread.Sleep(50);

            Console.WriteLine("🔍 [LOGIN] Esperando campo de OTP...");
            var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
            
            Console.WriteLine("🔍 [LOGIN] Esperando OTP de la red...");
            // Esperar el OTP con timeout de 60 segundos (aumentado)
            var timeoutTask = Task.Delay(60000);
            var completedTask = await Task.WhenAny(tcsOtp.Task, timeoutTask);
            
            if (completedTask == timeoutTask)
            {
                throw new TimeoutException("⏱️ Timeout esperando el OTP después de 60 segundos");
            }
            
            var otp = await tcsOtp.Task;
            Console.WriteLine($"✅ [LOGIN] Ingresando OTP: {otp}");
            inputOtp.SendKeys(otp);
            Thread.Sleep(50);
            
            Console.WriteLine("🔍 [LOGIN] Haciendo click en Validar...");
            var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[contains(text(),'Validar')])[1]")));
            botonEnviar.Click();

            Console.WriteLine("🔍 [LOGIN] Esperando que cargue la página después del login...");
            // Esperar a que cargue la página después del login
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            Console.WriteLine("✅ [LOGIN] Login completado exitosamente!");
        }
    }
}