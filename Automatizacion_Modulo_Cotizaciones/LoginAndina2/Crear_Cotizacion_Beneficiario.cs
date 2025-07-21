using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using LoginAndina2.Models;
using LoginAndina2.Helpers;
using System.Globalization;

namespace LoginAndina2
{
    [TestFixture]
    public class CrearCotizacionBeneficiario
    {
        //private IWebDriver driver;
        private DriverChrome chromeDriver;
        private WebDriverWait wait;
        private CreacionCotizacion creacionCotizacion;
        private CrearCotizacionCausante cotizacionCausante;
        private LoginPage loginPage;
        public CrearCotizacionBeneficiario(DriverChrome driver, WebDriverWait wait)
        {
            this.chromeDriver  = driver;
            this.wait = wait;
            this.creacionCotizacion = new CreacionCotizacion();
        }

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
            //chromeDriver.Driver.Manage().Window.Maximize();
            cotizacionCausante = new CrearCotizacionCausante(chromeDriver.Driver, wait);
            loginPage = new LoginPage(chromeDriver.Driver);


        }

        private void LlenarDetallesPensionBeneficiario()
        {
            var year = DateTime.Now.Year.ToString();
            var mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMM", new CultureInfo("es-ES")));

            var detalles = DetallesPensionBeneficiarioDatos.GenerarAleatorio();
            var campos = CotizacionBeneficiarioXpaths.DatosBeneficiario;
            

            Console.WriteLine("[BENEFICIARIO] Llenando sección Detalles de Pensión...");

            LogAndFillCampo("BENEFICIARIO", "TipoIdentificacion", detalles.TipoIdentificacion, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["TipoIdentificacion"], detalles.TipoIdentificacion, "Tipo de Identificación"));
            LogAndFillCampo("BENEFICIARIO", "Identificacion", detalles.Identificacion, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["Identificacion"], detalles.Identificacion, "Identificación"));
            LogAndFillCampo("BENEFICIARIO", "PrimerNombre", detalles.PrimerNombre, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["PrimerNombre"], detalles.PrimerNombre, "Primer Nombre"));
            LogAndFillCampo("BENEFICIARIO", "SegundoNombre", detalles.SegundoNombre, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["SegundoNombre"], detalles.SegundoNombre, "Segundo Nombre"));
            LogAndFillCampo("BENEFICIARIO", "PrimerApellido", detalles.PrimerApellido, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["PrimerApellido"], detalles.PrimerApellido, "Primer Apellido"));
            LogAndFillCampo("BENEFICIARIO", "SegundoApellido", detalles.SegundoApellido, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["SegundoApellido"], detalles.SegundoApellido, "Segundo Apellido"));
            LogAndFillCampo("BENEFICIARIO", "Parentesco", detalles.Parentesco, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["Parentesco"], detalles.Parentesco, "Parentesco"));
            LogAndFillCampo("BENEFICIARIO", "Sexo", detalles.Sexo, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["Sexo"], detalles.Sexo, "Sexo"));
            LogAndFillCampo("BENEFICIARIO", "EstadoCivil", detalles.EstadoCivil, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["EstadoCivil"], detalles.EstadoCivil, "Estado Civil"));
            LogAndFillCampo("BENEFICIARIO", "EstadoBeneficiario", detalles.EstadoBeneficiario, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["EstadoBeneficiario"], detalles.EstadoBeneficiario, "Estado Beneficiario"));
            // EPS no editable si OrigenPension es SOBREVIVENCIA
var datosCotizacion = LoginAndina2.Models.CausanteDatos.datosCotizacion();
if (datosCotizacion.OrigenPension == null || datosCotizacion.OrigenPension.Trim().ToUpper() != "SOBREVIVENCIA")
{
    LogAndFillCampo("BENEFICIARIO", "EPS", detalles.EPS, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["EPS"], detalles.EPS, "EPS"));
}
// Si es SOBREVIVENCIA, no se llena EPS
            LogAndFillCampo("BENEFICIARIO", "ResidenteExterior", detalles.ResidenteExterior, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["ResidenteExterior"], detalles.ResidenteExterior, "Residente Exterior"));
            var xpathsDatePicker = new DatePickerHelperCausante.DatePickerXpaths
            {
                YearSelectorBtn = $"//button[.//span[text()='{year}']]",
                YearItems = "(//div[@class='q-date__years-content col self-stretch row items-center'])[1]",
                PrevYearsBtn = "//button[@aria-label='Anterior 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                NextYearsBtn = "//button[@aria-label='Siguiente 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                MonthSelectorBtn = $"(//span[contains(text(),'{mes}')])[1]",
                MonthItems = "//div[contains(@class, 'q-date__months-item')]//span[@class='block']",
                DayItems = "//div[contains(@class,'q-date__calendar-item')]//button//span[@class='block']"
            };
            LogAndFillCampo("BENEFICIARIO", "FechaExpedicionDocumento", detalles.FechaExpedicionDocumento, () => DatePickerHelperCausante.SelectDate(chromeDriver.Driver, campos["FechaExpedicionDocumento"], detalles.FechaExpedicionDocumento, xpathsDatePicker));
            LogAndFillCampo("BENEFICIARIO", "FechaNacimiento", detalles.FechaNacimiento, () => DatePickerHelperCausante.SelectDate(chromeDriver.Driver, campos["FechaNacimiento"], detalles.FechaNacimiento, xpathsDatePicker));

            Console.WriteLine("[BENEFICIARIO] Sección Detalles de Pensión completada.");
        }

        private void LlenarDetallesPensionYCotizacionBeneficiario()
        {
            var campos = CotizacionBeneficiarioXpaths.DatosBeneficiario;
            var detalles = DetallesPensionYCotizacionBeneficiario.GenerarAleatorio();

            LogAndFillCampo("BENEFICIARIO", "AFP", detalles.AFP, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["AFP"], detalles.AFP, "AFP"));
            LogAndFillCampo("BENEFICIARIO", "Temporalidad", detalles.Temporalidad, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["Temporalidad"], detalles.Temporalidad, "Temporalidad"));

            // Solo llenar estos campos si la pensión es de tipo Sobrevivencia
            var datosCotizacion = LoginAndina2.Models.CausanteDatos.datosCotizacion(); // Ajusta si la instancia de datosCotizacion está disponible de otra forma
            if (datosCotizacion.OrigenPension != null && datosCotizacion.OrigenPension.Trim().ToUpper() == "SOBREVIVENCIA")
            {
                LogAndFillCampo("BENEFICIARIO", "DerechoAPension", detalles.DerechoAPension, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["DerechoAPension"], detalles.DerechoAPension, "Derecho a pensión"));
                LogAndFillCampo("BENEFICIARIO", "PorcentajePension", detalles.PorcentajePension, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["PorcentajePension"], detalles.PorcentajePension, "% Pensión"));
            }

            Console.WriteLine("[BENEFICIARIO] Sección Detalles de Pensión y Cotización completada.");
        }


        // Logging helper similar to causante
        private void LogAndFillCampo(string seccion, string campo, string valor, Action accion)
        {
            Console.WriteLine($"[{seccion}] Llenando campo {campo}: {valor}");
            accion();
        }

        private void LlenarDatosContactoBeneficiario()
        {
            var campos = CotizacionBeneficiarioXpaths.DatosBeneficiario;
            var datosContacto = LoginAndina2.Models.DatosContactoDatos.GenerarAleatorio();
            Console.WriteLine($"[BENEFICIARIO] Datos de contacto generados: {System.Text.Json.JsonSerializer.Serialize(datosContacto)}");

            LogAndFillCampo("BENEFICIARIO", "DepartamentoNacimiento", datosContacto.DepartamentoNacimiento, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["DepartamentoNacimiento"], datosContacto.DepartamentoNacimiento, "Departamento de Nacimiento"));
            LogAndFillCampo("BENEFICIARIO", "CiudadNacimiento", datosContacto.CiudadNacimiento, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["CiudadNacimiento"], datosContacto.CiudadNacimiento, "Ciudad de Nacimiento"));
            LogAndFillCampo("BENEFICIARIO", "PaisResidencia", datosContacto.PaisResidencia, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["PaisResidencia"], datosContacto.PaisResidencia, "País de Residencia"));
            LogAndFillCampo("BENEFICIARIO", "DepartamentoResidencia", datosContacto.DepartamentoResidencia, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["DepartamentoResidencia"], datosContacto.DepartamentoResidencia, "Departamento de Residencia"));
            LogAndFillCampo("BENEFICIARIO", "CiudadResidencia", datosContacto.CiudadResidencia, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["CiudadResidencia"], datosContacto.CiudadResidencia, "Ciudad de Residencia"));
            LogAndFillCampo("BENEFICIARIO", "DireccionYBarrio", datosContacto.DireccionYBarrio, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["DireccionYBarrio"], datosContacto.DireccionYBarrio, "Dirección y Barrio"));
            LogAndFillCampo("BENEFICIARIO", "TelefonoFijo", datosContacto.TelefonoFijo, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["TelefonoFijo"], datosContacto.TelefonoFijo, "Teléfono Fijo"));
            LogAndFillCampo("BENEFICIARIO", "Celular", datosContacto.Celular, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["Celular"], datosContacto.Celular, "Celular"));
            LogAndFillCampo("BENEFICIARIO", "CorreoElectronico", datosContacto.CorreoElectronico, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["CorreoElectronico"], datosContacto.CorreoElectronico, "Correo Electrónico"));
        }

        private void LlenarDatosBancariosBeneficiario()
        {
            var campos = CotizacionBeneficiarioXpaths.DatosBeneficiario;
            var datosBancarios = LoginAndina2.Models.DatosBancariosDatos.GenerarAleatorio();
            Console.WriteLine($"[BENEFICIARIO] Datos bancarios generados: {System.Text.Json.JsonSerializer.Serialize(datosBancarios)}");

            LogAndFillCampo("BENEFICIARIO", "EntidadBancaria", datosBancarios.EntidadBancaria, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["EntidadBancaria"], datosBancarios.EntidadBancaria, "Entidad Bancaria"));
            LogAndFillCampo("BENEFICIARIO", "TipoCuenta", datosBancarios.TipoCuenta, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["TipoCuenta"], datosBancarios.TipoCuenta, "Tipo de Cuenta"));
            LogAndFillCampo("BENEFICIARIO", "NumeroCuentaBancaria", datosBancarios.NumeroCuentaBancaria, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["NumeroCuentaBancaria"], datosBancarios.NumeroCuentaBancaria, "Número de Cuenta Bancaria"));
            LogAndFillCampo("BENEFICIARIO", "FormaPago", datosBancarios.FormaPago, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["FormaPago"], datosBancarios.FormaPago, "Forma de Pago"));
            LogAndFillCampo("BENEFICIARIO", "TipoTerceroPago", datosBancarios.TipoTerceroPago, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["TipoTerceroPago"], datosBancarios.TipoTerceroPago, "Tipo de Tercero Pago"));
            LogAndFillCampo("BENEFICIARIO", "TipoIdentificacionTerceroPago", datosBancarios.TipoIdentificacionTerceroPago, () => FormHelper.SeleccionarOpcion(chromeDriver.Driver, wait, campos["TipoIdentificacionTerceroPago"], datosBancarios.TipoIdentificacionTerceroPago, "Tipo de Identificación Tercero Pago"));
            LogAndFillCampo("BENEFICIARIO", "IdentificacionTerceroPago", datosBancarios.IdentificacionTerceroPago, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["IdentificacionTerceroPago"], datosBancarios.IdentificacionTerceroPago, "Identificación Tercero Pago"));
            LogAndFillCampo("BENEFICIARIO", "PrimerNombreBanco", datosBancarios.PrimerNombre, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["PrimerNombreBanco"], datosBancarios.PrimerNombre, "Primer Nombre del Banco"));
            LogAndFillCampo("BENEFICIARIO", "SegundoNombreBanco", datosBancarios.SegundoNombre, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["SegundoNombreBanco"], datosBancarios.SegundoNombre, "Segundo Nombre del Banco"));
            LogAndFillCampo("BENEFICIARIO", "PrimerApellidoBanco", datosBancarios.PrimerApellido, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["PrimerApellidoBanco"], datosBancarios.PrimerApellido, "Primer Apellido cuenta bancaria"));
            LogAndFillCampo("BENEFICIARIO", "SegundoApellidoBanco", datosBancarios.SegundoApellido, () => FormHelper.LlenarCampo(chromeDriver.Driver, wait, campos["SegundoApellidoBanco"], datosBancarios.SegundoApellido, "Segundo Apellido cuenta bancaria"));
        }

        [Test]

        public async Task EjecutarCotizacionBeneficiario()
        {
            
            //await loginPage.LoginSequentialAsync();
            //Console.WriteLine("Login exitoso");
            //cotizacionCausante.Test_Fase2_CrearCotizacion();

            int numBeneficiarios = 1; // Cambia este valor según lo que necesites
            for (int i = 0; i <= numBeneficiarios; i++)
            {
                Console.WriteLine($"--- Llenando formulario de beneficiario #{i} ---");                             

                // Si no es el último beneficiario, haz clic en el botón para añadir formulario
                if (i < numBeneficiarios)
                {
                    LlenarDetallesPensionBeneficiario();
                    LlenarDetallesPensionYCotizacionBeneficiario();
                    LlenarDatosBancariosBeneficiario();
                    LlenarDatosContactoBeneficiario();
                    ClickAgregarBeneficiarioForm();
                    Console.WriteLine("[BENEFICIARIO] Se hizo clic en 'Agregar beneficiario'.");
                    Thread.Sleep(2000); // Espera a que cargue el nuevo formulario
                }else
                {
                   Thread.Sleep(1000);
                    //ClickAgregarBeneficiarioForm();
                    ClickGuardarForm();
                    Console.WriteLine("[BENEFICIARIO] Se hizo clic en 'Guardar'.");
                   
                }
            }
            
        }

        private void ClickAgregarBeneficiarioForm()
        {
            // Ajusta el XPath según el botón real de tu formulario
            var xpathBtnAgregar = "(//span[contains(text(),'Agregar beneficiario')])[1]";
            var btnAgregar = chromeDriver.Driver.FindElement(By.XPath(xpathBtnAgregar));
            btnAgregar.Click();
           
        }
        private void ClickGuardarForm()
        {
            // Ajusta el XPath según el botón real de tu formulario
            var xpathBtnguardar = "//span[normalize-space()='Guardar']";
            var btnGuardar = chromeDriver.Driver.FindElement(By.XPath(xpathBtnguardar));
            btnGuardar.Click();
            //Console.WriteLine("Cotizacion guardada"); 
        }

        

    }
}