namespace AspelTools.Server.Modelo
{
    public class MovimientoInventario
    {
        public string CVE_ART { get; set; }

        public int ALMACEN { get; set; }

        public int NUM_MOV { get; set; }

        public int CVE_CPTO { get; set; }

        public DateTime? FECHA_DOCU { get; set; }

        public string TIPO_DOC { get; set; }

        public string REFER { get; set; }

        public string CLAVE_CLPV { get; set; }

        public string VEND { get; set; }

        public double? CANT { get; set; }

        public double? CANT_COST { get; set; }

        public double? PRECIO { get; set; }

        public double? COSTO { get; set; }

        public string AFEC_COI { get; set; }

        public int? CVE_OBS { get; set; }

        public int? REG_SERIE { get; set; }

        public string UNI_VENTA { get; set; }

        public int? E_LTPD { get; set; }

        public double? EXISTENCIA { get; set; }

        public string TIPO_PROD { get; set; }

        public double? FACTOR_CON { get; set; }

        public DateTime? FECHAELAB { get; set; }

        public int? CTLPOL { get; set; }

        public string CVE_FOLIO { get; set; }

        public int? SIGNO { get; set; }

        public string COSTEADO { get; set; }

        public double? COSTO_PROM_INI { get; set; }

        public double? COSTO_PROM_FIN { get; set; }

        public string DESDE_INVE { get; set; }

        public int? MOV_ENLAZADO { get; set; }

        public double? EXIST_G { get; set; }

        public double? COSTO_PROM_GRAL { get; set; }

    }
}
