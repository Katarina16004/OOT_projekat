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
    public partial class DodajDiskusiju : Window
    {
        private Korisnik k;
        private Grupa g;
        public DodajDiskusiju(Grupa g)
        {
            InitializeComponent();
            this.g = g;
            naziv.Focus();
        }
        
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if(naziv.Text != "")
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
            string naziv_diskusije = naziv.Text;

            int idg = File.ReadAllLines("fajlovi/Diskusije.txt").Length + 1;

            DateTime danas = DateTime.Now;
            string dat = danas.ToString("dd.MM.yyyy.");

            Diskusija diskusija = new Diskusija(idg, naziv_diskusije, dat, g.Id_grupe, 1);

            g.DodajDiskusiju(diskusija);
            diskusija.ZapamtiDiskusiju();

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
