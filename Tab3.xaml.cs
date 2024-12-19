using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using projekat.models;

namespace projekat
{
    /// <summary>
    /// Interaction logic for Tab3.xaml
    /// </summary>
    public partial class Tab3 : UserControl
    {
        private Grupa izabrana;
        private Diskusija izabranaDiskusija;

        public Tab3()
        {
            InitializeComponent();

            this.DataContext = MainWindow.MojKorisnik;
        }

        private void listagrupa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                izabrana = (Grupa)listagrupa.SelectedItem;
                listadisk.Visibility = Visibility.Visible;
                txtdisk.Visibility = Visibility.Visible;
                uredjivanje_diskusija.Visibility=Visibility.Visible;
                obrisigrupu.IsEnabled = true;
                izmenigrupu.IsEnabled=true;

                Diskusija.ucitajDiskusije(izabrana);
                listadisk.DataContext = izabrana;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

            }
            
        }

        private void listadisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            izabranaDiskusija = (Diskusija)listadisk.SelectedItem;

            izmenidiskusiju.IsEnabled = true;
            obrisidiskusiju.IsEnabled=true;
        }

        private void dodajgrupu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DodajGrupu dg = new DodajGrupu(MainWindow.MojKorisnik);
            dg.ShowDialog();

            this.DataContext = MainWindow.MojKorisnik;
        }

        private void obrisigrupu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (izabrana == null)
                return;

            ObrisiGrupu obisipost = new ObrisiGrupu(MainWindow.MojKorisnik, izabrana);
            if (obisipost.ShowDialog() == true)
            {
                listagrupa.SelectedItem = listagrupa.Items[0];
                obrisigrupu.IsEnabled = false;
                izmenigrupu.IsEnabled = false;
            }
            else
            {
                obrisigrupu.IsEnabled = true;
                izmenigrupu.IsEnabled = true;
            }
        }

        private void izmenigrupu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (izabrana == null)
                return;

            UrediGrupu urediGrupu = new UrediGrupu(MainWindow.MojKorisnik, izabrana);
            urediGrupu.ShowDialog();
        }

        private void dodajdiskusiju_Click(object sender, RoutedEventArgs e)
        {
            if (izabrana == null)
            {
                MessageBox.Show("Izaberite grupu");
                return;
            }

            DodajDiskusiju dd = new DodajDiskusiju(izabrana);
            dd.Show();
        }

        private void obrisidiskusiju_Click(object sender, RoutedEventArgs e)
        {
            if(izabranaDiskusija == null)
            {
                MessageBox.Show("Izaberite diskusiju");
                return;
            }

            ObrisiDiskusiju od = new ObrisiDiskusiju(izabrana, izabranaDiskusija);
            if (od.ShowDialog() == true)
            {
                listadisk.SelectedItem = listadisk.Items[0];
                obrisidiskusiju.IsEnabled = false;
                izmenidiskusiju.IsEnabled = false;
            }
            else
            {
                obrisidiskusiju.IsEnabled = true;
                izmenidiskusiju.IsEnabled = true;
            }
        }

        private void izmenidiskusiju_Click(object sender, RoutedEventArgs e)
        {
            if(izabranaDiskusija == null)
            {
                MessageBox.Show("Izaberite diskusiju");
                return;
            }

            UrediDiskusiju ud = new UrediDiskusiju(izabrana, izabranaDiskusija);
            ud.Show();
        }
    }
}
