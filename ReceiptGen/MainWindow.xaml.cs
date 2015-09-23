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
            par.DodajTytuł("Firma Handlowa \"BO-HEN\" Sp. Jawna", "Bożena i Henryk Zimnol", "43-520 Chybie",
                "Mnich, ul. Kopernika 18", "NIP: 548-23-38-041", "19-09-2015                    W047160");  
            Drukuj(par.GetTytul);
        }

        private void Drukuj(string text)
        {
            var img = new MagickImage("image.jpg");
            using (var imgText = new MagickImage())
            {
                imgText.FontPointsize = 36;
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
