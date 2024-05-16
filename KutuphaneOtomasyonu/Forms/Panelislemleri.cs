using KutuphaneOtomasyonu.Forms;
using KutuphaneOtomasyonu.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonu
{
    public partial class Panelislemleri : Form
    {
        public string KullaniciRolu;
        public string KullaniciAdi;

        public Panelislemleri()
        {
            InitializeComponent();
        }

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        private void Listele()
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            var kiraliklar = db.KiralıkBilgileri.ToList();
            kiraliklarDataGrid.DataSource = kiraliklar;
            kiraliklarDataGrid.Columns[6].Visible = false;
            kiraliklarDataGrid.Columns[7].Visible = false;

            var cezalilar = db.UyeBilgileri.Where(x => x.Uye_Ceza > 0).ToList();
            CezalilarDataGrid.DataSource = cezalilar;
            CezalilarDataGrid.Columns[11].Visible = false;
            CezalilarDataGrid.Columns[12].Visible = false;
            CezalilarDataGrid.Columns[13].Visible = false;
            CezalilarDataGrid.Columns["Uye_Ceza"].DisplayIndex = 4;

            // Formdaki bütün dataGrid sutun baslikları
            string[] sutunBasliklari = {
            "ID", "Kitap ID", "Uye ID", "Kiralama Tarihi", "Son Teslim Tarihi","Kalan Gün Sayısı"
            };

            foreach (DataGridView dataGridView in new[] {kiraliklarDataGrid })
            {
                for (int i = 0; i < sutunBasliklari.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari[i];
                }
            }
        }

        private void Panelislemleri_Load(object sender, EventArgs e)
        {
            Listele();

            if (KullaniciRolu != "Admin")
            {
                label1.Text += KullaniciAdi + " ,";
                CalisanPaneliButonu.Visible = false;
                label2.Visible = false;
            }
            else
            {
                label1.Text += KullaniciAdi + " ,";
            }
        }

        private void CalisanPaneliButonu_Click(object sender, EventArgs e)
        {
            CalisanPaneli calisanPaneli = new CalisanPaneli();
            calisanPaneli.Show();
            calisanPaneli.CalisanPaneliKullaniciRolu = KullaniciRolu;
            calisanPaneli.CalisanPaneliKullaniciAdi = KullaniciAdi;
            this.Hide();
        }

        private void UyePaneliButonu_Click(object sender, EventArgs e)
        {
            UyePaneli uyePaneli = new UyePaneli();
            uyePaneli.UyePaneliKullaniciRolu = KullaniciRolu;
            uyePaneli.UyePaneliKullaniciAdi = KullaniciAdi;
            uyePaneli.Show();
            this.Hide();
        }

        private void KitapPaneliButonu_Click(object sender, EventArgs e)
        {
            KitapPaneli kitapPaneli = new KitapPaneli();
            kitapPaneli.KitapPaneliKullaniciRolu = KullaniciRolu;
            kitapPaneli.KitapPaneliKullaniciAdi = KullaniciAdi;
            kitapPaneli.Show();
            this.Hide();
        }

        private void KiralamaPaneliButonu_Click(object sender, EventArgs e)
        {
            KiralamaPaneli kiralamaPaneli = new KiralamaPaneli();
            kiralamaPaneli.KiralamaPaneliKullaniciRolu = KullaniciRolu;
            kiralamaPaneli.KiralamaPaneliKullaniciAdi = KullaniciAdi;
            kiralamaPaneli.Show();
            this.Hide();
        }


        private void OdemePaneliButonu_Click(object sender, EventArgs e)
        {
            OdemePaneli odemePaneli = new OdemePaneli();
            odemePaneli.OdemePaneliKullaniciRolu = KullaniciRolu;
            odemePaneli.OdemePaneliKullaniciAdi = KullaniciAdi;
            odemePaneli.Show();
            this.Hide();
        }

        private void kiraliklarDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kiralikKitapId = Convert.ToInt16(kiraliklarDataGrid.CurrentRow.Cells[1].Value.ToString());
            var kiralikKitap = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kiralikKitapId)).FirstOrDefault();

            label11.Text = kiralikKitap.Kitap_Adi.ToString();          // 11 Kitap Adı

            int kiralikUyeId = Convert.ToInt16(kiraliklarDataGrid.CurrentRow.Cells[2].Value.ToString());
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kiralikUyeId)).FirstOrDefault();

            label10.Text = uyeBilgisi.Uye_Adi.ToString();                 // 10 Üye Adı
            label10.Text += " " + uyeBilgisi.Uye_Soyadi.ToString();

            label12.Text = kiraliklarDataGrid.CurrentRow.Cells[5].Value.ToString();       // 12 Kalan Gün
            label12.Text += " Gün";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                var gunuGecenler = db.KiralıkBilgileri.Where(x => x.Kalan_Gun<=0).ToList();
                kiraliklarDataGrid.DataSource = gunuGecenler;
            }
            else if(checkBox1.Checked == false)
            {
                Listele();
            }
        }
    }
}
