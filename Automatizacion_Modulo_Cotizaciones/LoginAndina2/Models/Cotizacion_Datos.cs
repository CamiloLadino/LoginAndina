using LoginAndina2.Models;
using LoginAndina2;
using System;


namespace LoginAndina2.Models
{
   
        public class DatosBancariosDatos
    {
        private static readonly string[] TiposCuenta = { "AHORROS", "CORRIENTE" };
        private static readonly string[] Entidades = { "BANCOLOMBIA", "BANCAMIA", "BANCO ITAU", "BANCO DE BOGOTA" };
        private static readonly string[] FormasPago = { "TRANSFERENCIA" };
        private static readonly string[] TiposTercero = { "APODERADO", "EL MISMO", "GUARDADOR", "PERSONA DE APOYO", "REP. LEGAL" };
        private static readonly string[] TiposIdentificacion = { "CÉDULA CIUDADANIA", "NIT", "CÉDULA EXTRANJERÍA" };
        private static readonly string[] Nombres = { "Juan", "Carlos", "Maria", "Ana" };
        private static readonly string[] Apellidos = { "García", "Martínez", "Rodríguez", "López" };
        private static readonly Random random = new Random();

        public static DatosBancarios GenerarAleatorio()
        {
            return new DatosBancarios
            {
                PrimerNombre = Nombres[random.Next(Nombres.Length)],
                SegundoNombre = Nombres[random.Next(Nombres.Length)],
                PrimerApellido = Apellidos[random.Next(Apellidos.Length)],
                SegundoApellido = Apellidos[random.Next(Apellidos.Length)],
                IdentificacionTerceroPago = random.Next(10000000, 99999999).ToString(),
                NumeroCuentaBancaria = random.Next(100000000, 999999999).ToString(),
                TipoTerceroPago = TiposTercero[random.Next(TiposTercero.Length)],
                TipoIdentificacionTerceroPago = TiposIdentificacion[random.Next(TiposIdentificacion.Length)],
                TipoCuenta = TiposCuenta[random.Next(TiposCuenta.Length)],
                EntidadBancaria = Entidades[random.Next(Entidades.Length)],
                FormaPago = FormasPago[random.Next(FormasPago.Length)]
            };
        }
    }

    public class DatosContactoDatos
    {/*
        public string DepartamentoNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string PaisResidencia { get; set; }
        public string DepartamentoResidencia { get; set; }
        public string CiudadResidencia { get; set; }
        public string DireccionYBarrio { get; set; }
        public string TelefonoFijo { get; set; }
        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }*/

        private static readonly string[] Departamentos = { "CUNDINAMARCA" };
        private static readonly string[] Ciudades = { "ALBAN" };
        private static readonly string[] Paises = { "COLOMBIA" };
        private static readonly Random random = new Random();

        public static DatosContacto GenerarAleatorio()
        {
            return new DatosContacto
            {
                DepartamentoNacimiento = Departamentos[random.Next(Departamentos.Length)],
                CiudadNacimiento = Ciudades[random.Next(Ciudades.Length)],
                PaisResidencia = Paises[random.Next(Paises.Length)],
                DepartamentoResidencia = Departamentos[random.Next(Departamentos.Length)],
                CiudadResidencia = Ciudades[random.Next(Ciudades.Length)],
                DireccionYBarrio = $"Calle {random.Next(1, 100)} {random.Next(1, 100)} {random.Next(1, 100)} Barrio Prueba",
                TelefonoFijo = $"{random.Next(1000000, 9999999)}",
                Celular = $"3{random.Next(10, 100)}{random.Next(1000000, 9999999)}",
                CorreoElectronico = $"usuario{random.Next(1000, 9999)}@mail.com"
            };
        }
    }
    public class DetallesPensionBeneficiarioDatos
    {/*
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Parentesco { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string EstadoBeneficiario { get; set; }
        public string EPS { get; set; }
        public string ResidenteExterior { get; set; }
        public string FechaExpedicionDocumento { get; set; }
        public string FechaNacimiento { get; set; }*/
        

        // Métodos utilitarios para generación aleatoria
        private static readonly string[] TiposIdentificacion = { "CÉDULA CIUDADANIA",/* "CÉDULA EXTRANJERÍA", "NIT", "PASAPORTE", "PERMISO PERMANENCIA", "PERMISO TEMPORAL", "REGISTRO CIVIL", "TARJETA IDENTIDAD" */};
        private static readonly string[] Parentescos = { "COMPAÑERO/A", "CONYUGE", "HERMANO/A", "HIJO/A", "MADRE", "PADRE" };
        private static readonly string[] Sexos = { "FEMENINO", /*"MASCULINO"*/ };
        private static readonly string[] EstadosCiviles = { /*"CASADO(A)", "DIVORCIADO(A)", "SOLTERO(A)", "UNIÓN LIBRE",*/ "VIUDO(A)" };
        private static readonly string[] EstadosBeneficiario = { /*"INVALIDO",*/  "VALIDO" };
        private static readonly string[] EPSs = { "ALIANSALUD" };
        private static readonly string[] ResidenteExteriorOpciones = { "SI" };
        private static readonly string[] Nombres = { "Juan", "Carlos", "Andrés", "Camilo", "Felipe", "Luis", "Diego", "Jorge", "David", "Alejandro", "Santiago", "Daniel", "Sebastián", "Mateo", "Miguel", "Valentina", "Camila", "Daniela", "María", "Laura", "Sara", "Paula", "Isabella", "Gabriela", "Sofía" };
        private static readonly string[] Apellidos = { "García", "Martínez", "Rodríguez", "López", "González", "Pérez", "Sánchez", "Ramírez", "Torres", "Flores", "Rivera", "Gómez", "Castro", "Moreno", "Jiménez", "Ruiz", "Hernández", "Muñoz", "Romero", "Alvarez" };

        private static readonly Random random = new Random();
        
                   
        

        //DATOS BENEFICIARIOS

        public static DetallesPensionBeneficiario GenerarAleatorio()
        {
            var detalles = new DetallesPensionBeneficiario
            {
                TipoIdentificacion = TiposIdentificacion[random.Next(TiposIdentificacion.Length)],
                Identificacion = GenerarIdentificacion(),
                PrimerNombre = Nombres[random.Next(Nombres.Length)],
                SegundoNombre = Nombres[random.Next(Nombres.Length)],
                PrimerApellido = Apellidos[random.Next(Apellidos.Length)],
                SegundoApellido = Apellidos[random.Next(Apellidos.Length)],
                Parentesco = Parentescos[random.Next(Parentescos.Length)],
                Sexo = Sexos[random.Next(Sexos.Length)],
                EstadoCivil = EstadosCiviles[random.Next(EstadosCiviles.Length)],
                EstadoBeneficiario = EstadosBeneficiario[random.Next(EstadosBeneficiario.Length)],
                EPS = EPSs[random.Next(EPSs.Length)],
                ResidenteExterior = ResidenteExteriorOpciones[random.Next(ResidenteExteriorOpciones.Length)],
                FechaExpedicionDocumento = GenerarFechaAleatoria(1990, 2022),
                FechaNacimiento = GenerarFechaAleatoria(1950, 1970)
            };
            return detalles;
        }
        //Metodo para generar identificacion
        public static string GenerarIdentificacion()
        {
            // Genera un número de identificación colombiano de 8 a 10 dígitos
            int length = random.Next(8, 11);
            string id = "";
            for (int i = 0; i < length; i++)
                id += random.Next(0, 10).ToString();
            return id;
        }
        //Metodo para generar fecha
        public static string GenerarFechaAleatoria(int anioInicio, int anioFin)
        {
            int year = random.Next(anioInicio, anioFin + 1);
            int month = random.Next(1, 13);
            int day = random.Next(1, 28); // Para evitar problemas con febrero
            return $"{year:D4}/{month:D2}/{day:D2}";
        }
    }
 //DATOS DETALLES PENSION Y COTIZACION BENEFICIARIO
    public class DetallesPensionYCotizacionBeneficiario
    {
        public string AFP { get; set; }
        public string Temporalidad { get; set; }
        public string DerechoAPension { get; set; }
        public string PorcentajePension { get; set; }

        private static readonly string[] AFPs = { "PROTECCION", "COLFONDOS", "PORVENIR", "SKANDIA" };
        private static readonly string[] Temporalidades = { /*"TEMPORAL",*/ "VITALICIO" };
        private static readonly string[] DerechosAPension = { "SI", "NO" };
        private static readonly int numeroBeneficiarios = 2; // Ajusta el número de beneficiarios
        
        // Propiedad pública para acceder al número de beneficiarios
        public static int NumeroBeneficiarios => numeroBeneficiarios;
        
        private static readonly Random random = new Random();

        public static DetallesPensionYCotizacionBeneficiario GenerarAleatorio()
        {
            return new DetallesPensionYCotizacionBeneficiario
            {
                AFP = AFPs[random.Next(AFPs.Length)],
                Temporalidad = Temporalidades[random.Next(Temporalidades.Length)],
                DerechoAPension = DerechosAPension[random.Next(DerechosAPension.Length)],
                PorcentajePension = random.Next(2, 101).ToString() // 2 a 100
            };
        }
    }


    public class CausanteDatos
    {
        private static readonly Random random = new Random();
        

        //DATOS DE LA COTIZACION
        public static Cotizacion datosCotizacion() {
            var datosCotizacion = new Cotizacion
            {
                EntidadSolicitante = "COLFONDOS",
                TipoCotizacion = "COTIZACION DIRECTA",
                EstadoDocumentacion = "PENDIENTE",
                //ESTE CAMPO SELECCIONA EL TIPO DE PENSION
                OrigenPension = "INVALIDEZ", //SOBREVIVENCIA, INVALIDEZ, VEJEZ
                FechaFinVigencia = "2025/08/18", //Debe ser mayor a fecha actual
                Observaciones = "Cotización de prueba",
                VrCapital = 1500000,
                VrPension = 2000000,
                Mesadas = 13, //13 o 14
                XPathBoton = "//span[normalize-space()='Continuar']"
            }; return datosCotizacion;
        }

        //DATOS DEL CAUSANTE

        public static DatosCausante causanteDatos()
        {
            // Asignar EstadoCausante según OrigenPension
            string origenPension = datosCotizacion().OrigenPension?.Trim().ToUpper();
            string estadoCausante = "MUERTO";
            if (origenPension == "INVALIDEZ")
                estadoCausante = "INVALIDO";
            else if (origenPension == "VEJEZ")
                estadoCausante = "VALIDO";
            // Para SOBREVIVENCIA por defecto MUERTO

            var causante = new DatosCausante 
            {
                TipoIdentificacion = "CÉDULA CIUDADANIA",
                Identificacion = /*"75524587",*/random.Next(10000000, 99999999).ToString(),
                PrimerNombre = "Pedro",
                SegundoNombre = "CARLOS",
                PrimerApellido = "PEREZ",
                SegundoApellido = "RAMIREZ",
                FechaExpedicionDocumento = "2010/05/15",
                FechaNacimiento = "1964/12/01", 
                Sexo = "MASCULINO",
                EstadoCivil = "CASADO(A)",
                EstadoCausante = estadoCausante,
                EPS = "SURA",
                ResidenteExterior = "NO"
            };
            return causante;
        }
        //DATOS DE PRESENTACION
        public static DatosPresentacion datosPresentacion()
        {
            var presentacion = new DatosPresentacion
            {
                SemanasCotizadas = "1200",
                IBL = "1800000",
                PorcentajePCLDerecho = random.Next(51, 100).ToString(), 
                PorcentajePCLActual = random.Next(51, 100).ToString(),   
                FechaCausacionDerecho = "2023/07/01",
                FechaMuerte = "2022/11/15", // Solo si pensión es sobrevivencia + muerto
                OrigenInicialPrestacion = datosCotizacion().OrigenPension, // creacionCotizacion.ObtenerOrigenPension() // Usa el tipo de pensión de la cotización creada
            };
            return presentacion;
        }



    }
    

}


