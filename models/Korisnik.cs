using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media.Animation;

namespace projekat.models
{
    public class Korisnik : INotifyPropertyChanged
    {
        //Id glavnog korisnika
        private string mojKorisnikID = "1";

        private int id;
        private string ime;
        private string prezime;
        private string datum_rodjenja;
        private string profilna_slika;
        private ObservableCollection<Post> postovi = new ObservableCollection<Post>();
        private ObservableCollection<Grupa> grupe = new ObservableCollection<Grupa>();

        private int prijateljiCount;
        private int objaveCount;

        public bool IsFriend;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<int> friends = new ObservableCollection<int>();
        private Korisnik(int id, string ime, string prezime, string datum_rodjenja, string profilna_slika)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.datum_rodjenja = datum_rodjenja;
            this.profilna_slika = profilna_slika;

            Friends.CollectionChanged += ListaPrijateljaPromenjena;
            Postovi.CollectionChanged += ListaObjavaPromenjena;
        }

        private void ListaPrijateljaPromenjena(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                PrijateljiCount += e.NewItems.Count;
            }

            if (e.OldItems != null)
            {
                PrijateljiCount -= e.OldItems.Count;
            }
        }

        private void ListaObjavaPromenjena(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                ObjaveCount += e.NewItems.Count;
            }

            if (e.OldItems != null)
            {
                ObjaveCount -= e.OldItems.Count;
            }
        }

        public ObservableCollection<Post> Postovi
        {
            get
            {
                return postovi;
            }
            set
            {
                if (postovi != value)
                {
                    postovi = value;

                    OnPropertyChanged("Postovi");
                }

            }
        }
        public ObservableCollection<Grupa> Grupe
        {
            get
            {
                return grupe;
            }
            set
            {
                if (grupe != value)
                {
                    grupe = value;

                    OnPropertyChanged("Grupe");
                }

            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Ime
        {
            get { return ime; }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        public string Prezime
        {
            get { return prezime; }
            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }
        public string Datum_rodjenja
        {
            get { return datum_rodjenja; }
            set
            {
                if (datum_rodjenja != value)
                {
                    datum_rodjenja = value;
                    OnPropertyChanged("Datum_rodjenja");
                }
            }
        }
        public string Profilna_slika
        {
            get { return profilna_slika; }
            set
            {
                if (profilna_slika != value)
                {
                    profilna_slika = value;
                    OnPropertyChanged("Profilna_slika");
                }
            }
        }

        public ObservableCollection<int> Friends
        {
            get { return friends; }
            set
            {
                if (friends != value)
                {
                    friends = value;
                    OnPropertyChanged("Friends");
                }
            }
        }

        public int ObjaveCount
        {
            get { return objaveCount; }
            set
            {
                if (objaveCount != value)
                {
                    objaveCount = value;
                    OnPropertyChanged("ObjaveCount");
                }
            }
        }

        public int PrijateljiCount
        {
            get { return prijateljiCount; }
            set
            {
                if (prijateljiCount != value)
                {
                    prijateljiCount = value;
                    OnPropertyChanged("PrijateljiCount");
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));

            UpdateKorisnika();
        }

#pragma warning disable CS8603 // Possible null reference return.



        private bool IdPostaPostoji(int idp)
        {
            foreach (Post p in postovi)
            {
                if (p.Id_posta == idp)
                {
                    return true;
                }
            }
            return false;
        }

        public string IspisiPostove()
        {
            string s = "";
            foreach (Post post in postovi)
            {
                s = s + post + "\n";
            }
            return s;
        }

        public void DodajPost(Post p)
        {
            if (!IdPostaPostoji(p.Id_posta))
            {
                postovi.Add(p);

                OnPropertyChanged("Postovi");
            }
        }
        public void ObrisiPost(Post p)
        {
            postovi.Remove(p);
        }

        public void DodajGrupu(Grupa g)
        {
            grupe.Add(g);

            OnPropertyChanged("Grupe");
        }

        public void ObrisiGrupu(Grupa g)
        {
            grupe.Remove(g);

            OnPropertyChanged("Grupe");
        }


        public static Korisnik ucitajKorisnika(string id)
        {
            string filename = "fajlovi/korisnici/" + id + ".txt";

            try
            {
                if (!File.Exists(filename))
                {
                    return null;
                }

                var textLines = File.ReadAllLines(filename).ToList();

                // Prva linija su podaci o korisniku
                var podaciOKorisniku = textLines[0].Split(",");

                Korisnik k = new Korisnik(int.Parse(podaciOKorisniku[0]),
                                            podaciOKorisniku[1],
                                            podaciOKorisniku[2],
                                            podaciOKorisniku[3],
                                            podaciOKorisniku[4]);

                foreach (string line in textLines)
                {
                    var podaci = line.Split(",");

                    if (podaci[0].Equals("f"))
                    {
                        k.friends.Add(int.Parse(podaci[1]));
                    }
                }

                Post.ucitajPostove(k);

                List<Grupa> sveGrupe = Grupa.ucitajSveGrupe();

                foreach(Grupa g in sveGrupe)
                {
                    k.DodajGrupu(g);
                }

                return k;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return null;

            }
        }

        private void UpdateKorisnika()
        {
            string filename = "fajlovi/korisnici/" + id + ".txt";

            try
            {
                if (!File.Exists(filename))
                {
                    return;
                }

                List<string> lines = new List<string>();

                lines.Add(ToString());

                foreach (int id in friends)
                {
                    lines.Add("f," + id + "\n");
                }

                File.WriteAllLines(filename, lines.ToArray());

                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return;

            }
        }

        public static Korisnik mojKorisnik()
        {
            return ucitajKorisnika("1");
        }

        public static List<Korisnik> sviKorisnici()
        {
            List<Korisnik> l = new List<Korisnik>();

            int n = Directory.GetFiles("fajlovi/korisnici", "*", SearchOption.TopDirectoryOnly).Length;

            for (int i = 1; i <= n; ++i)
            {
                l.Add(ucitajKorisnika("" + i));
            }

            return l;
        }

#pragma warning restore CS8603 // Possible null reference return.

        public void IzmeniPost(Post p)
        {
            foreach (Post post in postovi)
            {
                if (post.Id_posta == p.Id_posta)
                {
                    post.Sadrzaj = p.Sadrzaj;
                    post.updatePost();
                }
            }
        }

        public void IzmeniGrupu(Grupa grupa)
        {
            foreach (Grupa g in grupe)
            {
                if (g.Id_grupe == grupa.Id_grupe)
                {
                    g.Ime_grupe = grupa.Ime_grupe;
                    g.Opis_grupe = grupa.Opis_grupe;
                    g.updateGrupu();
                }
            }
        }

        public string ToString()
        {
            return "" + id + "," +
                        ime + "," +
                        prezime + "," +
                        datum_rodjenja + "," +
                        profilna_slika + "\n";
        }
    }
}
