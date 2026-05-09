using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour
{
    internal class Versenyzo
    {
        public Versenyzo(int szakasz, string ido, string nev, string nemzetiseg, string csapat)
        {
            Szakasz = szakasz;
            Ido = ido;
            Nev = nev;
            Nemzetiseg = nemzetiseg;
            Csapat = csapat;
        }

        public int Szakasz {  get; set; }
        public string Ido {  get; set; }
        public string Nev {  get; set; }
        public string Nemzetiseg {  get; set; }
        public string Csapat {  get; set; }

        public int Masodperc()
        {
            string[] resz = Ido.Split(':');
            return 3600 * Convert.ToInt32(resz[0]) + 60 * Convert.ToInt32(resz[1]) + Convert.ToInt32(resz[2]);
        }

        public bool Kituntetes()
        {
            return Nemzetiseg == "USA" && Masodperc() < 3 * 3600;
        }
    }
}
