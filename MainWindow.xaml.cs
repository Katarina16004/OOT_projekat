using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using projekat.models;

namespace projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point startPoint = new Point();
        private static Korisnik k;

        Post selectedPost;

        public MainWindow()
        {
            InitializeComponent();

            k = Korisnik.mojKorisnik();

            PodaciOKorisniku.DataContext = k;

            Uri resourceUri = new Uri(k.Profilna_slika, UriKind.RelativeOrAbsolute);
            slika.Source = new BitmapImage(resourceUri);

            this.DataContext = k;
            
            if (k.Profilna_slika == "fajlovi/image/user.jpg")
            {
                obrisisliku.IsEnabled = false;
            }
            else
                obrisisliku.IsEnabled=true;
            imeprikaz.Text = k.Ime;
            prezimeprikaz.Text = k.Prezime;

        }
        public static Korisnik MojKorisnik
        {
            get {
                if (k == null)
                    k = Korisnik.mojKorisnik();

                return k;
            }
        }

        private void dodaj_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DodajPost dodajpost = new DodajPost(MojKorisnik);
            dodajpost.ShowDialog();
            
        }

        private void obrisi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObrisiPost obisipost=new ObrisiPost(k,selectedPost);
            if(obisipost.ShowDialog()==true)
            {
                PostListView.SelectedItem = null;
                obrisi.IsEnabled = false;
                uredi.IsEnabled = false;
            }
            else
            {
                obrisi.IsEnabled = true;
                uredi.IsEnabled=true;
            }
        }

        private void PostListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Post)) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void uredi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UrediPost uredipost = new UrediPost(k, selectedPost);
            uredipost.ShowDialog();
        }

        private void PostListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Post)))
            {
                Post post = e.Data.GetData(typeof(Post)) as Post;
                if (post != null)
                {
                    // Pronađi novu poziciju
                    Point dropPosition = e.GetPosition(PostListView);
                    var listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem != null)
                    {
                        Post targetPost = (Post)PostListView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                        if (targetPost != null)
                        {
                            int oldIndex = k.Postovi.IndexOf(post);
                            int newIndex = k.Postovi.IndexOf(targetPost);

                            if (oldIndex != newIndex)
                            {
                                k.Postovi.Move(oldIndex, newIndex);
                                Post.PomeriPost(post.Id_posta, targetPost.Id_posta);
                            }
                        }
                    }
                }
            }
        }
      
        private void PostListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                try
                {
                    Post post = (Post)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject(post);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }catch(Exception ex) { }
        
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void PostListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        } 

        private void PostListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPost = (Post)PostListView.SelectedItem;

            obrisi.IsEnabled = true;
            uredi.IsEnabled = true;
        }

        private void slika_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PrikaziSliku prikaz=new PrikaziSliku(MojKorisnik);
            prikaz.ShowDialog();
        }

        private void obrisisliku_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObrisiSliku ob = new ObrisiSliku();
            if(ob.ShowDialog()==true)
            {
                MojKorisnik.Profilna_slika = "fajlovi/image/user.jpg";
                obrisisliku.IsEnabled = false;
                Uri resourceUri = new Uri("fajlovi/image/user.jpg", UriKind.RelativeOrAbsolute);
                slika.Source = new BitmapImage(resourceUri);
            }
            
        }

        private void izmenisliku_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PromeniSliku prikaz= new PromeniSliku(MojKorisnik);
            
            if(prikaz.ShowDialog() == true)
            {
                Uri resourceUri = new Uri(prikaz.Slika, UriKind.RelativeOrAbsolute);
                slika.Source = new BitmapImage(resourceUri);
                obrisisliku.IsEnabled = true;
            }
        }
    }
}