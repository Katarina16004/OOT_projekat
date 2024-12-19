using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekat.models
{
    public class Diskusija : INotifyPropertyChanged
    {
        private int id_diskusije;
        private string naziv_diskusije;
        private string datumporuke;
        private int id_grupe;
        private int broj_clanova_diskusije;
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
        public Diskusija(int id_diskusije, string naziv_diskusije, string datumporuke, int id_grupe, int brclanova)
        {
            this.id_diskusije = id_diskusije;
            this.naziv_diskusije = naziv_diskusije;
            this.datumporuke = datumporuke;
            this.id_grupe = id_grupe;
            broj_clanova_diskusije = brclanova;

        }
        public int Id_diskusije
        {
            get { return id_diskusije; }
            set
            {
                if (id_diskusije != value)
                {
                    id_diskusije = value;
                    OnPropertyChanged("Id_diskusije");
                }
            }
        }
        public int Broj_clanova_diskusije
        {
            get { return broj_clanova_diskusije; }
            set
            {
                if (broj_clanova_diskusije != value)
                {
                    broj_clanova_diskusije = value;
                    OnPropertyChanged("Broj_clanova_diskusije");
                }
            }
        }
        public int Id_grupe
        {
            get { return id_grupe; }
            set
            {
                if (id_diskusije != value)
                {
                    id_grupe = value;
                    OnPropertyChanged("Id_grupe");
                }
            }
        }
        public string Naziv_diskusije
        {
            get { return naziv_diskusije; }
            set
            {
                if (naziv_diskusije != value)
                {
                    naziv_diskusije = value;
                    OnPropertyChanged("Naziv_diskusije");
                }
            }
        }
        public string Datumporuke
        {
            get { return datumporuke; }
            set
            {
                if (datumporuke != value)
                {
                    datumporuke = value;
                    OnPropertyChanged("Datumporuke");
                }
            }
        }
        public static void ucitajDiskusije(Grupa g)
        {
            if (g == null)
                return;

            try
            {
                string filename = "fajlovi/Diskusije.txt";

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
                    if (podaci[3].Equals("" + g.Id_grupe))
                    {
                        // Pronasli smo liniju koja nam treba
                        // Vratimo post sa podacima
                        g.Diskusije.Add(new Diskusija(int.Parse(podaci[0]), podaci[1], podaci[2], int.Parse(podaci[3]), g.Clanovi.Count));
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

        public void ZapamtiDiskusiju()
        {
            try
            {
                string filename = "fajlovi/Diskusije.txt";

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

        public void ObrisiDiskusuju()
        {
            try
            {
                string filename = "fajlovi/Diskusije.txt";

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

        public void updateDiskusuju()
        {
            try
            {
                string filename = "fajlovi/Diskusije.txt";

                if (!File.Exists(filename))
                {
                    MessageBox.Show("File do not exists: " + filename);

                    return;
                }


                var textLines = File.ReadAllLines(filename).ToList();

                int idx = textLines.FindIndex(d => d.Substring(0, d.IndexOf(",")).Equals("" + Id_diskusije));

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

        public override string ToString()
        {
            return "" + id_diskusije + "," +
                        naziv_diskusije + "," +
                        datumporuke + "," +
                        id_grupe;
        }
    }
}
