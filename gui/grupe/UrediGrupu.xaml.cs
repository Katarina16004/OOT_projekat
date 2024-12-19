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
    public partial class UrediGrupu : Window
    {
        private Korisnik k;
        private Grupa grupa;
        public UrediGrupu(Korisnik k, Grupa grupa)
        {
            InitializeComponent();
            this.k = k;
            this.grupa = grupa;

            naziv.Text = grupa.Ime_grupe;
            opis.Text = grupa.Opis_grupe;

            naziv.Focus();
        }
        
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void objavipost_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            grupa.Ime_grupe = naziv.Text;
            grupa.Opis_grupe = opis.Text;

            k.IzmeniGrupu(grupa);

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
