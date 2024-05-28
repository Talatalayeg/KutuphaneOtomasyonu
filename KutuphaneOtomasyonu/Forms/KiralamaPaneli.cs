using KutuphaneOtomasyonu.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KutuphaneOtomasyonu.Forms
{
    public partial class KiralamaPaneli : Form
    {
        public string KiralamaPaneliKullaniciRolu;
        public string KiralamaPaneliKullaniciAdi;

        //Veri Tabanı bağlantısı
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public KiralamaPaneli()
        {
            InitializeComponent();

            KiralaPaneli.Visible = false;
            GeriAlPaneli.Visible = false;
            ListelePaneli.Visible = false;
            GecmisPaneli.Visible = false;
        }

        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = KiralamaPaneliKullaniciRolu;
            panel.KullaniciAdi = KiralamaPaneliKullaniciAdi;
            panel.Show();
            this.Hide();

        }

        private void Listele()
        {
            /// Kirala - Kitaplar

            var kitap = db.KitapBilgileri.ToList();
            KitaplarDataGridView.DataSource = kitap.ToList();
            KitaplarDataGridView.Columns["K_OduncDurumu"].DisplayIndex = 4;
            KitaplarDataGridView.Columns[11].Visible = false;

            /// GeriAl- Kitaplar

            KitaplarDataGridView2.DataSource = kitap.ToList();
            KitaplarDataGridView2.Columns["K_OduncDurumu"].DisplayIndex = 4;
            KitaplarDataGridView2.Columns[11].Visible = false;
            
            /// Listele - Kitap

            ListeleKitaplarDataGrid.DataSource = kitap.ToList();
            ListeleKitaplarDataGrid.Columns["K_OduncDurumu"].DisplayIndex = 4;

            // üstteki 2 dataGrid için başlıklar
            #region
            string[] sutunBasliklari = {
            "ID", "Kitap ISBN", "Kitap Adı", "Kitap Yazarı", "Kitap Dili",
            "Sayfa Sayısı", "Basım Yılı", "Yayınevi", "Cevirmen", "Odunc Durumu","Eklenme Tarihi"
              };

            foreach (DataGridView dataGridView in new[] { KitaplarDataGridView, KitaplarDataGridView2 , ListeleKitaplarDataGrid })
            {
                for (int i = 0; i < sutunBasliklari.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari[i];
                }
            }
            #endregion


            /// Kirala - Uye
            #region
            var uye = db.UyeBilgileri.ToList();
            UyelerDataGridView.DataSource = uye.ToList();
            UyelerDataGridView.Columns[10].Visible = false;
            UyelerDataGridView.Columns[11].Visible = false;

            // Kirala - Uyeler Başlıklar
            #region
            UyelerDataGridView.Columns[0].HeaderText = "ID";
            UyelerDataGridView.Columns[1].HeaderText = "Üye Adı";
            UyelerDataGridView.Columns[2].HeaderText = "Üye Soyadı";
            UyelerDataGridView.Columns[3].HeaderText = "Üye TC";
            UyelerDataGridView.Columns[5].HeaderText = "Üye TelNo";
            UyelerDataGridView.Columns[6].HeaderText = "Üye Eposta";
            UyelerDataGridView.Columns[7].HeaderText = "Üye Adres";
            UyelerDataGridView.Columns[8].HeaderText = "Üye Ceza";
            UyelerDataGridView.Columns[9].HeaderText = "Sahip Oldugu Kitaplar";

            UyelerDataGridView.Columns["Uye_Ceza"].DisplayIndex = 4;
            UyelerDataGridView.Columns["Uye_SahipOldugu"].DisplayIndex = 5;
            #endregion

            #endregion


            /// Geri Al - Kiralıklar

            var kiraliklar = db.KiralıkBilgileri.ToList();
            KiralananlarDataGridView2.DataSource = kiraliklar.ToList();
            KiralananlarDataGridView2.Columns[6].Visible = false;
            KiralananlarDataGridView2.Columns[7].Visible = false;



            /// Listele - Kiralıklar

            KiralananlarDataGridView1.DataSource = kiraliklar.ToList();
            KiralananlarDataGridView1.Columns[6].Visible = false;
            KiralananlarDataGridView1.Columns[7].Visible = false;

            // üstteki 2 DataGrid için başlıklar
            #region
            string[] sutunBasliklari2 = {
            "ID", "Kitap ID", "Uye ID", "Kiralama Tarihi", "Son Teslim Tarihi","Kalan Gün Sayısı"
            };

            foreach (DataGridView dataGridView in new[] { KiralananlarDataGridView2, KiralananlarDataGridView1 })
            {
                for (int i = 0; i < sutunBasliklari2.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari2[i];
                }
            }
            #endregion

            /// Geçmişi Listele - Geçmiş Kiralıklar
            
            var gecmis = db.GecmisKiralikBilgileri.ToList();
            GecmisKiraliklarDataGrid.DataSource = gecmis.ToList();
            GecmisKiraliklarDataGrid.Columns[8].Visible = false;
            GecmisKiraliklarDataGrid.Columns[9].Visible = false;


        }

        private void Temizle()
        {
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label19.Text = "";
            label20.Text = "";
            label36.Text = "";
            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label35.Text = "";
            label37.Text = "";
            label38.Text = "";
            label42.Text = "";
            label43.Text = "";
            label44.Text = "";
            label50.Text = "";
            label51.Text = "";
            label52.Text = "";
            label53.Text = "";
            label55.Text = "";
            label62.Text = "";
            label63.Text = "";
            label66.Text = "";
            label87.Text = "";
            label86.Text = "";
            label85.Text = "";
            label77.Text = "";
            label76.Text = "";
            label75.Text = "";
            label74.Text = "";
            label73.Text = "";
        }

        private void KiralamaPaneli_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }


        /// Kiralama Bölümü

        #region

        //Sol Bar Kiralama butonu
        private void KiralaButonu_Click(object sender, EventArgs e)
        {
            KiralaPaneli.Visible = true;
            label1.Text = "Kiralama Paneli - Kirala";
            KiralaPaneli.BringToFront();
            Temizle();
            UyelerDataGridView.Columns.Clear();
            KitaplarDataGridView.Columns.Clear();
        }

        /// ÜyelerDataGrid içerisindeki değerleri labellere atama
        private void UyelerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenUyeID = Convert.ToInt16(UyelerDataGridView.CurrentRow.Cells[0].Value);
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id == secilenUyeID).FirstOrDefault();

            label15.Text = uyeBilgisi.Uye_Adi.ToString();                // 15 adi ve soyadi
            label15.Text += " " + uyeBilgisi.Uye_Soyadi.ToString();

            label16.Text = uyeBilgisi.Uye_Tc.ToString();             // 16 tc
            label66.Text = uyeBilgisi.Uye_SahipOldugu.ToString();    // 66 Sahip olduğu kitap sayısı

            if (uyeBilgisi.Uye_Ceza == null)
            {
                label17.Text = "0 TL";
            }                                                       // 17 ceza
            else
            {
                label17.Text = uyeBilgisi.Uye_Ceza.ToString();
            }

            DateTime bugun = DateTime.Now;
            label19.Text = "" + bugun;              // 19 Kiralama Tarihi
        }

        /// KitaplarDataGrid içerisindeki değerleri labellere atama
        private void KitaplarDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenKitapID = Convert.ToInt16(KitaplarDataGridView.CurrentRow.Cells[0].Value);
            var kitapBilgisi = db.KitapBilgileri.Where(x => x.Kitap_Id == secilenKitapID).FirstOrDefault();

            label14.Text = kitapBilgisi.Kitap_ISBN.ToString();      // 14 ISBN
            label12.Text = kitapBilgisi.Kitap_Adi.ToString();       // 12 adi
            label13.Text = kitapBilgisi.Kitap_YayinEvi.ToString();  // 13 Yayınevi
        }

        // Kiralama İşlemi
        private void KiralamaButonu_Click(object sender, EventArgs e)
        {
            string secilenUyeTc = label16.Text;
            var secilenKisi = db.UyeBilgileri.Where(x => x.Uye_Tc.Equals(secilenUyeTc)).FirstOrDefault();

            string secilenKitapAdi = label12.Text;
            var secilenKitap = db.KitapBilgileri.Where(x => x.Kitap_Adi.Equals(secilenKitapAdi)).FirstOrDefault();

            DateTime bugun = DateTime.Now;

            if (secilenKitap.K_OduncDurumu != "Hazır")
            {
                MessageBox.Show("Kitap Başkası tarafından kiralanmıştır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (secilenKisi.Uye_Ceza >= 50)
                {
                    MessageBox.Show("Üyenin Cezası 50TL üzeri olduğunda\n " +
                        "Kiralama işlemi yapılmamaktadır...\n" +
                        "Lütfen Ceza ödemesini ödedikten sonra kiralama\n" +
                        "işlemini deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    if (secilenKisi.Uye_SahipOldugu >= 3)
                    {
                        MessageBox.Show("Üyenin 3'ten fazla kitaba\n " +
                            "sahip olduğu için Kiralama işlemi yapılmamaktadır" +
                            "Lütfen Kitapları Teslim edip Tekrar deneyiniz...\n", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Onay Kutusu
                        DialogResult onay = MessageBox.Show(secilenKisi.Uye_Adi + " adlı üyeye \n" +
                                                            secilenKitap.Kitap_Adi + " adlı kitabı kiralamak istiyor musunuz?",
                                                                                    "Onaylama İşlemi", MessageBoxButtons.YesNo);
                        if (onay == DialogResult.Yes)
                        {
                            KiralıkBilgileri yeniKiralama = new KiralıkBilgileri();
                            yeniKiralama.Kitap_Id = secilenKitap.Kitap_Id;
                            yeniKiralama.Uye_Id = secilenKisi.Uye_Id;
                            yeniKiralama.Kiralama_Tarihi = bugun;
                            yeniKiralama.Teslim_Tarihi = dateTimePicker1.Value;

                            DateTime teslim = dateTimePicker1.Value;
                            TimeSpan ts = teslim - bugun;
                            label20.Text = (ts.Days + 1).ToString() + " Gün";

                            secilenKitap.K_OduncDurumu = "Kiralık";
                            secilenKisi.Uye_SahipOldugu += 1;

                            db.KiralıkBilgileri.Add(yeniKiralama);
                            db.SaveChanges();

                            MessageBox.Show(secilenKisi.Uye_Adi + " adlı üyeye \n" +
                                                            secilenKitap.Kitap_Adi + " adlı kitabı " + label20.Text + " günlüğüne kiraladınız"
                                                            , "Kiralandı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Listele();
                        }
                        else if (onay == DialogResult.No)
                        {
                            Listele();
                        }
                    }
                }
            }
        }

        //Kiralanacak Gün seçimi
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Now;
            DateTime teslim = dateTimePicker1.Value;

            TimeSpan ts = teslim - bugun;
            label20.Text = (ts.Days + 1).ToString() + " Gün";
        }

        // Tc ile üye Arama TXT
        private void TcBulTXT_TextChanged(object sender, EventArgs e)
        {
            if (TcBulTXT.Text == "")
            {
                UyelerDataGridView.Columns.Clear();
            }
            else
            {
                string arananTC = TcBulTXT.Text;
                var bulunanUyeler = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananTC)).ToList();
                UyelerDataGridView.DataSource = bulunanUyeler;
            }
        }

        // KitapAdı ile kitap Arama TXT
        private void KitapBulTXT_TextChanged(object sender, EventArgs e)
        {
            if (KitapBulTXT.Text == "")
            {
                KitaplarDataGridView.Columns.Clear();
            }
            else
            {
                string arananKitap = KitapBulTXT.Text;
                var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitap)).ToList();
                KitaplarDataGridView.DataSource = bulunanKitaplar;
            }
        }

        #endregion


        /// GeriAlma Bölümü

        #region

        ///Sol Kiralama Butonu
        private void GeriAlButonu_Click(object sender, EventArgs e)
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            Temizle();
            GeriAlPaneli.Visible = true;
            label1.Text = "Kiralama Paneli - Geri Al";
            GeriAlPaneli.BringToFront();
            Listele();
            KitaplarDataGridView2.Columns.Clear();
        }

        /// KiralananlarDataGrid içerisindeki değerleri labellere atama
        public void KiralananlarDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kiralikKitapId = Convert.ToInt16(KiralananlarDataGridView2.CurrentRow.Cells[1].Value.ToString());  // 1 Kitap_Id
            var kiralikKitap = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kiralikKitapId)).FirstOrDefault();


            label36.Text = kiralikKitap.Kitap_Adi.ToString();          // 36 Adi
            label37.Text = kiralikKitap.Kitap_YayinEvi.ToString();     // 37 Yayınevi
            label38.Text = kiralikKitap.Kitap_ISBN.ToString();         // 38 ISBN


            int kiralikUyeId = Convert.ToInt16(KiralananlarDataGridView2.CurrentRow.Cells[2].Value.ToString());     // 2 Uye_Id
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kiralikUyeId)).FirstOrDefault();

            label33.Text = uyeBilgisi.Uye_Adi.ToString();                // 33 Adi ve Soyadi
            label33.Text += " " + uyeBilgisi.Uye_Soyadi.ToString();

            label34.Text = uyeBilgisi.Uye_Tc.ToString();           // 34 TC
            label35.Text = uyeBilgisi.Uye_Ceza.ToString();         // 35 Ceza

            label32.Text = KiralananlarDataGridView2.CurrentRow.Cells[3].Value.ToString();      // 3 Kiralama T
            label55.Text = KiralananlarDataGridView2.CurrentRow.Cells[4].Value.ToString();      // 4 Teslim T
        }

        /// Geri Alma Butonu
        private void GeriAlmaButonu_Click(object sender, EventArgs e)
        {
            // Uyenin sahip oldugu kitap sayisi azaltildi
            int kiralikUyeId = Convert.ToInt16(KiralananlarDataGridView2.CurrentRow.Cells[2].Value.ToString());     // 2 Uye_Id
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kiralikUyeId)).FirstOrDefault();
            uyeBilgisi.Uye_SahipOldugu -= 1;

            // Kitabin Kiralik durumu guncellendi
            int kiralikKitapId = Convert.ToInt16(KiralananlarDataGridView2.CurrentRow.Cells[1].Value.ToString());  // 1 Kitap_Id
            var kiralikKitap = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kiralikKitapId)).FirstOrDefault();
            kiralikKitap.K_OduncDurumu = "Hazır";

            // Kiralıklar veritabanından veri silindi
            int kiralamaID = Convert.ToInt16(KiralananlarDataGridView2.CurrentRow.Cells[0].Value.ToString());     // 0 Kirala_Id
            var Kiralik = db.KiralıkBilgileri.Where(x => x.Kirala_Id.Equals(kiralamaID)).FirstOrDefault();

            DateTime bugun = DateTime.Now;

            GecmisKiralikBilgileri yeniGecmis = new GecmisKiralikBilgileri();
            yeniGecmis.Kirala_Id = Kiralik.Kirala_Id;
            yeniGecmis.Uye_Id = Kiralik.Uye_Id;
            yeniGecmis.Kitap_Id = Kiralik.Kitap_Id;
            yeniGecmis.Kiralama_Tarihi = Kiralik.Kiralama_Tarihi;
            yeniGecmis.Planlanan_Teslim_Tarihi = Kiralik.Teslim_Tarihi;
            yeniGecmis.Teslim_Edilme_Tarihi = bugun;
            if(Kiralik.Kalan_Gun < 0)
            {
                yeniGecmis.Tanimlanan_Ceza = Convert.ToInt16(Kiralik.Kalan_Gun * -10);
            }
            else if(Kiralik.Kalan_Gun >= 0 || Kiralik.Kalan_Gun == null)
            {
                yeniGecmis.Tanimlanan_Ceza = 0;
            }

            db.GecmisKiralikBilgileri.Add(yeniGecmis);
            db.KiralıkBilgileri.Remove(Kiralik);
            db.SaveChanges();
            Listele();
        }

        // GeriAl sekmesi KitapId Arama
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                KiralananlarDataGridView2.Columns.Clear();
            }
            else
            {
                int arananKitapId = Convert.ToInt16(textBox2.Text);
                var bulunanIdler = db.KiralıkBilgileri.Where(x => x.Kitap_Id.Equals(arananKitapId)).ToList();
                KiralananlarDataGridView2.DataSource = bulunanIdler;
            }
        }

        // GeriAl sekmesi KitapAdı Arama
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                KitaplarDataGridView2.Columns.Clear();
            }
            else
            {
                string arananKitapAdi = textBox1.Text;
                var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitapAdi)).ToList();
                KitaplarDataGridView2.DataSource = bulunanKitaplar;
            }

            /*string arananKitapAdi = textBox1.Text;
            var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitapAdi)).ToList();
            KitaplarDataGridView2.DataSource = bulunanKitaplar;*/
        }

        #endregion


        /// Listeleme Bölümü

        #region

        //Sol Bar Listeleme Butonu
        private void ListeleButonu_Click(object sender, EventArgs e)
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            Temizle();
            //var kiraliklar = db.KiralıkBilgileri.ToList();
            //KiralananlarDataGridView1.DataSource = kiraliklar.ToList();

            ListelePaneli.Visible = true;
            label1.Text = "Kiralama Paneli - Listele";
            ListelePaneli.BringToFront();

            KiralananlarDataGridView1.Columns.Clear();
            ListeleKitaplarDataGrid.Columns.Clear();
        }

        //Data Grid yenilemek için listeleme butonu
        private void ListelemeButonu_Click(object sender, EventArgs e)
        {
            Listele();
        }

        /// KiralananlarDataGrid içerisindeki değerleri labellere atama
        private void KiralananlarDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kiralikKitapId = Convert.ToInt16(KiralananlarDataGridView1.CurrentRow.Cells[1].Value.ToString());      // 1 Kitap_Id
            var kiralikKitap = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kiralikKitapId)).FirstOrDefault();

            label53.Text = kiralikKitap.Kitap_Adi.ToString();          // 53 Adi
            label42.Text = kiralikKitap.Kitap_YayinEvi.ToString();     // 42 Yayınevi
            label43.Text = kiralikKitap.Kitap_ISBN.ToString();         // 43 ISBN


            int kiralikUyeId = Convert.ToInt16(KiralananlarDataGridView1.CurrentRow.Cells[2].Value.ToString());        // 2 Uye_Id
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kiralikUyeId)).FirstOrDefault();

            label44.Text = uyeBilgisi.Uye_Adi.ToString();                 // 44 Adi ve Soyadi
            label44.Text += " " + uyeBilgisi.Uye_Soyadi.ToString();

            label50.Text = uyeBilgisi.Uye_TelNo.ToString();               // 50 Tel NO
            label51.Text = uyeBilgisi.Uye_Eposta.ToString();              // 51 Eposta
            label52.Text = uyeBilgisi.Uye_SahipOldugu.ToString();         // 52 Kiralık Sayısı


            DateTime KiralamaTarihi = DateTime.Parse(KiralananlarDataGridView1.CurrentRow.Cells[3].Value.ToString());  // 3 Kiralık
            DateTime TeslimTarihi = DateTime.Parse(KiralananlarDataGridView1.CurrentRow.Cells[4].Value.ToString());    // 4 Teslim

            label62.Text = KiralamaTarihi.ToString();         // 62 Kiralık Tarihi
            label63.Text = TeslimTarihi.ToString();           // 63 Teslim Tarihi
        }

        // Sadece günü geçenler
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                var gunuGecenler = db.KiralıkBilgileri.Where(x => x.Kalan_Gun <= 0).ToList();
                KiralananlarDataGridView1.DataSource = gunuGecenler;
            }
            else if (checkBox1.Checked == false)
            {
                Listele();
                ListeleKitaplarDataGrid.Columns.Clear();
            }
        }

        //Listele Kiralık kitap ID Arama
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                KiralananlarDataGridView1.Columns.Clear();
            }
            else
            {
                int arananKitapId = Convert.ToInt16(textBox4.Text);
                var bulunanIdler = db.KiralıkBilgileri.Where(x => x.Kitap_Id.Equals(arananKitapId)).ToList();
                KiralananlarDataGridView1.DataSource = bulunanIdler;
            }
        }

        //Listele Kitap Adı Arama
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                ListeleKitaplarDataGrid.Columns.Clear();
            }
            else
            {
                string arananKitap = textBox3.Text;
                var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitap)).ToList();
                ListeleKitaplarDataGrid.DataSource = bulunanKitaplar;
            }
        }

        // Listele kısmı Kiralık hepsini göster check box
        private void hepsiniGosterCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox4.Checked == true)
            {
                var kiraliklar = db.KiralıkBilgileri.ToList();
                KiralananlarDataGridView1.DataSource = kiraliklar;
            }
            else if (hepsiniGosterCheckBox4.Checked == false)
            {
                KiralananlarDataGridView1.Columns.Clear();
            }
        }
        #endregion

        /// Geçmişi Listele Bölümü

        #region

        //Sol Geçmişi Listele Butonu
        private void GecmisiListeleButonu_Click(object sender, EventArgs e)
        {
            db.KalanGunHesaplama13();
            db.CezaGuncelle();
            Temizle();
            GecmisPaneli.Visible = true;
            label1.Text = "Kiralama Paneli - Geçmis Kiralıklar";
            GecmisPaneli.BringToFront();
            Listele();
            GecmisKiraliklarDataGrid.Columns.Clear();
        }

        private void GecmisKiraliklarDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kiralikKitapId = Convert.ToInt16(GecmisKiraliklarDataGrid.CurrentRow.Cells[3].Value.ToString());      // 3 Kitap_Id
            var kiralikKitap = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kiralikKitapId)).FirstOrDefault();

            label87.Text = kiralikKitap.Kitap_Adi.ToString();          // 87 Adi
            label86.Text = kiralikKitap.Kitap_YayinEvi.ToString();     // 86 Yayınevi
            label85.Text = kiralikKitap.Kitap_ISBN.ToString();         // 85 ISBN


            int kiralikUyeId = Convert.ToInt16(GecmisKiraliklarDataGrid.CurrentRow.Cells[2].Value.ToString());        // 2 Uye_Id
            var uyeBilgisi = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kiralikUyeId)).FirstOrDefault();

            label77.Text = uyeBilgisi.Uye_Adi.ToString();                 // 77 Adi ve Soyadi
            label77.Text += " " + uyeBilgisi.Uye_Soyadi.ToString();

            DateTime KiralamaTarihi = DateTime.Parse(GecmisKiraliklarDataGrid.CurrentRow.Cells[4].Value.ToString());  // 4 Kiralık
            DateTime PlanlananTeslimTarihi = DateTime.Parse(GecmisKiraliklarDataGrid.CurrentRow.Cells[5].Value.ToString());    // 5 Planlanan Teslim
            DateTime TeslimEdilmeTarihi = DateTime.Parse(GecmisKiraliklarDataGrid.CurrentRow.Cells[6].Value.ToString());    // 6 Planlanan Teslim

            label76.Text = KiralamaTarihi.ToString();                  // 76 Kiralık Tarihi
            label75.Text = PlanlananTeslimTarihi.ToString();           // 75 Planlanan Teslim Tarihi
            label74.Text = TeslimEdilmeTarihi.ToString();           // 74 Planlanan Teslim Tarihi
            
            int ceza = Convert.ToInt16(GecmisKiraliklarDataGrid.CurrentRow.Cells[7].Value.ToString());        // 7 ceza
            if (ceza > 0)
            {
                label73.ForeColor = Color.Red;
                label73.Text = ceza.ToString();
            }
            else
            {
                label73.ForeColor = Color.Blue;
                label73.Text = ceza.ToString();
            }

        }

        //geçmiş kirala kirala ID arama
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                GecmisKiraliklarDataGrid.Columns.Clear();
            }
            else
            {
                int arananGecmisKiralaId = Convert.ToInt16(textBox5.Text);
                var bulunanGecmis = db.GecmisKiralikBilgileri.Where(x => x.Kirala_Id == arananGecmisKiralaId).ToList();
                GecmisKiraliklarDataGrid.DataSource = bulunanGecmis;
            }
        }

        //geçmiş Hepsini göster check box
        private void hepsiniGosterCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox3.Checked == true)
            {
                var gecmis = db.GecmisKiralikBilgileri.ToList();
                GecmisKiraliklarDataGrid.DataSource = gecmis;
            }
            else if (hepsiniGosterCheckBox3.Checked == false)
            {
                GecmisKiraliklarDataGrid.Columns.Clear();
            }
        }
        
        #endregion

    }
}