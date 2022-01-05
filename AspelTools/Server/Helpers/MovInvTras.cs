namespace AspelTools.Server.Helpers
{
    public class MovInvTras
    {
        public string ClaveProducto { get; set; }
        public int Almacen { get; set; }
        public int NumeroMovimiento { get; set; }
        public int Concepto { get; set; }
        public double ExistenciaPrevia { get; set; }
        public double ExistenciaNueva { get; set; }
        public double Costo { get; set; }
        public double ExistenciaGlobal { get; set; }
        public int Folio { get; set; }
        public string Referencia { get; set; }
        public int NumeroMovimientoEnlazado { get; set; } = 0;
        public int Signo { get; set; }
    }
}
