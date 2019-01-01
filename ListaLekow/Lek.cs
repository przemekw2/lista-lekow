using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaLekow
{
    [Serializable]
    public class Lek
    {
        public string ID { get; set; }
        public string NazwaLeku { get; set; }
        public double DawkowanieRano { get; set; } = 0.0;
        public double DawkowaniePoludnie { get; set; } = 0.0;
        public double DawkowaniePopoludnie { get; set; } = 0.0;
        public double DawkowanieWieczor { get; set; } = 0.0;
        //public Dawkowanie Dawkowanie { get; set; }
        public byte Ilosc { get; set; }
        public bool Wydruk { get; set; }

        public Lek(string id, string nazwaleku, byte ilosc, bool wydruk, double dawkowanierano = 0.0, double dawkowaniepoludnie = 0.0, double dawkowaniepopoludnie = 0.0, double dawkowaniewieczor = 0.0)
        {
            this.ID = id;
            this.NazwaLeku = nazwaleku;
            this.DawkowanieRano = dawkowanierano;
            this.DawkowaniePoludnie = dawkowaniepoludnie;
            this.DawkowaniePopoludnie = dawkowaniepopoludnie;
            this.DawkowanieWieczor = dawkowaniewieczor;
            this.Ilosc = ilosc;
            this.Wydruk = wydruk;
        }

    }
}
