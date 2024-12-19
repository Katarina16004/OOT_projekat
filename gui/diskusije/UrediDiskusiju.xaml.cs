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
    public partial class UrediDiskusiju : Window
    {
        private Grupa grupa;
        private Diskusija diskusija;
        public UrediDiskusiju(Grupa grupa, Diskusija diskusija)
        {
            InitializeComponent();
            this.diskusija = diskusija;
            this.grupa = grupa;

            naziv.Text = diskusija.Naziv_diskusije;

            naziv.Focus();
        }
        
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            objavipost.IsEnabled = naziv.Text != "";
        }

        private void objavipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            diskusija.Naziv_diskusije = naziv.Text;

            grupa.IzmeniDiskusiju(diskusija);

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
