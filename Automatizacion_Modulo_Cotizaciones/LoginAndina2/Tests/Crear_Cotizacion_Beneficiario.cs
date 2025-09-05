using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LoginAndina2.Models;
using LoginAndina2.Helpers;
using System.Globalization;
using LoginAndina2.Object;

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
        private DetallesPensionBeneficiario detallesPensionBeneficiario;
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

        private void LlenarDetallesPensionBeneficiario(DetallesPensionBeneficiario detalles)
        {
            var year = DateTime.Now.Year.ToString();
            var mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMM", new CultureInfo("es-ES")));

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
            LogAndFillCampo("BENEFICIARIO", "FechaNacimiento", detalles.FechaNacimiento, () => DatePickerHelperCausante.SelectDate(chromeDriver.Driver, campos["FechaNacimiento"], detalles.FechaNacimiento, xpathsDatePicker));
            LogAndFillCampo("BENEFICIARIO", "FechaExpedicionDocumento", detalles.FechaExpedicionDocumento, () => DatePickerHelperCausante.SelectDate(chromeDriver.Driver, campos["FechaExpedicionDocumento"], detalles.FechaExpedicionDocumento, xpathsDatePicker));

            Console.WriteLine("[BENEFICIARIO] Sección Detalles de Pensión completada.");
        }

        private void LlenarDetallesPensionYCotizacionBeneficiario(DetallesPensionYCotizacionBeneficiario detalles)
{
    var campos = CotizacionBeneficiarioXpaths.DatosBeneficiario;

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

            int numBeneficiarios = DetallesPensionYCotizacionBeneficiario.NumeroBeneficiarios;     // Obtiene el valor de numeroBeneficiarios desde cotizacionDatos
            bool companeroCreado = false;
            bool conyugeCreado = false;

            // Obtener fecha de nacimiento del causante
            var datosCausante = LoginAndina2.Models.CausanteDatos.causanteDatos();
            DateTime fechaNacimientoCausante;
            DateTime.TryParseExact(datosCausante.FechaNacimiento, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimientoCausante);

            // Detectar si el origen de pensión es VEJEZ
            var origenPension = datosCausante != null ? datosCausante.EstadoCausante : null;
            origenPension = LoginAndina2.Models.CausanteDatos.datosCotizacion().OrigenPension?.Trim().ToUpper();

            // Calcular porcentaje de pensión para cada beneficiario
            int porcentajeBase = 100 / (numBeneficiarios);
            int porcentajeRestante = 100;

            for (int i = 0; i <= numBeneficiarios; i++)
            {
                // Crear detalles de pensión y cotización beneficiario con porcentaje correcto
                var detallesPensionYCotizacion = DetallesPensionYCotizacionBeneficiario.GenerarAleatorio();
                if (i < numBeneficiarios)
                {
                    detallesPensionYCotizacion.PorcentajePension = porcentajeBase.ToString();
                }
                else
                {
                    detallesPensionYCotizacion.PorcentajePension = porcentajeRestante.ToString();
                }

                Console.WriteLine($"--- Llenando formulario de beneficiario #{i} ---");

                // Generar detalles aleatorios del beneficiario
                var detalles = DetallesPensionBeneficiarioDatos.GenerarAleatorio();
                string parentesco = detalles.Parentesco?.Trim().ToUpper();

                // Asignar porcentaje de pensión
                porcentajeRestante -= int.Parse(detallesPensionYCotizacion.PorcentajePension);

                // Llamar método de llenado con el objeto correcto
               // LlenarDetallesPensionYCotizacionBeneficiario(detallesPensionYCotizacion);

                // Si origen pensión es VEJEZ y ya hay un compañero/a o conyuge, todos los siguientes deben ser HIJO/A
                if (origenPension == "VEJEZ" && (companeroCreado || conyugeCreado))
                {
                    parentesco = "HIJO/A";
                    detalles.Parentesco = "HIJO/A";
                }
                // Si origen pensión es VEJEZ y parentesco generado no es permitido, forzar HIJO/A
               /* if (origenPension == "VEJEZ" && parentesco != "HIJO/A" && parentesco != "CONYUGE" && parentesco != "COMPAÑERO/A")
                {
                    parentesco = "HIJO/A";
                    detalles.Parentesco = "HIJO/A";
                }*/
                // Validaciones de parentesco según estado civil y SOBREVIVENCIA
                if (origenPension == "SOBREVIVENCIA" || origenPension == "INVALIDEZ")
                {
                    string estadoCivil = datosCausante.EstadoCivil?.Trim().ToUpper();
                    // Si es casado/a o unión libre, solo se permiten compañero/a, conyuge o hijo/a
                    if ((estadoCivil == "CASADO(A)" || estadoCivil == "UNION LIBRE") && parentesco != "HIJO/A" && parentesco != "CONYUGE" && parentesco != "COMPAÑERO/A")
                    {
                        parentesco = "HIJO/A";
                        detalles.Parentesco = "HIJO/A";
                    }
                    // Si es soltero/a, no se permite compañero/a ni conyuge
                    if (estadoCivil == "SOLTERO(A)" && (parentesco == "CONYUGE" || parentesco == "COMPAÑERO/A"))
                    {
                        parentesco = "HIJO/A";
                        detalles.Parentesco = "HIJO/A";
                    }
                }

                // Validación de parentesco único
                if ((parentesco == "COMPAÑERO/A" && companeroCreado) || (parentesco == "CONYUGE" && conyugeCreado))
                {
                    Console.WriteLine($"[VALIDACIÓN] Ya existe un beneficiario con parentesco '{detalles.Parentesco}', se omite este registro.");
                    continue;
                }
                if (parentesco == "COMPAÑERO/A") companeroCreado = true;
                if (parentesco == "CONYUGE") conyugeCreado = true;

                // Validación de edad para HIJO/A
                if (parentesco == "HIJO/A")
                {
                    DateTime fechaNacimiento;
                    int edad = 100; // valor imposible para entrar al bucle
                    int intentos = 0;
                    while (true)
                    {
                        if (DateTime.TryParseExact(detalles.FechaNacimiento, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimiento))
                        {
                            edad = DateTime.Now.Year - fechaNacimiento.Year;
                            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;

                            // Validaciones especiales para VEJEZ
                            if (origenPension == "VEJEZ")
                            {
                                // Hijos no pueden ser menores de 18 ni nacer antes que el causante
                                if (edad <= 25 && edad >= 18 && fechaNacimiento > fechaNacimientoCausante)
                                    break;
                                // Regenerar fecha válida: entre (hoy-25) y (hoy-18), pero después de la fecha del causante
                                int anioMin = Math.Max(DateTime.Now.Year - 25, fechaNacimientoCausante.Year + 1);
                                int anioMax = DateTime.Now.Year - 18;
                                if (anioMin > anioMax)
                                {
                                    Console.WriteLine($"[ERROR] Rango de años inválido para HIJO/A en VEJEZ. Se omite este registro.");
                                    continue;
                                }
                                detalles.FechaNacimiento = LoginAndina2.Models.DetallesPensionBeneficiarioDatos.GenerarFechaAleatoria(anioMin, anioMax);
                            }
                            else if (origenPension == "SOBREVIVENCIA" || origenPension == "INVALIDEZ")
                            {
                                // Hijos deben ser mayores de 18 años
                                if (edad <= 25 && edad >= 18) break;
                                // Regenerar fecha válida: entre (hoy-25) y (hoy-18)
                                int anioMin = DateTime.Now.Year - 25;
                                int anioMax = DateTime.Now.Year - 18;
                                if (anioMin > anioMax)
                                {
                                    Console.WriteLine($"[ERROR] Rango de años inválido para HIJO/A en SOBREVIVENCIA. Se omite este registro.");
                                    continue;
                                }
                                detalles.FechaNacimiento = LoginAndina2.Models.DetallesPensionBeneficiarioDatos.GenerarFechaAleatoria(anioMin, anioMax);
                            }
                            else
                            {
                                if (edad <= 25) break;
                                detalles.FechaNacimiento = LoginAndina2.Models.DetallesPensionBeneficiarioDatos.GenerarFechaAleatoria(DateTime.Now.Year - 25, DateTime.Now.Year);
                            }
                            intentos++;
                            if (intentos > 10) // Prevención de bucle infinito
                            {
                                Console.WriteLine($"[ERROR] No se pudo generar una fecha válida para HIJO/A tras 10 intentos. Se omite este registro.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[ERROR] No se pudo parsear la fecha de nacimiento '{detalles.FechaNacimiento}' para el beneficiario HIJO/A. Se omite este registro.");
                            continue;
                        }
                    }
                    // Ajustar fecha de expedición del documento: 18 años después de nacimiento
                    if (DateTime.TryParseExact(detalles.FechaNacimiento, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimiento))
                    {
                        DateTime fechaExpedicion = fechaNacimiento.AddYears(18);
                        detalles.FechaExpedicionDocumento = fechaExpedicion.ToString("yyyy/MM/dd");
                    }
                }

                // Validación de diferencia de edad para CONYUGE/COMPAÑERO/A
                if (parentesco == "CONYUGE" || parentesco == "COMPAÑERO/A")
                {
                    DateTime fechaNacimientoBeneficiario;
                    int intentos = 0;
                    while (true)
                    {
                        if (DateTime.TryParseExact(detalles.FechaNacimiento, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimientoBeneficiario))
                        {
                            int diferenciaAnios = fechaNacimientoBeneficiario.Year - fechaNacimientoCausante.Year;
                            int edadCompanero = DateTime.Now.Year - fechaNacimientoBeneficiario.Year;
                            if (DateTime.Now < fechaNacimientoBeneficiario.AddYears(edadCompanero)) edadCompanero--;
                            // Debe ser mayor de 18 años y cumplir diferencia con causante
                            if (diferenciaAnios >= 11 && edadCompanero > 18) break;
                            // Regenerar fecha nacimiento beneficiario
                            int anioMin = Math.Max(fechaNacimientoCausante.Year + 11, DateTime.Now.Year - 100); // límite inferior razonable
                            int anioMax = DateTime.Now.Year - 19; // para asegurar más de 18 años
                            if (anioMin > anioMax)
                            {
                                Console.WriteLine($"[ERROR] No se pudo generar un rango válido de años para {detalles.Parentesco}. Se omite este registro.");
                                continue;
                            }
                            detalles.FechaNacimiento = LoginAndina2.Models.DetallesPensionBeneficiarioDatos.GenerarFechaAleatoria(anioMin, anioMax);
                            intentos++;
                            if (intentos > 10)
                            {
                                Console.WriteLine($"[ERROR] No se pudo generar una fecha válida para {detalles.Parentesco} tras 10 intentos. Se omite este registro.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[ERROR] No se pudo parsear la fecha de nacimiento '{detalles.FechaNacimiento}' para el beneficiario {detalles.Parentesco}. Se omite este registro.");
                            continue;
                        }
                    }
                }

                // Si no es el último beneficiario, haz clic en el botón para añadir formulario
                if (i < numBeneficiarios)
                {
                    LlenarDetallesPensionBeneficiario(detalles);
                    LlenarDetallesPensionYCotizacionBeneficiario(detallesPensionYCotizacion);
                    LlenarDatosBancariosBeneficiario();
                    LlenarDatosContactoBeneficiario();
                    ClickAgregarBeneficiarioForm();
                    Console.WriteLine("[BENEFICIARIO] Se hizo clic en 'Agregar beneficiario'.");
                    Thread.Sleep(2000); // Espera a que cargue el nuevo formulario
                }
                else
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