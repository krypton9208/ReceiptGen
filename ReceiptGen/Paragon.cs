using System;
using System.CodeDom;
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
    public enum VAT { A, B }
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

    public class Linia
    {
        private TypWyrowniania Wyrownaj { get; set; }
        private string Text { get; set; }
        private MiejsceLini Typ { get; set; }
        private WielkoscLini Rozmiar { get; set; }
        private GruboscLini Grubosc { get; set; }
        private int SzerokoscLini { get; set; }
        char znak = '-';

        public Linia(string text, TypWyrowniania wyrownaj, MiejsceLini typ, WielkoscLini rozmiar, GruboscLini grubosc, int szerokosclini)
        {
            Text = text;
            Wyrownaj = wyrownaj;
            Typ = typ;
            Rozmiar = rozmiar;
            Grubosc = grubosc;
            SzerokoscLini = szerokosclini;
        }

        private string Akcja
        {
            get
            {
                switch (Wyrownaj)
                {
                    case TypWyrowniania.DoLewej:
                        {
                            //string empty = String.Empty;
                            //empty += Text;
                            //for (int i = 0; i < SzerokoscLini - Text.Length; i++)
                            //{
                            //    empty += znak;
                            //}
                            //return empty;
                            return Text.PadRight(SzerokoscLini+2, znak);
                        }
                    case TypWyrowniania.DoPrawej:
                        {
                            //string empty = String.Empty;
                            //for (int i = 0; i < SzerokoscLini - Text.Length; i++)
                            //{
                            //    empty += znak;
                            //}
                            //return empty += Text;
                            return Text.PadLeft(SzerokoscLini+2, znak);
                        }
                    case TypWyrowniania.DoSrodka:
                        {
                            string empty = String.Empty;
                            int IlePoLewej = 0, IlePoPrawej = 0;
                            if (Text.Length % 2 == 0)
                            {
                                IlePoLewej = SzerokoscLini / 2;
                                IlePoLewej -= Text.Length / 2;
                                IlePoPrawej = IlePoLewej;
                            }
                            else
                            {
                                IlePoLewej = SzerokoscLini / 2;
                                IlePoLewej -= Text.Length / 2;
                                IlePoPrawej = IlePoLewej - 1;
                            }
                            for (int i = 0; i <= IlePoLewej; i++)
                            {
                                empty += znak;
                            }
                            empty += Text;
                            for (int i = 0; i <= IlePoPrawej; i++)
                            {
                                empty += znak;
                            }
                            return empty;
                        }
                    case TypWyrowniania.DoKrawedzi:
                        {
                            string empty = String.Empty;
                            for (int i = 0; i <= SzerokoscLini - Text.Length+2; i++)
                            {
                                empty += znak;
                            }
                            empty = Text.Replace(";", empty);
                            return empty;
                        }
                }
                return string.Empty;
            }
        }

        public string GetContent => Akcja;
    }

    public enum TypWyrowniania { DoLewej, DoPrawej, DoSrodka, DoKrawedzi }
    public enum MiejsceLini { Naglowek, Stopka }
    public enum WielkoscLini { Mala, Srednia, Duza }
    public enum GruboscLini { Normalna, Gruba }

    public class Paragon
    {
        private string Tytuł { get; set; }
        public int szerokoscParagonu { get; set; }
        public List<Linia> Linie { get; set; }
        public List<Zakupy> Lista { get; set; }



        public Paragon(int szer)
        {
            Lista = new List<Zakupy>();
            Linie = new List<Linia>();
            szerokoscParagonu = szer;
        }

        public double CenaParagonu
        {
            get
            {
                return Lista.Sum(item => item.GetCena);
            }
        }
        public string GetTytul
        {
            get
            {
                string empty = string.Empty;
                foreach (var item in Linie)
                {
                    empty += item.GetContent;
                    empty += "\n";
                }
                return empty;
            }
        }

    }
}
