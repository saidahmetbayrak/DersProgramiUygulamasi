using DersProgramiUygulamasi.Models;
using Microsoft.EntityFrameworkCore;

namespace DersProgramiUygulamasi.Services
{
    public class DersProgramiService
    {
        private readonly ApplicationDbContext _context;
        private static readonly string[] Gunler = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma" };
        private static readonly Random _random = new Random();

        public DersProgramiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void HaftalikProgramOlustur()
        {
            var ogretmenler = _context.Ogretmenler.ToList();
            var dersler = _context.Dersler.ToList();

            foreach (var ogretmen in ogretmenler)
            {
                for (int i = 0; i < ogretmen.ToplamDersSaati; i++)
                {
                    var rastgeleGun = Gunler[_random.Next(Gunler.Length)];
                    var rastgeleDers = dersler[_random.Next(dersler.Count)];

                    if (!_context.DersProgramlari.Any(dp => dp.OgretmenId == ogretmen.Id && dp.Gun == rastgeleGun && dp.DersId == rastgeleDers.Id))
                    {
                        var program = new DersProgrami
                        {
                            OgretmenId = ogretmen.Id,
                            DersId = rastgeleDers.Id,
                            Gun = rastgeleGun
                        };

                        _context.DersProgramlari.Add(program);
                    }
                }
            }

            _context.SaveChanges();
        }

        public async Task<List<DersProgrami>> GetHaftalikDersProgramiAsync()
        {
            return await _context.DersProgramlari.Include(dp => dp.Ogretmen).Include(dp => dp.Ders).ToListAsync();
        }

        public async Task AddOgretmenAsync(Ogretmen ogretmen)
        {
            _context.Ogretmenler.Add(ogretmen);
            await _context.SaveChangesAsync();
        }

        public async Task AddDersAsync(Ders ders)
        {
            _context.Dersler.Add(ders);
            await _context.SaveChangesAsync();
        }
    }

}
