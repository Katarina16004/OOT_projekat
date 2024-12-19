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
    public partial class ObrisiPost : Window
    {
        private Korisnik k;
        private Post p;
        public ObrisiPost(Korisnik k,Post p)
        { 
            InitializeComponent();
            this.k = k;
            this.p = p;
            sadr.Text = "\""+p.Sadrzaj+"\""; 
            sadr.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void obrisipost_Click(object sender, RoutedEventArgs e)
        {   
            k.Postovi.Remove(p);
            p.ObrisiPost();
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
