using Accessibility;
using projekat.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace projekat
{
    /// <summary>
    /// Interaction logic for Tab2.xaml
    /// </summary>
    /// 

    public partial class Tab2 : UserControl
    {

        private Korisnik? izabrani;

        private ObservableCollection<Korisnik> prijatelji;

        public Tab2()
        {
            InitializeComponent();

            Korisnik k = MainWindow.MojKorisnik;

            prijatelji = new ObservableCollection<Korisnik>();

            List<Korisnik> sviKorisnici = Korisnik.sviKorisnici();


            foreach(Korisnik korisnik in sviKorisnici)
            {
                if (korisnik == null)
                    continue;

                if (!k.Friends.Contains(korisnik.Id))
                    continue;

                prijatelji.Add(korisnik);
            }

            izabrani = prijatelji[0];

            profilePreview.DataContext = izabrani;
            profilePreview.Visibility = Visibility.Visible;
            SearchResult.Visibility = Visibility.Collapsed;
            friendList.ItemsSource = prijatelji;
        }

        private ListViewItem ItemZaKorsnika(Korisnik k)
        {
            ListViewItem item = new ListViewItem();
            item.Padding = new Thickness(5);

            DockPanel panel = new DockPanel();
            panel.LastChildFill = true;

            Image img = new Image();

            img.Source = new BitmapImage(new Uri(k.Profilna_slika, UriKind.Relative));
            img.Width = 30;

            panel.Children.Add(img);

            TextBlock ime_prezime = new TextBlock();
            ime_prezime.Text = k.Ime + " " + k.Prezime;
            ime_prezime.VerticalAlignment = VerticalAlignment.Center;
            ime_prezime.Margin = new Thickness(5, 0, 0, 0);

            panel.Children.Add(ime_prezime);

            if (k.IsFriend)
            {
                TextBlock prijatelj = new TextBlock();
                prijatelj.Text = "Vec ste prijatelji";
                prijatelj.Foreground = Brushes.Gray;
                prijatelj.HorizontalAlignment = HorizontalAlignment.Right;
                prijatelj.VerticalAlignment = VerticalAlignment.Center;
                panel.Children.Add(prijatelj);
            }
            else
            {
                Button dodajPrijatelja = new Button();
                dodajPrijatelja.Content = "Dodaj prijatelja";
                dodajPrijatelja.Tag = k.Id;
                dodajPrijatelja.HorizontalAlignment = HorizontalAlignment.Right;
                dodajPrijatelja.MouseDoubleClick += dodajPrijateljaFunc;

                panel.Children.Add(dodajPrijatelja);
            }

            item.Content = panel;

            return item;
        }

        private void dodajPrijateljaFunc(object sender, EventArgs e)
        {
            int novi_id = (int)((Button)sender).Tag;

            MainWindow.MojKorisnik.Friends.Add(novi_id);

            Korisnik noviKorisnik = Korisnik.ucitajKorisnika(""+novi_id);

            prijatelji.Add(noviKorisnik);

            UpdateSearchList(SearchBar.Text);
        }

        private void HandleMouseClick(object sender, MouseButtonEventArgs e)
        {
            SearchResult.Visibility = Visibility.Collapsed;
            if (izabrani != null)
            {
                if ((sender as TreeViewItem).Header.Equals(izabrani.Ime + " " + izabrani.Prezime))
                {
                    profilePreview.Visibility = (profilePreview.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
                    
                }
                else
                {
                    if (profilePreview.Visibility == Visibility.Collapsed)
                        profilePreview.Visibility = Visibility.Visible;

                    TreeViewItem tv = (TreeViewItem)sender;

                    int tag = (int)((TreeViewItem)sender).Tag;
                    Korisnik noviIzabrani = prijatelji.FirstOrDefault(k => k.Id == tag);

                    profilePreview.DataContext = noviIzabrani;
                    izabrani = noviIzabrani;
                }
            }
            else
            {
                TreeViewItem tv = (TreeViewItem)sender;
                int tag = (int)((TreeViewItem)sender).Tag;
                Korisnik noviIzabrani = prijatelji.FirstOrDefault(k => k.Id == tag);

                profilePreview.DataContext = noviIzabrani;
                izabrani = noviIzabrani;

                profilePreview.Visibility = Visibility.Visible;
            }
        }

        private void UkloniPrijatelja(object sender, MouseButtonEventArgs e)
        {
            int tag = (int)((Button)sender).Tag;
            Korisnik zaUkloniti = prijatelji.FirstOrDefault(k => k.Id == tag);
            prijatelji.Remove(zaUkloniti);

            MainWindow.MojKorisnik.Friends.Remove(tag);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;

            SearchResult.Visibility = Visibility.Visible;
            profilePreview.Visibility = Visibility.Collapsed;

            UpdateSearchList(text);
        }

        private void UpdateSearchList(string text)
        {
            SearchResult.Items.Clear();

            if (text.Trim().Length == 0)
            {
                return;
            }

            List<Korisnik> sviKorisnici = Korisnik.sviKorisnici();

            int i = 0;

            foreach (Korisnik korisnik in sviKorisnici)
            {
                if (korisnik == null)
                    continue;

                if (korisnik.Id == MainWindow.MojKorisnik.Id)
                    continue;

                if ((korisnik.Ime + korisnik.Prezime).Contains(text))
                {
                    if (MainWindow.MojKorisnik.Friends.Contains(korisnik.Id))
                        korisnik.IsFriend = true;

                    SearchResult.Items.Add(ItemZaKorsnika(korisnik));
                }
            }

            List<Post> sviPostovi = Post.ucitajSvePostove();

            foreach (Post post in sviPostovi)
            {
                if (post.Sadrzaj.Contains(text) && MainWindow.MojKorisnik.Friends.Contains(post.Id_korisnika))
                {
                    ListViewItem item = new ListViewItem();

                    item.Content = post.Id_posta + " - " + post.Sadrzaj;

                    SearchResult.Items.Add(item);
                }
            }
        }

    }
}
