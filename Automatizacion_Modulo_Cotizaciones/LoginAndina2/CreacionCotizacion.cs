using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;
using LoginAndina2.Helpers;
using System.Collections.Generic;
using SeleniumExtras.WaitHelpers;
using LoginAndina2.Models;
using DatePickerHelperCausanteNS = LoginAndina2.Helpers.DatePickerHelperCausante;
using System.Globalization;

namespace LoginAndina2
{
    [TestFixture]
    public class CreacionCotizacion
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string origenPensionSeleccionado;

        // Constructor vacío requerido por NUnit
        public CreacionCotizacion() { }

        // Constructor para inyectar driver y wait
        public CreacionCotizacion(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }



        // Clase para representar los datos de una cotización


        // Datos de la cotización a crear
        // Valores editables para distintos escenarios
        //private readonly DatosCotizacion cotizacion = new DatosCotizacion 
        // {    
        // };

        // Diccionario con los XPath de los campos del formulario
        private readonly Dictionary<string, string> camposFormulario = new Dictionary<string, string>
        {
            { "EntidadSolicitante", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]" },
            { "TipoCotizacion", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]" },
            { "EstadoDocumentacion", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]" },
            { "OrigenPension", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]" },
            { "Observaciones", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/textarea[1]" },
            { "VrCapital", "//label[contains(., 'Vr. capital')]//input" },
            { "VrPension", "//label[contains(., 'Vr. pensión')]//input" },
            { "FechaFinVigencia", "/html/body/div[1]/div/div[3]/main/div[3]/div[2]/div/div/div/div/div/div/div[2]/form/div/div[1]/div[8]/div/div/label/label/div/div" },
            { "Mesadas", "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[5]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]" }
        };

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private DatePickerHelperCausanteNS.DatePickerXpaths ObtenerDatePickerXpaths()
        {
            var year = DateTime.Now.Year.ToString();
            var fechaSiguienteMes = DateTime.Now.AddMonths(1);
            var mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                fechaSiguienteMes.ToString("MMM", new CultureInfo("es-ES")));
            // var mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMM", new CultureInfo("es-ES")));
            return new DatePickerHelperCausanteNS.DatePickerXpaths
            {
                YearSelectorBtn = $"//button[.//span[text()='{year}']]",
                YearItems = "//div[contains(@class, 'q-date__years-content')]",
                PrevYearsBtn = "//button[@aria-label='Anterior 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                NextYearsBtn = "//button[@aria-label='Siguiente 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                MonthSelectorBtn = $"//span[contains(text(),'{mes}')]",
                MonthItems = "//span[@class='block']",
                DayItems = "//div[contains(@class,'q-date__calendar-item')]//button//span[@class='block']"
            };
        }

        [Test]

        public async Task CrearCotizacionHappyPath()
        {
            Console.WriteLine("🚨 VERIFICACION: Este mensaje debe aparecer si se ejecuta el código correcto - ACTUALIZADO");
            // Iniciar sesión
            var login = new LoginC(driver, wait);
            await login.Login();

            // Esperar a que cargue la página principal después del login
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            
            // Debug: Verificar URL y título después del login
            Console.WriteLine($"URL después del login: {driver.Url}");
            Console.WriteLine($"Título de la página: {driver.Title}");
            
            // Agregar tiempo de espera adicional para que la página se cargue completamente
            Thread.Sleep(3000);

            try
            {
                // Intentar múltiples estrategias para encontrar el menú
                Console.WriteLine("🔍 Buscando menú de Suscripción/Cotización...");
                
                // Estrategia 1: Buscar por texto Suscripción o Cotización
                IWebElement MenuSuscripcionCotizacion = null;
                try
                {
                    MenuSuscripcionCotizacion = wait.Until(ExpectedConditions.ElementIsVisible(
                        By.XPath("//p[contains(text(), 'Suscripción') or contains(text(), 'Cotización')]")));
                    Console.WriteLine("✅ Encontrado con estrategia 1 (//p[contains...])");
                }
                catch
                {
                    Console.WriteLine("⚠️ Estrategia 1 falló, intentando estrategia 2...");
                    
                    // Estrategia 2: Buscar cualquier elemento que contenga "Cotizaci"
                    try
                    {
                        MenuSuscripcionCotizacion = wait.Until(ExpectedConditions.ElementIsVisible(
                            By.XPath("//*[contains(text(), 'Cotizaci')]")));
                        Console.WriteLine("✅ Encontrado con estrategia 2 (//*[contains(text(), 'Cotizaci')])");
                    }
                    catch
                    {
                        Console.WriteLine("⚠️ Estrategia 2 falló, intentando estrategia 3...");
                        
                        // Estrategia 3: Buscar elementos de menú navegable
                        try
                        {
                            MenuSuscripcionCotizacion = wait.Until(ExpectedConditions.ElementToBeClickable(
                                By.XPath("//a[contains(@href, 'cotizac') or contains(@href, 'suscri')]")));
                            Console.WriteLine("✅ Encontrado con estrategia 3 (//a[contains(@href...)])");
                        }
                        catch
                        {
                            Console.WriteLine("❌ Todas las estrategias fallaron. Listando elementos disponibles...");
                            
                            // Listar todos los elementos clickeables disponibles
                            var clickableElements = driver.FindElements(By.XPath("//*[@role='button' or @role='menuitem' or //a or //button]"));
                            Console.WriteLine($"📋 Elementos clickeables encontrados: {clickableElements.Count}");
                            
                            for (int i = 0; i < Math.Min(clickableElements.Count, 10); i++)
                            {
                                try
                                {
                                    var elem = clickableElements[i];
                                    Console.WriteLine($"  {i + 1}. Tag: {elem.TagName}, Texto: '{elem.Text}', Clase: '{elem.GetAttribute("class")}'");
                                }
                                catch { }
                            }
                            
                            throw new Exception("No se pudo encontrar el menú de cotizaciones");
                        }
                    }
                }
                
                MenuSuscripcionCotizacion.Click();
                Console.WriteLine("✅ Click en menú exitoso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error buscando menú Suscripción/Cotización: {ex.Message}");
                Console.WriteLine($"📄 HTML de la página actual (primeros 2000 caracteres):");
                Console.WriteLine(driver.PageSource.Substring(0, Math.Min(2000, driver.PageSource.Length)));
                throw;
            }
            //Thread.Sleep(500);
            var BotonCotizacion = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='item']/div[contains(text(), 'Crear Cotización')]")));
            BotonCotizacion.Click();
            var cotizacion = LoginAndina2.Models.CausanteDatos.datosCotizacion();
            // Thread.Sleep(500);
            // Llenar el formulario con los datos de la cotización

            // Llenar listas desplegables
            // if (!string.IsNullOrEmpty(camposFormulario["EntidadSolicitante"]))
            FormHelper.SeleccionarOpcion(driver, wait, camposFormulario["EntidadSolicitante"], cotizacion.EntidadSolicitante, "Entidad Solicitante");

            // if (!string.IsNullOrEmpty(camposFormulario["TipoCotizacion"]))
            FormHelper.SeleccionarOpcion(driver, wait, camposFormulario["TipoCotizacion"], cotizacion.TipoCotizacion, "Tipo Cotizacion");

            // if (!string.IsNullOrEmpty(camposFormulario["EstadoDocumentacion"]))
            FormHelper.SeleccionarOpcion(driver, wait, camposFormulario["EstadoDocumentacion"], cotizacion.EstadoDocumentacion, "Estado Documentacion");

            //  if (!string.IsNullOrEmpty(camposFormulario["OrigenPension"]))
            FormHelper.SeleccionarOpcion(driver, wait, camposFormulario["OrigenPension"], cotizacion.OrigenPension, "Origen Pension");

            // Asignar valor a la variable privada para exponerlo en la fase 2
            origenPensionSeleccionado = cotizacion.OrigenPension;


            // Usar DatePickerHelper para el campo de fecha
            try
            {
                Console.WriteLine("Seleccionando fecha de fin de vigencia...");
                // Usar el XPath del diccionario camposFormulario para máxima flexibilidad
                var xpathsFecha = ObtenerDatePickerXpaths();
                DatePickerHelperCausante.SelectDate(driver, camposFormulario["FechaFinVigencia"], cotizacion.FechaFinVigencia, xpathsFecha);

                //DatePickerHelper.SelectDate(driver, camposFormulario["FechaFinVigencia"], cotizacion.FechaFinVigencia);
                Console.WriteLine($"Fecha establecida a: {cotizacion.FechaFinVigencia}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al establecer la fecha: {ex.Message}");
                throw;
            }
            // Llenar campos de texto
            if (cotizacion.OrigenPension != null && cotizacion.OrigenPension.Trim().ToUpper() == "VEJEZ")
            {
                string valorCapital = cotizacion.VrCapital != 0 ? cotizacion.VrCapital.ToString() : "1500000";
                FormHelper.LlenarCampo(driver, wait, camposFormulario["VrCapital"], valorCapital, "VrCapital");

            }
            // Origen inicial de la prestación solo editable si es INVALIDEZ o VEJEZ
            if (cotizacion.OrigenPension != null &&
    (cotizacion.OrigenPension.Trim().ToUpper() == "INVALIDEZ" || cotizacion.OrigenPension.Trim().ToUpper() == "VEJEZ"))
            {
                if (camposFormulario.ContainsKey("OrigenInicialPrestacion"))
                {
                    // El valor debe coincidir con OrigenPension
                    FormHelper.LlenarCampo(driver, wait, camposFormulario["OrigenInicialPrestacion"], cotizacion.OrigenPension, "Origen inicial de la prestación");
                }
            }
            // Si es SOBREVIVENCIA, no se llena ni se edita
            FormHelper.LlenarCampo(driver, wait, camposFormulario["VrPension"], cotizacion.VrPension.ToString(), "VrPension");
            FormHelper.LlenarCampo(driver, wait, camposFormulario["Mesadas"], cotizacion.Mesadas.ToString(), "Mesadas");
            FormHelper.LlenarCampo(driver, wait, camposFormulario["Observaciones"], cotizacion.Observaciones.ToString(), "Observaciones");



            // Hacer clic en el botón de guardar
            var botonGuardar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath(cotizacion.XPathBoton)));
            botonGuardar.Click();
            // Thread.Sleep(1000);


            // Verificar que se completó correctamente
            // Assert.IsTrue(true, "La cotización se creó exitosamente");
        }

        // Método  para llenar campos de texto
        /*  private void LlenarCampo(string xpath, string valor)
          {
              if (string.IsNullOrEmpty(valor)) return;

              try
              {
                  Console.WriteLine($"Intentando llenar campo con XPath: {xpath} y valor: {valor}");

                  // Esperar a que el elemento sea visible y esté habilitado
                  var elemento = wait.Until(driver =>
                  {
                      try
                      {
                          var element = driver.FindElement(By.XPath(xpath));
                          return (element.Displayed && element.Enabled) ? element : null;
                      }
                      catch (NoSuchElementException)
                      {
                          return null;
                      }
                  }) ?? throw new NoSuchElementException($"No se pudo encontrar el elemento con XPath: {xpath}");

                  Console.WriteLine("Elemento encontrado, intentando interactuar...");

                  // Hacer scroll hasta el elemento 
                  ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'center'});", elemento);
                  Thread.Sleep(300);

                  // Obtener el tipo de elemento
                  string tagName = elemento.TagName.ToLower();
                  string type = elemento.GetAttribute("type")?.ToLower() ?? "";

                  Console.WriteLine($"Tipo de elemento: {tagName}, Tipo de input: {type}");


                  // Usar SendKeys 
                  try
                  {
                      Console.WriteLine("Intentando establecer valor con SendKeys...");

                      // Hacer clic en el elemento para darle foco
                      try
                      {
                          ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", elemento);
                      }
                      catch
                      {

                          elemento.Click();
                      }

                      // Limpiar el campo
                      elemento.Clear();
                      Thread.Sleep(100);

                      // Enviar el texto carácter por carácter
                      foreach (char c in valor)
                      {
                          elemento.SendKeys(c.ToString());
                      }

                      Console.WriteLine("Valor establecido con SendKeys");
                      return;
                  }
                  catch (Exception sendKeysEx)
                  {
                      Console.WriteLine($"Error al usar SendKeys: {sendKeysEx.Message}");
                  }

                  // Si llegamos aquí, ambos métodos fallaron
                  //throw new InvalidOperationException("No se pudo establecer el valor usando ningún método");
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Error al llenar campo con XPath: {xpath}");
                  Console.WriteLine($"Mensaje de error: {ex.Message}");




                  throw;
              }
        }*/
        /*
             // Método auxiliar para seleccionar opciones en dropdowns de Quasar
              private void SeleccionarOpcion(string xpath, string valor)
              {
                  if (string.IsNullOrEmpty(valor)) return;

                  try
                  {
                      // 1. Hacer clic en el campo para abrir el menú desplegable
                      var campo = wait.Until(driver => 
                      {
                          var element = driver.FindElement(By.XPath(xpath));
                          return (element.Displayed && element.Enabled) ? element : null;
                      });

                      // Usar JavaScript para hacer clic y evitar problemas de intercepción
                      ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", campo);

                      // 2. Esperar a que aparezca el menú desplegable
                      Thread.Sleep(500); // Pequeña pausa para que se abra el menú

                      // 3. Seleccionar la opción por el texto visible
                      var opcionXPath = $"//div[contains(@class, 'q-item__label') and normalize-space()='{valor}']";
                      var opcion = wait.Until(driver => 
                      {
                          var element = driver.FindElement(By.XPath(opcionXPath));
                          return (element.Displayed && element.Enabled) ? element : null;
                      });

                      ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", opcion);
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"Error al seleccionar opción '{valor}' con XPath: {xpath}");
                      Console.WriteLine($"Mensaje de error: {ex.Message}");
                      throw;
                  }
              }*/

        [TearDown]
        public void Teardown()
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante cleanup: {ex.Message}");
            }
            finally
            {
                // Asegurar que siempre se limpie
                driver = null;
                wait = null;
            }
        }

        public string ObtenerOrigenPension()
        {
            return origenPensionSeleccionado;
        }
    }
}