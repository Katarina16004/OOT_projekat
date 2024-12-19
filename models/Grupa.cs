using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekat.models
{
    public class Grupa : INotifyPropertyChanged
    {
        private int id_grupe;
        private string ime_grupe;
        private string opis_grupe;
        private List<int> clanovi;
        private ObservableCollection<Diskusija> diskusije = new ObservableCollection<Diskusija>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Grupa(int id_grupe, string ime_grupe, string opis_grupe, List<int> clanovi)
        {
            this.id_grupe = id_grupe;
            this.ime_grupe = ime_grupe;
            this.opis_grupe = opis_grupe;
            this.clanovi = clanovi;
        }
        public ObservableCollection<Diskusija> Diskusije
        {
            get { return diskusije; }
            set
            {
                if (diskusije != value)
                {
                    diskusije = value;
                    OnPropertyChanged("Diskusije");
                }
            }
        }
        public int Id_grupe
        {
            get { return id_grupe; }
            set
            {
                if (id_grupe != value)
                {
                    id_grupe = value;
                    OnPropertyChanged("Id_grupe");
                }
            }
        }
        public string Ime_grupe
        {
            get { return ime_grupe; }
            set
            {
                if (ime_grupe != value)
                {
                    ime_grupe = value;
                    OnPropertyChanged("Ime_grupe");
                }
            }
        }
        public string Opis_grupe
        {
            get { return opis_grupe; }
            set
            {
                if (opis_grupe != value)
                {
                    opis_grupe = value;
                    OnPropertyChanged("Opis_grupe");
                }
            }
        }
        public List<int> Clanovi
        {
            get { return clanovi; }
            set
            {
                if (clanovi != value)
                {
                    clanovi = value;
                    OnPropertyChanged("Clanovi");
                }
            }
        }
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

#pragma warning disable CS8603 // Possible null reference return.

        public static Grupa ucitajGrupu(string id)
        {
            try
            {
                string filename = "fajlovi/Grupe.txt";

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
                        // Vratimo grupu sa podacima

                        List<int> korsinici = new List<int>();

                        for (int i = 3; i < podaci.Length; ++i)
                        {
                            korsinici.Add(int.Parse(podaci[i]));
                        }

                        return new Grupa(int.Parse(podaci[0]), podaci[1], podaci[2], korsinici);
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

        public static List<Grupa> ucitajSveGrupe()
        {
            try
            {
                string filename = "fajlovi/Grupe.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return null;
                }

                List<Grupa> sveGrupe = new List<Grupa>();

                var textLines = File.ReadAllLines(filename).ToList();

                for (int i = 1; i < textLines.Count; ++i)
                {
                    string[] podaci = textLines[i].Split(",");

                    List<int> korisnici = new List<int>();

                    for (int j = 3; j < podaci.Length; ++j)
                    {
                        korisnici.Add(int.Parse(podaci[j]));
                    }

                    sveGrupe.Add(new Grupa(int.Parse(podaci[0]), podaci[1], podaci[2], korisnici));
                }

                return sveGrupe;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return null;
        }

        public void ZapamtiGrupu()
        {
            try
            {
                string filename = "fajlovi/Grupe.txt";

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

        public void ObrisiGrupu()
        {
            try
            {
                string filename = "fajlovi/Grupe.txt";

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

        public void updateGrupu()
        {
            try
            {
                string filename = "fajlovi/Grupe.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                int idx = textLines.FindIndex(g => g.Substring(0, g.IndexOf(",")).Equals("" + Id_grupe));

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

        public void DodajDiskusiju(Diskusija diskusija)
        {
            diskusije.Add(diskusija);

            OnPropertyChanged("Diskusije");
        }

        public void ObrisiDiskusiju(Diskusija diskusija)
        {
            diskusije.Remove(diskusija);

            OnPropertyChanged("Diskusije");
        }

        public void IzmeniDiskusiju(Diskusija diskusija)
        {
            foreach (Diskusija d in diskusije)
            {
                if (d.Id_diskusije == diskusija.Id_diskusije)
                {
                    d.Naziv_diskusije = diskusija.Naziv_diskusije;
                    d.updateDiskusuju();
                }
            }
        }

#pragma warning restore CS8603 // Possible null reference return.

        public override string ToString()
        {
            string res =  "" + id_grupe + "," +
                               ime_grupe + "," +
                               opis_grupe;

            foreach(int id in clanovi)
            {
                res += "," + id;
            }

            return res;
        }
    }
}
