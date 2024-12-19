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
    /// Interaction logic for ObrisiPost.xaml
    /// </summary>
    public partial class ObrisiGrupu : Window
    {
        private Korisnik k;
        private Grupa g;
        public ObrisiGrupu(Korisnik k,Grupa g)
        { 
            InitializeComponent();
            this.k = k;
            this.g = g;

            sadr.Text = "\""+g.Ime_grupe+"\""; 
            sadr.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void obrisipost_Click(object sender, RoutedEventArgs e)
        {   
            k.Grupe.Remove(g);
            g.ObrisiGrupu();

            DialogResult = true;
            this.Close();
        }

        private void odustanipost_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
