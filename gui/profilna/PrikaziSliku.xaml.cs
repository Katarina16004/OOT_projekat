using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using projekat.models;

namespace projekat
{
    /// <summary>
    /// Interaction logic for PrikaziSliku.xaml
    /// </summary>
    public partial class PrikaziSliku : Window
    {
        public PrikaziSliku(Korisnik k)
        {
            InitializeComponent();
            Uri resourceUri = new Uri(k.Profilna_slika, UriKind.Relative);
            prikazImage.ImageSource= new BitmapImage(resourceUri);

        }
    }
}
