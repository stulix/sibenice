using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sibenice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Hra hra = new Hra();
            hra.Start();

        }
        


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            Pismena pismo = new Pismena();
            Hra hra = new Hra();

            btn.Visible = false;
            char c = btn.Text[0];
            List<char> list = pismo.SeznamZnaku(c);
            bool trefa = pismo.Trefa(list);

            if (trefa)
            {
                lbSlovo.Text = pismo.Sifra();
                bool win = hra.Vitez();
                if (win)
                    hra.GameWin();
            }

            else
            {
                pismo.Vedle();
            }
                


        }
    }
    public class Pismena: Hra
    {
        List<char> list = new List<char>();

        public List<char> SeznamZnaku(char c)
        {
            list.Add(c);
            return list;
        }

        public string Sifra()
        {
            zvolenyZnak = zvolenyZnak.Concat(list);
            string sifra = "";
            foreach (char c in slovo)
            {
                if (zvolenyZnak.Contains(c))
                {
                    sifra += c;
                }
                else
                {
                    sifra += "*";
                }
                   
            }
            return sifra;
        }

        public bool Trefa(List<char> list)
        {
            foreach (char c in slovo)
                if (list.Contains(c))
                    return true;
            return false;
        }


        public void Vedle()
        {
            Error++;
            bool ok = NacteniObrazku(Error);
            if (!ok)
            {
                KonecHry();
            }
                

        }
        

    }

    public class Hra : Obrazek
    {
        public IEnumerable<char> zvolenyZnak = new List<char>();
        public static string slovo;
        private Random nahoda = new Random();
        Pismena pis = new Pismena();
        int error;

        public void ZobrazitTlacitka()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button13.Visible = true;
            button14.Visible = true;
            button15.Visible = true;
            button16.Visible = true;
            button17.Visible = true;
            button18.Visible = true;
            button19.Visible = true;
            button20.Visible = true;
            button21.Visible = true;
            button22.Visible = true;
            button23.Visible = true;
            button24.Visible = true;
            button25.Visible = true;
            button26.Visible = true;
            button27.Visible = true;

        }

        public int Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }
        
        
        private int body = 0;

        public void Start()
        {
            
            slovo = GetSlovo();
            zvolenyZnak = new List<char>();
            error = 0;
            
            lbSlovo.Text = pis.Sifra();
            NacteniObrazku(error);
            ZobrazitTlacitka();
            lbBody.Text = body.ToString();

        }

        public bool Vitez()
        {
            foreach (char c in slovo)
            {
                if (!zvolenyZnak.Contains(c))
                    return false;
            }
            return true;
        }

        private string GetSlovo()
        {
            string cesta = Slozka + @"\slova.txt";
            string[] slova = File.ReadAllLines(cesta);
            int i = nahoda.Next(0, slova.Length);
            return slova[i].ToUpper();
        }

        public void KonecHry()
        {
            MessageBox.Show("Prohál si! Správné slovo bylo: " + slovo.ToLower());
            Start();
        }
        public void GameWin()
        {
            MessageBox.Show("Vyhrál jsi bod!");
            body++;
            Start();
        }

    }

    public class Obrazek
    {
        public string Slozka
        {
            get
            {
                FileInfo info = new FileInfo(Application.ExecutablePath);
                return info.DirectoryName;
            }
        }

        public bool NacteniObrazku(int i)
        {
            string cesta = Slozka + @"\imgs\";
            string slozka = cesta + "img" + i.ToString() + ".bmp";
            if (File.Exists(slozka))
            {
                pcObrazek.Image = new Bitmap(slozka);
                return File.Exists(cesta + "img" + (i + 1).ToString() + ".bmp");
            }
            else
                return false;
        }
    }

    
}
