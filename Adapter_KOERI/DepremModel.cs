namespace Adapter_KOERI
{
    public class DepremModel
    {
        public DateTime TarihSaat { get; set; } // Tarih ve Saat
        public double Enlem { get; set; } // Enlem (N)
        public double Boylam { get; set; } // Boylam (E)
        public double Derinlik { get; set; } // Derinlik (km)
        public double Siddet { get; set; } // ML
        public string Yer { get; set; } = null!; // Yer
    }
}
