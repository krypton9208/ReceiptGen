using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReceiptGen
{
    public enum VAT {A, B } 
    public class Zakupy
    {
        private double ilosc { get; set; }
        private string Nazwa { get; set; }
        private double Cena { get; set; }
        private VAT Podatek { get; set; }
        public Zakupy(int il, string name, double cena, VAT pod)
        {
            ilosc = il;
            Nazwa = name;
            Cena = cena;
            Podatek = pod;
        }

        public double GetCena
        {
            get { return Cena; }
        }
    }
    public class Paragon
    {
        private string Tytuł { get; set; }
        public int szerokoscParagonu { get; set; }
        private string WyrownajNaglowek(string text)
        {
            char znak  = '-';
            string temp1 = "";
            if (text.Length % 2 == 0)
            {
                int ilespacji = szerokoscParagonu - text.Length;
                ilespacji -= text.Length/2;
                ilespacji /= 2;
                for (int i = 0; i < ilespacji; i++)
                {
                    temp1 += znak;
                }
                temp1 += text;
                for (int i = 0; i < ilespacji; i++)
                {
                    temp1 += znak;
                }
            }
            else
            {
                int ilespacji = szerokoscParagonu - text.Length;
                ilespacji -= text.Length / 2;
                ilespacji /= 2;
                ilespacji++;
                for (int i = 0; i < ilespacji; i++)
                {
                    temp1 += znak;
                }
                temp1 += text;
                for (int i = 0; i < ilespacji - 1; i++)
                {
                    temp1 += znak;
                }
            }
            return temp1;
        }
        private List<Zakupy> Lista { get; set; } 
        public void DodajTytuł(string linia1, string linia2, string linia3, string linia4, string linia5,string linia6)
        {
            szerokoscParagonu = 72;
            Tytuł = WyrownajNaglowek(linia1) + "\n" + WyrownajNaglowek(linia2) + "\n" + WyrownajNaglowek(linia3) +
                "\n" + WyrownajNaglowek(linia4) + "\n" + WyrownajNaglowek(linia5) + "\n" + WyrownajNaglowek(linia6);
        }

        public Paragon(int szer)
        {
            Lista = new List<Zakupy>();
            szerokoscParagonu = szer;
        }

        public double CenaParagonu
        {
            get
            {
                double temp = 0;
                foreach (var item in Lista)
                {
                    temp += item.GetCena;
                }
                return temp;
            }
        }
        public string GetTytul
        {
            get { return Tytuł; }
        }

    }
}
