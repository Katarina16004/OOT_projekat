using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media.TextFormatting;

namespace projekat.models
{
    public class Post : INotifyPropertyChanged
    {
        private int id_posta;
        private int id_korisnika;
        private string datum_objave;
        private int lajkovi;
        private string sadrzaj;

        public event PropertyChangedEventHandler PropertyChanged;
        public Post(int id_posta, int id_korisnika, string datum_objave, int lajkovi, string sadrzaj)
        {
            this.id_posta = id_posta;
            this.id_korisnika = id_korisnika;
            this.datum_objave = datum_objave;
            this.lajkovi = lajkovi;
            this.sadrzaj = sadrzaj;
        }
        public int Id_posta
        {
            get { return id_posta; }
            set
            {
                if (id_posta != value)
                {
                    id_posta = value;
                    OnPropertyChanged("Id_posta");
                }
            }
        }
        public int Id_korisnika
        {
            get { return id_korisnika; }
            set
            {
                if (id_korisnika != value)
                {
                    id_korisnika = value;
                    OnPropertyChanged("Id_korisnika");
                }
            }
        }
        public int Lajkovi
        {
            get { return lajkovi; }
            set
            {
                if (lajkovi != value)
                {
                    // Komnetar
                    lajkovi = value;
                    OnPropertyChanged("Lajkovi");
                }
            }
        }
        public string Sadrzaj
        {
            get { return sadrzaj; }
            set
            {
                if (sadrzaj != value)
                {
                    sadrzaj = value;
                    OnPropertyChanged("Sadrzaj");
                }
            }
        }
        public string Datum_objave
        {
            get { return datum_objave; }
            set
            {
                if (datum_objave != value)
                {
                    datum_objave = value;
                    OnPropertyChanged("Datum_objave");
                }
            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
#pragma warning disable CS8603 // Possible null reference return.

        public static Post ucitajPost(string id)
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return null;
                }

                var textLines = File.ReadAllLines(filename).ToList();

                foreach (string line in textLines)
                {
                    string[] podaci = line.Split(",");

                    // Proverimo id u svakoj liniji
                    if (podaci[0].Equals(id))
                    {
                        // Pronasli smo liniju koja nam treba
                        // Vratimo post sa podacima
                        return new Post(int.Parse(podaci[0]), int.Parse(podaci[1]), podaci[2], int.Parse(podaci[3]), podaci[4]);
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                return null;
            }
        }

        public static void ucitajPostove(Korisnik k)
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }

                var textLines = File.ReadAllLines(filename).ToList();

                foreach (string line in textLines)
                {
                    string[] podaci = line.Split(",");

                    // Proverimo id u svakoj liniji
                    if (podaci[1].Equals("" + k.Id))
                    {
                        // Pronasli smo liniju koja nam treba
                        // Vratimo post sa podacima
                        k.Postovi.Add(new Post(int.Parse(podaci[0]), int.Parse(podaci[1]), podaci[2], int.Parse(podaci[3]), podaci[4]));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static List<Post> ucitajSvePostove()
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return null;
                }

                List<Post> sviPostovi = new List<Post>();

                var textLines = File.ReadAllLines(filename).ToList();

                for (int i = 1; i < textLines.Count; ++i)
                {
                    string[] podaci = textLines[i].Split(",");

                    sviPostovi.Add(new Post(int.Parse(podaci[0]), int.Parse(podaci[1]), podaci[2], int.Parse(podaci[3]), podaci[4]));
                }

                return sviPostovi;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return null;
        }

        public void updatePost()
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                int idx = textLines.FindIndex(p => p.Substring(0, p.IndexOf(",")).Equals("" + id_posta));

                textLines[idx] = ToString();

                File.WriteAllLines(filename, textLines);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void PomeriPost(int id1, int id2)
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                int idx1 = textLines.FindIndex(p => p.Substring(0, p.IndexOf(",")).Equals("" + id1));
                int idx2 = textLines.FindIndex(p => p.Substring(0, p.IndexOf(",")).Equals("" + id2));

                string tmp = textLines[idx1];
                textLines.Remove(textLines[idx1]);
                textLines.Insert(idx2, tmp);

                File.WriteAllLines(filename, textLines);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ZapamtiPost()
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                textLines.Add(ToString());

                File.WriteAllLines(filename, textLines);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ObrisiPost()
        {
            try
            {
                string filename = "fajlovi/Postovi.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                textLines.Remove(ToString());

                File.WriteAllLines(filename, textLines);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

#pragma warning restore CS8603 // Possible null reference return.

        public override string ToString()
        {
            string s = "" + id_posta + "," +
                            id_korisnika + "," +
                            datum_objave + "," +
                            lajkovi + "," +
                            sadrzaj;
            return s;
        }
    }
}
