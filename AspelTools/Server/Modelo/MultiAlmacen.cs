namespace AspelTools.Server.Modelo
{
    public class MultiAlmacen
    {
        public string? ClaveArticulo { get; set; }
        public int ClaveAlmacen { get; set; }
        public string? Status { get; set; } = "A";
        public string? ControlAlmacen { get; set; } = String.Empty;
        public double Existencia { get; set; } = 0;
        public double StockMinimo { get; set; } = 0;
        public double StockMaximo { get; set; } = 0;
        public double ComprasPorRecibir { get; set; } = 0;
        public string? UUID { get; set; }
        public DateTime VersionSincronizacion { get; set; } = DateTime.Now;
    }
}
