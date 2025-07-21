using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using LoginAndina2.Models;
using LoginAndina2.Helpers;
using DatePickerHelperCausanteNS = LoginAndina2.Helpers.DatePickerHelperCausante;
using System.Globalization;

namespace LoginAndina2
{
    [TestFixture]
    public class CrearCotizacionCausante
    {
        public string idusado; // ID usado realmente en el formulario

        private IWebDriver driver;
        private WebDriverWait wait;
        private CreacionCotizacion creacionCotizacion;

        public CrearCotizacionCausante() {}
        public CrearCotizacionCausante(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
            this.creacionCotizacion = new CreacionCotizacion(driver, wait);
        }

        [SetUp]
        public void SetUp()
        {
            if (driver == null || wait == null)
            {
                driver = new ChromeDriver();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Manage().Window.Maximize();
                creacionCotizacion = new CreacionCotizacion(driver, wait);
            }
        }

        [TearDown]
        public void TearDown()
        {
          //  driver.Quit();
        }


        // --------- SUBFASE: DATOS BÁSICOS DEL CAUSANTE ---------       
       
        private DatePickerHelperCausanteNS.DatePickerXpaths ObtenerDatePickerXpaths()
        {
            var year = DateTime.Now.Year.ToString();
            var mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMM", new CultureInfo("es-ES")));
            return new DatePickerHelperCausanteNS.DatePickerXpaths
            {
                YearSelectorBtn = $"//button[.//span[text()='{year}']]",
                YearItems = "//div[contains(@class, 'q-date__years-content')]",
                PrevYearsBtn = "//button[@aria-label='Anterior 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                NextYearsBtn = "//button[@aria-label='Siguiente 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']",
                MonthSelectorBtn = $"(//span[contains(text(),'{mes}')])[1]",
                MonthItems = "//span[@class='block']",
                DayItems = "//div[contains(@class,'q-date__calendar-item')]//button//span[@class='block']"
            };
        }
       



        [Test]
        public void Test_Fase2_CrearCotizacion()
        {
            // Llama el flujo de la fase 1 (CreacionCotizacion)
            creacionCotizacion.CrearCotizacionHappyPath();

             Console.WriteLine("[TEST] Iniciando llenado de datos basicos del causante...");
            llenardatosBasicosCausante();

             Console.WriteLine("[TEST] Iniciando llenado de datos de presentación...");
            llenarDatosPresentacion();


            Console.WriteLine("[TEST] Iniciando llenado de datos bancarios...");
            LlenarDatosBancarios();
       
            Console.WriteLine("[TEST] Iniciando llenado de datos de contacto...");
            var datosContacto = LoginAndina2.Models.DatosContactoDatos.GenerarAleatorio();
            LlenarDatosContacto(datosContacto);
            
            Console.WriteLine("[TEST] Presionando botón Continuar...");
            driver.FindElement(By.XPath(CotizacionCausanteXpaths.BtnContinuar)).Click();          

           
        }
        

        // --------- Logging utilitario optimizado ---------
        private void LogAndFillCampo(string tipo, string campo, string valor, Action fillAction)
        {
            Console.WriteLine($"[{tipo}] Campo: {campo} | Valor: {valor}");
            fillAction();
        }

  public void llenardatosBasicosCausante()
        {
            var causante = CausanteDatos.causanteDatos();
            idusado = causante.Identificacion; // Guardar el ID usado realmente

            // Definir datos de prueba para el causante

            // 1. Seleccionar primero el tipo de identificación para habilitar los demás campos
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["TipoIdentificacion"], causante.TipoIdentificacion, "Tipo de Identificación");

            // 2. Llenar campos de texto
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, CotizacionCausanteXpaths.DatosCausante["Identificacion"], causante.Identificacion, "Identificación");
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, CotizacionCausanteXpaths.DatosCausante["PrimerNombre"], causante.PrimerNombre, "Primer Nombre");
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, CotizacionCausanteXpaths.DatosCausante["SegundoNombre"], causante.SegundoNombre, "Segundo Nombre");
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, CotizacionCausanteXpaths.DatosCausante["PrimerApellido"], causante.PrimerApellido, "Primer Apellido");
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, CotizacionCausanteXpaths.DatosCausante["SegundoApellido"], causante.SegundoApellido, "Segundo Apellido");

            // 3. Seleccionar listas desplegables
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["Sexo"], causante.Sexo, "Sexo");
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["EstadoCivil"], causante.EstadoCivil, "Estado Civil");
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["EstadoCausante"], causante.EstadoCausante, "Estado Causante");
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["EPS"], causante.EPS, "EPS");
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, CotizacionCausanteXpaths.DatosCausante["ResidenteExterior"], causante.ResidenteExterior, "Residente Exterior");

            // 4. Llenar campos de fechas usando el DatePickerHelperCausante con XPaths configurables
          

           

            var xpathsFecha = ObtenerDatePickerXpaths();
            DatePickerHelperCausanteNS.SelectDate(driver, CotizacionCausanteXpaths.DatosCausante["FechaExpedicionDocumento"], causante.FechaExpedicionDocumento, xpathsFecha);
            DatePickerHelperCausanteNS.SelectDate(driver, CotizacionCausanteXpaths.DatosCausante["FechaNacimiento"], causante.FechaNacimiento, xpathsFecha);

        }        


        private void llenarDatosPresentacion() {

            // Diccionario de XPaths parametrizables para los campos de presentación
            var camposPresentacion = CotizacionCausanteXpaths.DatosPresentacion;
            var causante = CausanteDatos.causanteDatos();
            var presentacion = CausanteDatos.datosPresentacion();
           

            // 1. Llenar campos de texto
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposPresentacion["SemanasCotizadas"], presentacion.SemanasCotizadas, "Semanas Cotizadas");
            LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposPresentacion["IBL"], presentacion.IBL, "IBL");

            // 2. Solo llenar %PCL si la pensión es invalidez
            if (presentacion.OrigenInicialPrestacion.ToUpper().Contains("INVALIDEZ"))
            {
                LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposPresentacion["PorcentajePCLDerecho"], presentacion.PorcentajePCLDerecho, "%PCL Derecho");
                LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposPresentacion["PorcentajePCLActual"], presentacion.PorcentajePCLActual, "%PCL Actual");
            }

          
            var xpathsFechaPresentacion = ObtenerDatePickerXpaths();
            DatePickerHelperCausanteNS.SelectDate(driver, camposPresentacion["FechaCausacionDerecho"], presentacion.FechaCausacionDerecho, xpathsFechaPresentacion);
            // Solo si pensión es sobrevivencia + muerto
            if (presentacion.OrigenInicialPrestacion.ToUpper().Contains("SOBREVIVENCIA") &&
               causante.EstadoCausante.ToUpper().Contains("VALIDO") == false)
            {
                DatePickerHelperCausanteNS.SelectDate(driver, camposPresentacion["FechaMuerte"], presentacion.FechaMuerte, xpathsFechaPresentacion);
            }

            // 4. Seleccionar lista desplegable solo si pensión es INVALIDEZ o VEJEZ
            if (presentacion.OrigenInicialPrestacion != null && 
                (presentacion.OrigenInicialPrestacion.Trim().ToUpper() == "INVALIDEZ" || presentacion.OrigenInicialPrestacion.Trim().ToUpper() == "VEJEZ"))
            {
                LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposPresentacion["OrigenInicialPrestacion"], presentacion.OrigenInicialPrestacion, "Origen Inicial de Prestación");
            }
            // Si es SOBREVIVENCIA, no se intenta llenar ese campo

        }




        // --------- SUBFASE 3: DATOS BANCARIOS ---------
        private void LlenarDatosBancarios()
        {
            var causante = CausanteDatos.causanteDatos();
             // Caso: EL MISMO
            /*var bancariosMismo = new DatosBancarios
            {
                PrimerNombre = causante.PrimerNombre, 
                SegundoNombre = causante.SegundoNombre,
                PrimerApellido = causante.PrimerApellido,
                SegundoApellido = causante.SegundoApellido,
                IdentificacionTerceroPago = causante.Identificacion,
                NumeroCuentaBancaria = "123456789012",
                TipoTerceroPago = "EL MISMO",
                TipoIdentificacionTerceroPago = causante.TipoIdentificacion,
                TipoCuenta = "AHORROS",
                EntidadBancaria = "BANCAMIA",
                FormaPago = "TRANSFERENCIA"
            };*/

            var datosBancarios = LoginAndina2.Models.DatosBancariosDatos.GenerarAleatorio();
            // Diccionario de XPaths parametrizables para los campos de datos bancarios
            var camposBancarios = CotizacionCausanteXpaths.DatosBancarios;

            Console.WriteLine("[SUBFASE 3] Llenando datos bancarios...");

            // 1. Seleccionar tipo de tercero de pago primero (condición clave)
            LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["TipoTerceroPago"], datosBancarios.TipoTerceroPago,"Tipo tercero pago");
            bool esMismo = datosBancarios.TipoTerceroPago.Trim().ToUpper() == "EL MISMO";

            // 2. Llenar campos de texto condicionalmente
            if (esMismo)
            {
                LogAndFillCampo("BANCARIOS", "PrimerNombre", causante.PrimerNombre, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["PrimerNombre"], causante.PrimerNombre, "Primer Nombre cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "SegundoNombre", causante.SegundoNombre, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["SegundoNombre"], causante.SegundoNombre, "Segundo nombre cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "PrimerApellido", causante.PrimerApellido, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["PrimerApellido"], causante.PrimerApellido, "Primer Apellido cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "SegundoApellido", causante.SegundoApellido, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["SegundoApellido"], causante.SegundoApellido, "Segundo Apellido cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "IdentificacionTerceroPago", causante.Identificacion, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["IdentificacionTerceroPago"], causante.Identificacion, "Identificacion cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "TipoIdentificacionTerceroPago", causante.TipoIdentificacion, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["TipoIdentificacionTerceroPago"], causante.TipoIdentificacion, "Tipo Identificacion cuenta bancaria"));
            }
            else
            {
                LogAndFillCampo("BANCARIOS", "PrimerNombre", datosBancarios.PrimerNombre, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["PrimerNombre"], datosBancarios.PrimerNombre, "Primer Nombre cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "SegundoNombre", datosBancarios.SegundoNombre, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["SegundoNombre"], datosBancarios.SegundoNombre, "Segundo Nombre cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "PrimerApellido", datosBancarios.PrimerApellido, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["PrimerApellido"], datosBancarios.PrimerApellido, "Primer Apellido cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "SegundoApellido", datosBancarios.SegundoApellido, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["SegundoApellido"], datosBancarios.SegundoApellido, "Segundo Apellido cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "IdentificacionTerceroPago", datosBancarios.IdentificacionTerceroPago, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["IdentificacionTerceroPago"], datosBancarios.IdentificacionTerceroPago, "Identificacion cuenta bancaria"));
                LogAndFillCampo("BANCARIOS", "TipoIdentificacionTerceroPago", datosBancarios.TipoIdentificacionTerceroPago, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["TipoIdentificacionTerceroPago"], datosBancarios.TipoIdentificacionTerceroPago, "Tipo identificacion cuenta bancaria"));
            }

            // 3. Campos comunes
            LogAndFillCampo("BANCARIOS", "NumeroCuentaBancaria", datosBancarios.NumeroCuentaBancaria, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposBancarios["NumeroCuentaBancaria"], datosBancarios.NumeroCuentaBancaria, "Numero Cuenta Bancaria"));
            LogAndFillCampo("BANCARIOS", "TipoCuenta", datosBancarios.TipoCuenta, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["TipoCuenta"], datosBancarios.TipoCuenta,"Tipo Cuenta Bancaria"));
            LogAndFillCampo("BANCARIOS", "EntidadBancaria", datosBancarios.EntidadBancaria, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["EntidadBancaria"], datosBancarios.EntidadBancaria,"Entidad Bancaria"));
            LogAndFillCampo("BANCARIOS", "FormaPago", datosBancarios.FormaPago, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposBancarios["FormaPago"], datosBancarios.FormaPago,"Forma Pago"));
            Console.WriteLine("[SUBFASE 3] Datos bancarios completados");
        }


        // --------- SUBFASE 4: DATOS DE CONTACTO ---------
        private void LlenarDatosContacto(DatosContacto contacto)
        {
            var camposContacto = CotizacionCausanteXpaths.DatosContacto;
              var datosContacto = LoginAndina2.Models.DatosContactoDatos.GenerarAleatorio();
            Console.WriteLine("[SUBFASE 4] Llenando datos de contacto...");
            LogAndFillCampo("CONTACTO", "DepartamentoNacimiento", datosContacto.DepartamentoNacimiento, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposContacto["DepartamentoNacimiento"], datosContacto.DepartamentoNacimiento,"Departamento Nacimiento"));
            LogAndFillCampo("CONTACTO", "CiudadNacimiento", datosContacto.CiudadNacimiento, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposContacto["CiudadNacimiento"], datosContacto.CiudadNacimiento,"Ciudad Nacimiento"));
            LogAndFillCampo("CONTACTO", "PaisResidencia", datosContacto.PaisResidencia, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposContacto["PaisResidencia"], datosContacto.PaisResidencia,"Pais Residencia"));
            LogAndFillCampo("CONTACTO", "DepartamentoResidencia", datosContacto.DepartamentoResidencia, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposContacto["DepartamentoResidencia"], datosContacto.DepartamentoResidencia,"Departamento Residencia"));
            LogAndFillCampo("CONTACTO", "CiudadResidencia", datosContacto.CiudadResidencia, () => LoginAndina2.Helpers.FormHelper.SeleccionarOpcion(driver, wait, camposContacto["CiudadResidencia"], datosContacto.CiudadResidencia,"Ciudad Residencia"));
            LogAndFillCampo("CONTACTO", "DireccionYBarrio", datosContacto.DireccionYBarrio, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposContacto["DireccionYBarrio"], datosContacto.DireccionYBarrio,"Direccion Y Barrio"));
            LogAndFillCampo("CONTACTO", "TelefonoFijo", datosContacto.TelefonoFijo, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposContacto["TelefonoFijo"], datosContacto.TelefonoFijo,"Telefono Fijo"));
            LogAndFillCampo("CONTACTO", "Celular", datosContacto.Celular, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposContacto["Celular"], datosContacto.Celular,"Celular"));
            LogAndFillCampo("CONTACTO", "CorreoElectronico", datosContacto.CorreoElectronico, () => LoginAndina2.Helpers.FormHelper.LlenarCampo(driver, wait, camposContacto["CorreoElectronico"], datosContacto.CorreoElectronico,"Correo Electronico"));
            Console.WriteLine("[SUBFASE 4] Datos de contacto completados");
        }
          

    

       
    }
}
