using System.Windows;
using ImageMagick;
using Color = System.Drawing.Color;

namespace ReceiptGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MagickNET.Initialize(@"D:\");
            MagickNET.SetTempDirectory(@"D:\Dokumenty\GitHub\ReceiptGen\ReceiptGen\bin\Debug\");
            Paragon par = new Paragon(72);
            par.Linie.Add(new Linia("Firma Handlowa \"BO-HEN\" Sp. Jawna", TypWyrowniania.DoSrodka, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));
            par.Linie.Add(new Linia("Bozena i Henryk Zimnol",  TypWyrowniania.DoSrodka, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));
            par.Linie.Add(new Linia("43-520 Chybie", TypWyrowniania.DoSrodka, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));

            par.Linie.Add(new Linia("Mnich, ul. Kopernika 18", TypWyrowniania.DoSrodka, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));

            par.Linie.Add(new Linia("NIP: 548-23-38-041", TypWyrowniania.DoLewej, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));
            par.Linie.Add(new Linia("19-09-2015;W047160", TypWyrowniania.DoKrawedzi, MiejsceLini.Naglowek, WielkoscLini.Srednia, GruboscLini.Normalna, 72));

            Drukuj(par.GetTytul);
        }

        private void Drukuj(string text)
        {
            var img = new MagickImage("image.jpg");
            using (var imgText = new MagickImage())
            {
                imgText.FontPointsize = 24;
                imgText.BackgroundColor = new MagickColor(Color.White);
                imgText.FillColor = new MagickColor(Color.Black);
                imgText.AntiAlias = true;
                imgText.FontFamily = "Fake Receipt";
                imgText.Read("label:" + text);
                img.Composite(imgText, Gravity.West);
            }
            image.Source = img.ToBitmapSource();
        }
    }
}
