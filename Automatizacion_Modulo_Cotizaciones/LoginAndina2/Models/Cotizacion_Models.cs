namespace LoginAndina2.Models
{
    public class DatosCausante
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string FechaExpedicionDocumento { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string EstadoCausante { get; set; }
        public string EPS { get; set; }
        public string ResidenteExterior { get; set; }
    }

    public class DatosPresentacion
    {
        public string SemanasCotizadas { get; set; }
        public string IBL { get; set; }
        public string PorcentajePCLDerecho { get; set; }
        public string PorcentajePCLActual { get; set; }
        public string FechaCausacionDerecho { get; set; }
        public string FechaMuerte { get; set; }
        public string OrigenInicialPrestacion { get; set; }
    }
    public class Cotizacion
    {
        public string EntidadSolicitante { get; set; }
        public string TipoCotizacion { get; set; }
        public string EstadoDocumentacion { get; set; }
        public string OrigenPension { get; set; }
        public string FechaFinVigencia { get; set; }
        public string Observaciones { get; set; }
        public float VrCapital { get; set; }
        public float VrPension { get; set; }
        public int Mesadas { get; set; }
        public string OrigenInicialPrestacion { get; set; } // NUEVO: para validación de edición
        public string XPathBoton { get; set; }
    }
    public class DatosBancarios
    {
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string IdentificacionTerceroPago { get; set; }
        public string NumeroCuentaBancaria { get; set; }
        public string TipoTerceroPago { get; set; }
        public string TipoIdentificacionTerceroPago { get; set; }
        public string TipoCuenta { get; set; }
        public string EntidadBancaria { get; set; }
        public string FormaPago { get; set; }
    }
    public class DatosContacto
    {
        public string DepartamentoNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string PaisResidencia { get; set; }
        public string DepartamentoResidencia { get; set; }
        public string CiudadResidencia { get; set; }
        public string DireccionYBarrio { get; set; }
        public string TelefonoFijo { get; set; }
        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }
    }
    public class DetallesPensionBeneficiario
    {
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
        public string FechaNacimiento { get; set; }
    }

    }
