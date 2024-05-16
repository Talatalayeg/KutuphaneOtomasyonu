using KutuphaneOtomasyonu.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonu.Forms
{
    public partial class OdemePaneli : Form
    {
        public string OdemePaneliKullaniciRolu;
        public string OdemePaneliKullaniciAdi;
        public int _secilenUyeId;

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public OdemePaneli()
        {
            InitializeComponent();
        }

        private void OdemePaneli_Load(object sender, EventArgs e)
        {
            label1.Text = "Ödeme Paneli - Ceza Ödeme";
            CezaOdemePaneli.Visible = true;
            CezaOdemePaneli.BringToFront();
            Listele();
            Temizle();
        }

        // Kod üretimi
        static string OdemeKoduUret()
        {
            KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

            Random random = new Random();
            string OdemeNo = "";
            for (int i = 0; i < 11; i++)
            {
                // ASCII'de 48-57 arası rakamları temsil eder
                int randomNumber = random.Next(48, 58);
                char ch = Convert.ToChar(randomNumber);
                OdemeNo += ch;
            }

            bool varMi = db.OdemeBilgileri.Any(x => x.Odeme_No.Contains(OdemeNo));

            if (varMi == true)
            {
                return OdemeKoduUret();
            }
            else
            {
                return OdemeNo;
            }
        }

        // Önceki Panele Dönme
        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = OdemePaneliKullaniciRolu;
            panel.KullaniciAdi = OdemePaneliKullaniciAdi;
            panel.Show();
            this.Hide();
        }

        private void Listele()
        {
            var uyeler = db.UyeBilgileri.ToList();
            CezalilarDataGrid.DataSource = uyeler.ToList();
            CezalilarDataGrid.Columns["Uye_Ceza"].DisplayIndex = 4;
            CezalilarDataGrid.Columns[11].Visible = false;
            CezalilarDataGrid.Columns[12].Visible = false;
            CezalilarDataGrid.Columns[13].Visible = false;

            var gecmis = db.OdemeBilgileri.ToList();
            OdemeGecmisiDataGrid.DataSource = gecmis.ToList();
            OdemeGecmisiDataGrid.Columns[5].Visible = false;
        }

        private void Temizle()
        {
            label2.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label12.Text = "";
            label14.Text = "";

            label29.Visible = false;
            birKisminiOdeTXT.Visible = false;
            birKismiCheckBox.Checked = false;
            BirKisminiOdeButonu.Visible = false;

            TamaminiOdeButonu.Visible = true;
        }



        //Odeme Bölümü

        #region

        // Sol Odeme Butonu
        private void S_OdemeButonu_Click(object sender, EventArgs e)
        {
            label1.Text = "Ödeme Paneli - Ceza Ödeme";
            CezaOdemePaneli.Visible = true;
            CezaOdemePaneli.BringToFront();
        }

        // bir kısmını öde checkbox
        private void birKismiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (birKismiCheckBox.Checked == true)
            {
                label29.Visible = true;
                birKisminiOdeTXT.Visible = true;
                BirKisminiOdeButonu.Visible = true;

                TamaminiOdeButonu.Visible = false;
            }
            else if (birKismiCheckBox.Checked == false)
            {
                label29.Visible = false;
                birKisminiOdeTXT.Visible = false;
                BirKisminiOdeButonu.Visible = false;

                TamaminiOdeButonu.Visible = true;
            }
        }

        // Sadece cezalilar
        private void cezalilarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(cezalilarCheckBox.Checked == true)
            {
                var cezalilar = db.UyeBilgileri.Where(x => x.Uye_Ceza > 0).ToList();
                CezalilarDataGrid.DataSource = cezalilar;
            }
            else if(cezalilarCheckBox.Checked == false)
            {
                Listele();
            }
        }

        // Kullanıcı Bilgilerini labele atama
        private void CezalilarDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenUyeID = Convert.ToInt16(CezalilarDataGrid.CurrentRow.Cells[0].Value);
            _secilenUyeId = secilenUyeID;
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id == secilenUyeID).FirstOrDefault();

            label2.Text = uyeBilgisi.Uye_Adi.ToString() + " " + uyeBilgisi.Uye_Soyadi.ToString();  // 2 adi ve soyadi

            if (uyeBilgisi.Uye_Ceza == null)
            {
                label4.Text = "0 TL";
            }                                                       // 4 ceza
            else
            {
                label4.Text = uyeBilgisi.Uye_Ceza.ToString();
            }
        }
        
        // Kullanıcı Tc arama
        private void G_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            string arananTC = G_TcAraTXT.Text;
            if (cezalilarCheckBox.Checked == true)
            {
                var bulunanCezaliUyeler = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananTC) && x.Uye_Ceza > 0).ToList();
                CezalilarDataGrid.DataSource = bulunanCezaliUyeler;
            }
            else if (cezalilarCheckBox.Checked == false)
            {
                var bulunanUyeler = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananTC) && x.Uye_Ceza == 0).ToList();
                CezalilarDataGrid.DataSource = bulunanUyeler;
            }
        }

        // Tamamını Öde Butonu
        private void TamaminiOdeButonu_Click(object sender, EventArgs e)
        {
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id == _secilenUyeId).FirstOrDefault();
            
            if(uyeBilgisi.Uye_Ceza <= 0)
            {
                MessageBox.Show("Üyenin Cezası bulunmamaktadır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DateTime bugun = DateTime.Now;

                OdemeBilgileri yeniOdeme = new OdemeBilgileri();
                yeniOdeme.Odeme_No = OdemeKoduUret();
                yeniOdeme.Uye_Id = uyeBilgisi.Uye_Id;
                yeniOdeme.Odenen_Tutar = uyeBilgisi.Uye_Ceza;
                yeniOdeme.Odeme_Tarihi = bugun;

                //Onay Kutusu
                DialogResult onay = MessageBox.Show(uyeBilgisi.Uye_Adi + " adlı üyenin cezasını ödemek istiyor musun?",
                                                                            "Onaylama İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    uyeBilgisi.Uye_Ceza = 0;
                    db.OdemeBilgileri.Add(yeniOdeme);
                    db.SaveChanges();
                    Listele();
                }
                else if (onay == DialogResult.No)
                {
                    Temizle();
                    Listele();
                }
            }
        }

        // Bir Kısmını Öde Butonu
        private void BirKisminiOdeButonu_Click(object sender, EventArgs e)
        {
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id == _secilenUyeId).FirstOrDefault();

            int tutar = Convert.ToInt16(birKisminiOdeTXT.Text);

            if (uyeBilgisi.Uye_Ceza <= 0)
            {
                MessageBox.Show("Üyenin Cezası bulunmamaktadır...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (tutar <= 0 || tutar > uyeBilgisi.Uye_Ceza)
            {
                MessageBox.Show("Lütfen Doğru değer giriniz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DateTime bugun = DateTime.Now;

                OdemeBilgileri yeniOdeme = new OdemeBilgileri();
                yeniOdeme.Odeme_No = OdemeKoduUret();
                yeniOdeme.Uye_Id = uyeBilgisi.Uye_Id;
                yeniOdeme.Odenen_Tutar = tutar;
                yeniOdeme.Odeme_Tarihi = bugun;

                //Onay Kutusu
                DialogResult onay = MessageBox.Show(uyeBilgisi.Uye_Adi + " adlı üyenin\n" + tutar +" TL " 
                                                                       + "cezasını ödemek istiyor musun?",
                                                                            "Onaylama İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    uyeBilgisi.Uye_Ceza -= tutar;
                    db.OdemeBilgileri.Add(yeniOdeme);
                    db.SaveChanges();
                    
                    MessageBox.Show(uyeBilgisi.Uye_Adi + " adlı üyenin\n" 
                                                       + tutar + " TL kadar cezasını ödenmiştir.\n" 
                                                       + "kalan ceza Tutarı = " + uyeBilgisi.Uye_Ceza,
                                                       "Tamamlandı", MessageBoxButtons.OK, 
                                                       MessageBoxIcon.Information);
                    Listele();
                }
                else if (onay == DialogResult.No)
                {
                    Temizle();
                    Listele();
                }
            }
        }
        
        #endregion


        // Listeleme Bölümü

        #region

        // Sol Listele Butonu
        private void ListeleButonu_Click(object sender, EventArgs e)
        {
            label1.Text = "Ödeme Paneli - Ödemeleri Listele";
            GecmisPaneli.Visible = true;
            GecmisPaneli.BringToFront();
            Temizle();
            Listele();
        }

        // Data Grid bilgileri labele atama
        private void OdemeGecmisiDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenOdemeID = Convert.ToInt16(OdemeGecmisiDataGrid.CurrentRow.Cells[0].Value);       // 0 Odeme_Id
            var odemeBilgisi = db.OdemeBilgileri.Where(x => x.Odeme_Id == secilenOdemeID).FirstOrDefault();

            label14.Text = odemeBilgisi.Odeme_No.ToString();            // 14 Odeme No
            label12.Text = odemeBilgisi.Odeme_Tarihi.ToString();        // 12 Odeme Tarihi

            int secilenUyeID = Convert.ToInt16(OdemeGecmisiDataGrid.CurrentRow.Cells[2].Value);         // 2 Odeme_Id
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id == secilenUyeID).FirstOrDefault();

            label6.Text = uyeBilgisi.Uye_Adi.ToString() + " " + uyeBilgisi.Uye_Soyadi.ToString();  // 6 adi ve soyadi

            if (odemeBilgisi.Odenen_Tutar == null)
            {
                label5.Text = "0 TL";
            }                                                       // 5 ceza
            else
            {
                label5.Text = odemeBilgisi.Odenen_Tutar.ToString();
            }
        }

        // Odeme No ile arama
        private void odemeNoTXT_TextChanged(object sender, EventArgs e)
        {
            string arananNo = odemeNoTXT.Text;
            var bulunanOdemeler = db.OdemeBilgileri.Where(x => x.Odeme_No.Contains(arananNo)).ToList();
            OdemeGecmisiDataGrid.DataSource = bulunanOdemeler;
        }

        #endregion

    }
}
