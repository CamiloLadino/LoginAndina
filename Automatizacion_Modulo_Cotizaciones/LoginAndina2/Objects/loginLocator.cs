using OpenQA.Selenium;
using LoginAndina2.Helpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V138.Network;
using Newtonsoft.Json.Linq;
using System.Text;
using WaitHerlpers.Helpers;


namespace LoginAndina2
{
    

    public class LoginL
    {

        private readonly IWebDriver driver;

        public LoginL(IWebDriver driver)
        {
            this.driver = driver;
        }
        //Se crean los localizadores de login
        public IWebElement User =>
        driver.FindElement(By.XPath("//input[@aria-label='Inserte usuario']"));

        public IWebElement Pass =>
        driver.FindElement(By.XPath("//input[@aria-label='Inserte clave']"));

        public IWebElement Ingresar =>
        driver.FindElement(By.XPath("//span[contains(text(), 'Ingresar')]"));

        //Localizadores de OTP
        public IWebElement InputOtp =>
        driver.FindElement(By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']"));

        public IWebElement botonEnviar =>
        driver.FindElement(By.XPath("(//span[contains(text(),'Validar')])[1]"));



        // Metodo para ingresar credenciales
        public void IngresarUser()
        {

            var waitHelper = new WaitHelper(driver);

            waitHelper.EsperarElementoVisible(User);
            User.Clear();
            User.SendKeys("automatizacion");

            waitHelper.EsperarElementoVisible(Pass);
            Pass.Clear();
            Pass.SendKeys("Hola2025**");

        }

        //Metodo para lectura e ingreso de OTP
        public async Task IngresarOtp()
        {
            var waitHelper = new WaitHelper(driver);
            DevToolsSession devToolsSession = null;

            try
            {
                // Configurar DevTools ANTES de hacer clic en Ingresar
                devToolsSession = ((ChromeDriver)driver).GetDevToolsSession();
                var devToolsDomains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains>();

                // Habilitar Network con configuración completa
                await devToolsDomains.Network.Enable(new EnableCommandSettings
                {
                    MaxResourceBufferSize = 1024 * 1024,
                    MaxPostDataSize = 1024 * 1024
                });

                // Configurar para capturar todas las respuestas
                await devToolsDomains.Network.SetCacheDisabled(new SetCacheDisabledCommandSettings { CacheDisabled = true });

                var tcsOtp = new TaskCompletionSource<string>();
                var monitoredRequests = new Dictionary<string, string>(); // RequestId -> URL

                // Monitorear TODAS las requests
                devToolsDomains.Network.RequestWillBeSent += (sender, e) =>
                {
                   // Console.WriteLine($"🔍 Request enviado: {e.Request.Url}");
                    monitoredRequests[e.RequestId] = e.Request.Url;
                };

                // Monitorear respuestas
                devToolsDomains.Network.ResponseReceived += (sender, e) =>
                {
                   /* Console.WriteLine($"📡 Response recibida: {e.Response.Url}");
                    Console.WriteLine($"📡 Status: {e.Response.Status}");
                    Console.WriteLine($"📡 Content Type: {e.Response.MimeType}");
                    Console.WriteLine($"📡 Request ID: {e.RequestId}");*/

                    // Filtros más amplios para capturar OTP
                    string url = e.Response.Url.ToLower();
                    if (url.Contains("otp") ||
                        e.Response.MimeType?.Contains("application/json") == true)
                    {
                        //Console.WriteLine($"✅ URL candidata para OTP: {e.Response.Url}");
                        // No procesamos aquí, esperamos a LoadingFinished
                    }
                };

                // Procesar cuando la respuesta esté completamente cargada
                devToolsDomains.Network.LoadingFinished += async (sender, e) =>
                {
                    if (tcsOtp.Task.IsCompleted) return; // Ya encontramos el OTP

                    try
                    {
                        if (monitoredRequests.TryGetValue(e.RequestId, out string requestUrl))
                        {
                            string url = requestUrl.ToLower();

                            // Verificar si es una URL que podría contener OTP
                            if (url.Contains("otp"))

                            {
                               // Console.WriteLine($"🔍 Procesando respuesta de: {requestUrl}");

                                var responseBody = await devToolsDomains.Network.GetResponseBody(
                                    new GetResponseBodyCommandSettings { RequestId = e.RequestId });

                                string jsonText = responseBody.Base64Encoded
                                    ? Encoding.UTF8.GetString(Convert.FromBase64String(responseBody.Body))
                                    : responseBody.Body;

                               // Console.WriteLine($"📦 Cuerpo de respuesta: {jsonText}");

                                // Verificar si es JSON válido
                                if (!string.IsNullOrEmpty(jsonText) &&
                                    (jsonText.TrimStart().StartsWith("{") || jsonText.TrimStart().StartsWith("[")))
                                {
                                    var json = JObject.Parse(jsonText);

                                    // Buscar OTP en múltiples ubicaciones posibles
                                    var otp = ExtractOtpFromJson(json);

                                    if (!string.IsNullOrEmpty(otp))
                                    {
                                        Console.WriteLine($"🎯 OTP encontrado: {otp}");
                                        tcsOtp.TrySetResult(otp);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error procesando respuesta {e.RequestId}: {ex.Message}");
                    }
                };

                // AHORA hacer clic en Ingresar para iniciar el proceso
                waitHelper.EsperarElementoVisible(Ingresar);
                Ingresar.Click();
                Console.WriteLine("✅ Click en Ingresar realizado, esperando OTP...");

                // Esperar máximo 45 segundos por el OTP
                var timeoutTask = Task.Delay(10000);
                var completedTask = await Task.WhenAny(tcsOtp.Task, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    Console.WriteLine("⏰ Tiempo de espera agotado. OTP no recibido.");

                    // Mostrar todas las URLs capturadas para debug
                    Console.WriteLine("📋 URLs capturadas durante el proceso:");
                    foreach (var url in monitoredRequests.Values.Distinct())
                    {
                        Console.WriteLine($"  - {url}");
                    }

                    throw new TimeoutException("OTP no recibido en el tiempo esperado.");
                }

                var otp = await tcsOtp.Task;
                Console.WriteLine($"✅ OTP obtenido exitosamente: {otp}");

                // Ingresar el OTP
                waitHelper.EsperarElementoVisible(InputOtp);
                InputOtp.Clear();
                InputOtp.SendKeys(otp);

                Thread.Sleep(1000);

                waitHelper.EsperarElementoVisible(botonEnviar);
                botonEnviar.Click();

                Console.WriteLine("✅ OTP enviado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en IngresarOtp: {ex.Message}");
                throw;
            }

        }

        private string ExtractOtpFromJson(JObject json)
        {
            try
            {

                // Rutas comunes para OTP
                var commonPaths = new[]
                {
                    "data.code",
                    "data.otp",
                };

                // Buscar en rutas conocidas
                foreach (var path in commonPaths)
                {
                    var token = json.SelectToken(path);
                    if (token != null)
                    {
                        var value = token.ToString();
                        if (IsValidOtp(value))
                        {
                            Console.WriteLine($"🎯 OTP encontrado en ruta '{path}': {value}");
                            return value;
                        }
                        ;
                    }
                }
            

                    // Búsqueda exhaustiva en toda la estructura JSON
             var allStringValues = json.Descendants()
                         .Where(t => t.Type == JTokenType.String)
                         .Select(t => t.Value<string>())
                         .Where(s => IsValidOtp(s))
                         .ToList();

             if (allStringValues.Any())
             {
                 var foundOtp = allStringValues.First();
                // Console.WriteLine($"🔍 OTP encontrado por búsqueda exhaustiva: {foundOtp}");
                 return foundOtp;
             }

             //Console.WriteLine("❌ No se encontró OTP válido en el JSON");
             //Console.WriteLine($"📦 Estructura JSON completa: {json}");
             return null;
             // }
         }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error extrayendo OTP del JSON: {ex.Message}");
                return null;
            }
            }
        

        private bool IsValidOtp(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   value.Length == 6 &&
                   value.All(char.IsDigit);
        }
    }
    }