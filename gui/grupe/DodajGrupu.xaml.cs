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
    public partial class DodajGrupu : Window
    {
        private Korisnik k;
        public DodajGrupu(Korisnik k)
        {
            InitializeComponent();
            this.k = k;
            naziv.Focus();
        }
        
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if(naziv.Text != "" && opis.Text != "")
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
            string naziv_grupe = naziv.Text;
            string opis_grupe = opis.Text;

            int idg = File.ReadAllLines("fajlovi/Grupe.txt").Length + 1;
            List<int> clanovi = new List<int>();

            clanovi.Add(k.Id);

            Grupa g = new Grupa(idg, naziv_grupe, opis_grupe, clanovi);

            k.DodajGrupu(g);
            g.ZapamtiGrupu();

            this.Close();
        }

        private void odustanipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void naziv_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void opis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
