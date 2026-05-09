using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour
{
    internal class Feladat
    {
        List<Versenyzo> adatok = [];
        public Feladat()
        {
            foreach (var item in File.ReadAllLines("tour.csv",Encoding.UTF8).Skip(1))
            {
                string[] resz = item.Split(';');
                int szakasz = Convert.ToInt32(resz[0]);
                string ido = resz[1];
                string nev = resz[2];
                string nemzetiseg = resz[3];
                string csapat = resz[4];
                adatok.Add(new(szakasz, ido, nev, nemzetiseg, csapat));
            }
        }

        public void Feladat4()
        {
            int eredmeny = adatok.Count(x => x.Szakasz == 5);
            Console.WriteLine($"4. Feladat: 5-ös szakasz versenyzői: {eredmeny}");
        }

        public void Feladat5()
        {
            double atlag = adatok.Where(x=>x.Szakasz==5).Average(x => x.Masodperc());
            var eredmeny = adatok.Count(x => x.Masodperc() > atlag && x.Szakasz == 5);
            Console.WriteLine($"5. Feladat: Átlag felett: {eredmeny} fő");
        }

        public void Feladat6()
        {
            Console.WriteLine("6. Feladat: Csapatonkénti átlagos idő");
            foreach (var item in adatok.OrderBy(x=>x.Csapat).GroupBy(x=>x.Csapat))
            {
                Console.WriteLine($"{item.Key}: {item.Average(x=>x.Masodperc()):n1}");
            }
            {

            }
        }

        public void Feladat7()
        {
            var eredmeny = adatok.Count(x=>x.Kituntetes());
            Console.WriteLine($"7. feladat: Kitüntetett versenyzők: {eredmeny}");
        }

        public void Feladat8()
        {
            Console.WriteLine("8. feladat: Top 10");
            foreach (var item in adatok.Where(x=>x.Szakasz==5).OrderBy(x=>x.Masodperc()).Take(10))
            {
                Console.WriteLine($"{item.Nev}, {item.Ido}");
            }
        }
    }
}
