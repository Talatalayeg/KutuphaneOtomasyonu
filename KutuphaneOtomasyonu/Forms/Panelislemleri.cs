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


        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            Listele();
            GirisPaneli girisPaneli = new GirisPaneli();
            girisPaneli.Show();
            this.Hide();
        }

        private void CalisanPaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            CalisanPaneli calisanPaneli = new CalisanPaneli();
            calisanPaneli.Show();
            calisanPaneli.CalisanPaneliKullaniciRolu = KullaniciRolu;
            calisanPaneli.CalisanPaneliKullaniciAdi = KullaniciAdi;
            this.Hide();
        }

        private void UyePaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            UyePaneli uyePaneli = new UyePaneli();
            uyePaneli.UyePaneliKullaniciRolu = KullaniciRolu;
            uyePaneli.UyePaneliKullaniciAdi = KullaniciAdi;
            uyePaneli.Show();
            this.Hide();
        }

        private void KitapPaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            KitapPaneli kitapPaneli = new KitapPaneli();
            kitapPaneli.KitapPaneliKullaniciRolu = KullaniciRolu;
            kitapPaneli.KitapPaneliKullaniciAdi = KullaniciAdi;
            kitapPaneli.Show();
            this.Hide();
        }

        private void KiralamaPaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            KiralamaPaneli kiralamaPaneli = new KiralamaPaneli();
            kiralamaPaneli.KiralamaPaneliKullaniciRolu = KullaniciRolu;
            kiralamaPaneli.KiralamaPaneliKullaniciAdi = KullaniciAdi;
            kiralamaPaneli.Show();
            this.Hide();
        }


        private void OdemePaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            OdemePaneli odemePaneli = new OdemePaneli();
            odemePaneli.OdemePaneliKullaniciRolu = KullaniciRolu;
            odemePaneli.OdemePaneliKullaniciAdi = KullaniciAdi;
            odemePaneli.Show();
            this.Hide();
        }

        private void RaporPaneliButonu_Click(object sender, EventArgs e)
        {
            Listele();
            RaporPaneli raporPaneli = new RaporPaneli();
            raporPaneli.RaporPaneliKullaniciRolu = KullaniciRolu;
            raporPaneli.RaporPaneliKullaniciAdi = KullaniciAdi;
            raporPaneli.Show();
            this.Hide();
        }
    }
}
