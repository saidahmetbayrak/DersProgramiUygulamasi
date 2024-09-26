using DersProgramiUygulamasi.Models;
using Microsoft.EntityFrameworkCore;

namespace DersProgramiUygulamasi.Services
{
    public class DersProgramiService
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();
        private readonly string[] Gunler = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma" };
        private readonly string[] Saatler = {
        "08:00 - 08:45",
        "09:00 - 09:45",
        "10:00 - 10:45",
        "11:00 - 11:45",
        "13:00 - 13:45",
        "14:00 - 14:45",
        "15:00 - 15:45"
    };

        public DersProgramiService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Öğretmen işlemleri
        public List<Ogretmen> GetOgretmenler()
        {
            return _context.Ogretmenler.ToList();
        }

        public Ogretmen GetOgretmenById(int id)
        {
            return _context.Ogretmenler.Find(id);
        }

        public void OgretmenEkle(Ogretmen ogretmen)
        {
            _context.Ogretmenler.Add(ogretmen);
            _context.SaveChanges();
        }

        public void OgretmenGuncelle(Ogretmen ogretmen)
        {
            _context.Ogretmenler.Update(ogretmen);
            _context.SaveChanges();
        }

        public void OgretmenSil(int id)
        {
            var ogretmen = _context.Ogretmenler.Find(id);
            if (ogretmen != null)
            {
                _context.Ogretmenler.Remove(ogretmen);
                _context.SaveChanges();
            }
        }

        // Ders işlemleri
        public List<Ders> GetDersler()
        {
            return _context.Dersler.ToList();
        }

        public Ders GetDersById(int id)
        {
            return _context.Dersler.Find(id);
        }

        public void DersEkle(Ders ders)
        {
            _context.Dersler.Add(ders);
            _context.SaveChanges();
        }

        public void DersGuncelle(Ders ders)
        {
            _context.Dersler.Update(ders);
            _context.SaveChanges();
        }

        public void DersSil(int id)
        {
            var ders = _context.Dersler.Find(id);
            if (ders != null)
            {
                _context.Dersler.Remove(ders);
                _context.SaveChanges();
            }
        }

        // Tüm ders programlarını sil
        public void TemizleDersProgrami()
        {
            var mevcutProgramlar = _context.DersProgramlari.ToList();
            _context.DersProgramlari.RemoveRange(mevcutProgramlar);
            _context.SaveChanges();
        }

        // Haftalık ders programını silip yeniden oluştur
        public void HaftalikProgramOlustur()
        {
            // Öncelikle tüm mevcut ders programlarını sil
            TemizleDersProgrami();

            var ogretmenler = _context.Ogretmenler.ToList();
            var dersler = _context.Dersler.ToList();

            foreach (var ogretmen in ogretmenler)
            {
                // Öğretmenin branşına uygun dersleri filtrele
                var uygunDersler = dersler.Where(d => d.Brans == ogretmen.Brans).ToList();

                if (!uygunDersler.Any())
                {
                    continue; // Eğer öğretmenin branşına uygun ders yoksa geç
                }

                for (int i = 0; i < ogretmen.ToplamDersSaati; i++)
                {
                    bool dersEklendi = false;

                    while (!dersEklendi)
                    {
                        // Rastgele bir gün seç
                        var rastgeleGun = Gunler[_random.Next(Gunler.Length)];
                        // Öğretmenin branşına uygun derslerden rastgele bir ders seç
                        var rastgeleDers = uygunDersler[_random.Next(uygunDersler.Count)];
                        // Rastgele bir ders saati seç
                        var rastgeleSaat = Saatler[_random.Next(Saatler.Length)];

                        // Aynı öğretmenin aynı gün ve aynı saat diliminde başka bir dersi olup olmadığını kontrol et
                        if (!_context.DersProgramlari.Any(dp => dp.OgretmenId == ogretmen.Id && dp.Gun == rastgeleGun && dp.DersSaati == rastgeleSaat))
                        {
                            // Ders programına yeni bir giriş ekle
                            var program = new DersProgrami
                            {
                                OgretmenId = ogretmen.Id,
                                DersId = rastgeleDers.Id,
                                Gun = rastgeleGun,
                                DersSaati = rastgeleSaat
                            };

                            _context.DersProgramlari.Add(program);
                            dersEklendi = true;
                        }
                    }
                }
            }

            _context.SaveChanges();
        }

        // Haftalık programı getir
        public List<DersProgrami> GetHaftalikDersProgrami()
        {
            return _context.DersProgramlari.Include(dp => dp.Ogretmen).Include(dp => dp.Ders).ToList();
        }
    }
}
