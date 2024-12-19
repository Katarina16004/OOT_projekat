using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for DodajPost.xaml
    /// </summary>
    public partial class DodajPost : Window
    {
        private Korisnik k;
        public DodajPost(Korisnik k)
        {
            InitializeComponent();
            this.k = k;
            unesen.Focus();
        }
        
        private void unesen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(unesen.Text!="")
            {
                objavipost.IsEnabled=true;
            }
            else
            {
                objavipost.IsEnabled = false;
            }
        }

        private void objavipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int lajkovi = 0;
            string sadr=unesen.Text;
            int idp = File.ReadAllLines("fajlovi/Postovi.txt").Length + 1;
            int idk = k.Id;
            DateTime danas = DateTime.Now;
            string dat = danas.ToString("dd.MM.yyyy.");
            Post p = new Post(idp, idk, dat, lajkovi, sadr);

            k.DodajPost(p);
            p.ZapamtiPost();

            this.Close();

        }

        private void odustanipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
