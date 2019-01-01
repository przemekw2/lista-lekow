using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaLekow
{
    public class DaneOsobowe
    {

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string Adres { get; set; }

        public DaneOsobowe(string imie, string nazwisko, string pesel, string adres)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Adres = adres;
        }
    }
}
