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
    public partial class ObrisiDiskusiju : Window
    {
        private Grupa g;
        private Diskusija diskusija;
        public ObrisiDiskusiju(Grupa g, Diskusija diskusija)
        { 
            InitializeComponent();
            this.g = g;
            this.diskusija = diskusija;

            sadr.Text = "\""+diskusija.Naziv_diskusije+"\""; 
            sadr.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void obrisipost_Click(object sender, RoutedEventArgs e)
        {
            g.Diskusije.Remove(diskusija);
            diskusija.ObrisiDiskusuju();

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
