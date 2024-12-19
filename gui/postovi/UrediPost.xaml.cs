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
    /// Interaction logic for UrediPost.xaml
    /// </summary>
    public partial class UrediPost : Window
    {
        private Post p;
        Korisnik k;
        string pocetni;
        public UrediPost(Korisnik k,Post p)
        {
            InitializeComponent();
            this.p = p;
            this.k = k;

            unesen.Text = p.Sadrzaj;
            pocetni = p.Sadrzaj;
            unesen.Focus();
            unesen.Select(unesen.Text.Length, 0);
            objavipost.IsEnabled = false;
        }

        private void objavipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            p.Sadrzaj= unesen.Text;
            k.IzmeniPost(p);
            this.Close();
        }

        private void odustanipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void unesen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (unesen.Text != "" && unesen.Text!=pocetni)
            {
                objavipost.IsEnabled = true;
            }
            else
            { 
               objavipost.IsEnabled = false;
            }
        }
    }
}
