using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaLekow
{
    [Serializable]
    public class Lek : ICloneable
    {
        public string ID { get; set; }
        public string NazwaLeku { get; set; }
        public double DawkowanieRano { get; set; }
        public double DawkowaniePoludnie { get; set; }
        public double DawkowaniePopoludnie { get; set; }
        public double DawkowanieWieczor { get; set; }
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
