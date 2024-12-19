using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using projekat.models;

namespace projekat
{
    /// <summary>
    /// Interaction logic for PromeniSliku.xaml
    /// </summary>
    public partial class PromeniSliku : Window
    {
        Korisnik k;
        public string Slika 
        {
            get;set;
        }
        public PromeniSliku(Korisnik k)
        {
            InitializeComponent();
            this.k = k;
            Uri resourceUri = new Uri("fajlovi/image/musko1.jpg", UriKind.Relative);
            ImageBrush brush = new ImageBrush(new BitmapImage(resourceUri));
            musko1slika.Background = brush;
            Uri resourceUri2 = new Uri("fajlovi/image/musko2.jpg", UriKind.Relative);
            ImageBrush brush2 = new ImageBrush(new BitmapImage(resourceUri2));
            musko2slika.Background = brush2;
            Uri resourceUri3 = new Uri("fajlovi/image/musko3.jpg", UriKind.Relative);
            ImageBrush brush3 = new ImageBrush(new BitmapImage(resourceUri3));
            musko3slika.Background = brush3;
            Uri resourceUri4 = new Uri("fajlovi/image/zensko1.jpg", UriKind.Relative);
            ImageBrush brush4 = new ImageBrush(new BitmapImage(resourceUri4));
            zensko1slika.Background = brush4;
            Uri resourceUri5 = new Uri("fajlovi/image/zensko2.jpg", UriKind.Relative);
            ImageBrush brush5 = new ImageBrush(new BitmapImage(resourceUri5));
            zensko2slika.Background = brush5;
            Uri resourceUri6 = new Uri("fajlovi/image/zensko3.jpg", UriKind.Relative);
            ImageBrush brush6 = new ImageBrush(new BitmapImage(resourceUri6));
            zensko3slika.Background = brush6;
            Uri resourceUri7 = new Uri("fajlovi/image/zensko4.jpg", UriKind.Relative);
            ImageBrush brush7 = new ImageBrush(new BitmapImage(resourceUri7));
            zensko4slika.Background = brush7;
            this.DataContext = k;
        }

        private void musko1slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           Slika = "fajlovi/image/musko1.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void musko2slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/musko2.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void musko3slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/musko3.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void zensko1slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/zensko1.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void zensko2slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/zensko2.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void zensko3slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/zensko3.jpg";
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void zensko4slika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Slika = "fajlovi/image/zensko4.jpg"; 
            string[] naziv = Slika.Split("/");
            MessageBox.Show("Izabrali ste sliku \"" + naziv[2] + "\"");
        }

        private void odustani_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void postavi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            k.Profilna_slika = Slika;
            this.DialogResult = true;
            this.Close();
        }

        private void izaberiSaRacunara_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Slika = filename;
                //MessageBox.Show("Izabrali ste sliku \"" + Slika + "\"");
            }
        }
    }
}
