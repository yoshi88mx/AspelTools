namespace AspelTools.Client.Pages.Traspasos
{
    public class TraspasoPorLineaModelo
    {
        public string Linea { get; set; } = "%%%%%";
        public string AlmacenOrigen { get; set; }
        public string AlmacenDestino { get; set; }
        public bool Confirmacion { get; set; }
        public string Referencia { get; set; } = $"TRAS-{DateTime.Now.ToString("dMMyyyy")}";
    }
}
