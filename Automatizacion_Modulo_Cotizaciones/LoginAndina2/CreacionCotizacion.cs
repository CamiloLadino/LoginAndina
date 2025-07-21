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
            // Iniciar sesión
            //var login = new LoginC(driver, wait);
           // await login.Login();

            // Esperar a que cargue la página principal después del login
           // wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            var MenuSuscripcionCotizacion = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'q-app\']/div/div[2]/aside/div/div[4]/div[1]/div/div/p")));
            MenuSuscripcionCotizacion.Click();
            //Thread.Sleep(500);
            var BotonCotizacion = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='item']/div[contains(text(), 'Crear Cotización')]")));
            BotonCotizacion.Click();
            var cotizacion = CausanteDatos.datosCotizacion();
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
            {
                FormHelper.SeleccionarOpcion(driver, wait, camposFormulario["OrigenPension"], cotizacion.OrigenPension, "Origen Pension");

                // Asignar valor a la variable privada para exponerlo en la fase 2
                origenPensionSeleccionado = cotizacion.OrigenPension;
            }

          


    

            
            // Usar DatePickerHelper para el campo de fecha
            try 
            {
                Console.WriteLine("Seleccionando fecha de fin de vigencia...");
                // Usar el XPath del diccionario camposFormulario para máxima flexibilidad
                var xpathsFecha = ObtenerDatePickerXpaths();
                DatePickerHelperCausante.SelectDate(driver, camposFormulario["FechaFinVigencia"], cotizacion.FechaFinVigencia,xpathsFecha);

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
    FormHelper.LlenarCampo(driver, wait, camposFormulario["VrCapital"], cotizacion.VrCapital.ToString(), "VrCapital");
}
            // Origen inicial de la prestación solo editable si es INVALIDEZ o VEJEZ
if (cotizacion.OrigenPension != null && 
    (cotizacion.OrigenPension.Trim().ToUpper() == "INVALIDEZ" || cotizacion.OrigenPension.Trim().ToUpper() == "VEJEZ"))
{
    if(camposFormulario.ContainsKey("OrigenInicialPrestacion"))
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
            driver?.Quit();
        }
        private string origenPensionSeleccionado;

        

        public string ObtenerOrigenPension()
        {
            return origenPensionSeleccionado;
        }
    }
}
