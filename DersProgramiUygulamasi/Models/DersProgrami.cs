namespace DersProgramiUygulamasi.Models
{
    public class DersProgrami
    {
        public int Id { get; set; }
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }
        public int DersId { get; set; }
        public Ders Ders { get; set; }
        public string Gun { get; set; }
        public string DersSaati { get; set; }
    }
}
