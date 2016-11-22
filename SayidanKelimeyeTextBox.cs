using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SayindanKelimeye.WFA
{
    public delegate void MaximumValueHandler(string message);
    public partial class SayidanKelimeyeTextBox : UserControl
    {
        public SayidanKelimeyeTextBox()
        {
            InitializeComponent();
        }
        public bool SinifPropYazmayiBiliyorMu { get; set; }
        public int Sayi { get; set; }
        public event MaximumValueHandler MaxValueOver;
        public string Yazi
        {
            get
            {
                return txtYazi.Text;
            }
        }
        string OkunusunuEkranaYaz(int[] basamaklar)
        {
            string[] birler = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
            string[] onlar = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Atmış", "Yetmiş", "Seksen", "Doksan" };
            string yuzler = "Yüz";
            string binler = "Bin";
            string cikti = string.Empty;

            if (basamaklar[3] == 1)
                cikti += binler;
            else if (basamaklar[3] != 0)
                cikti += birler[basamaklar[3]] + binler;

            if (basamaklar[2] == 1)
                cikti += yuzler;
            else if (basamaklar[2] != 0)
                cikti += birler[basamaklar[2]] + yuzler;

            if (basamaklar[0] == 0 && basamaklar[1] == 0 && basamaklar[2] == 0 & basamaklar[3] == 0)
                cikti = "Sıfır";
            else
                cikti += onlar[basamaklar[1]] + birler[basamaklar[0]];
            return cikti;
        }
        int[] Basamaklandir(int girilenSayi)
        {
            int[] basamaklar = new int[4];
            basamaklar[0] = girilenSayi % 10; // birler basamağı
            basamaklar[1] = (girilenSayi / 10) % 10; //onlar basamağı
            basamaklar[2] = (girilenSayi / 100) % 10; // yüzler basamağı
            basamaklar[3] = (girilenSayi / 1000) % 10; // binler basamağı
            return basamaklar;
        }
        int SayiOku()
        {
            int sayi = 0;
            try
            {
                sayi = int.Parse(txtSayi.Text);
                if (sayi < 0 || sayi > 9999)
                {
                    MaxValueOver("Maximum değeri aştınız");
                    txtSayi.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtSayi.Text = "0";
            }
            return sayi;
        }
        private void txtSayi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;
            else if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void txtSayi_TextChanged(object sender, EventArgs e)
        {
            if (txtSayi.Text.Length > 4)
                MaxValueOver("Maksimum Sayıyı aştınız");
            txtYazi.Text = OkunusunuEkranaYaz(Basamaklandir(SayiOku()));
        }
    }
}
