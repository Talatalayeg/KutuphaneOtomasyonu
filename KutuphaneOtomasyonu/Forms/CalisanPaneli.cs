using KutuphaneOtomasyonu.Others;
using KutuphaneOtomasyonu.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace KutuphaneOtomasyonu
{
    public partial class CalisanPaneli : Form
    {
        public string CalisanPaneliKullaniciRolu;
        public string CalisanPaneliKullaniciAdi;
        //Veri Tabanı bağlantısı
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public CalisanPaneli()
        {
            InitializeComponent();

            eklePaneli.Visible = false;
            kaldirPaneli.Visible = false;
            guncellePaneli.Visible = false;
            listelePaneli.Visible = false;
        }

        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = CalisanPaneliKullaniciRolu;
            panel.KullaniciAdi = CalisanPaneliKullaniciAdi;
            panel.Show();
            this.Hide();
        }


        //Form'daki verileri ve Önizleme'deki değerleri sıfırlar
        private void temizle()
        {
            C_AdiTXT.Text = "";
            C_SoyadiTXT.Text = "";
            C_KimlikNoTXT.Text = "";
            C_KullaniciAdiTXT.Text = "";
            C_SifreTXT.Text = "";
            C_TelnoTXT.Text = "";
            C_EpostaTXT.Text = "";
            C_AdresTXT.Text = "";
            radioE.Checked = false;
            radioK.Checked = false;
            erkekRadio.Checked = false;
            kadinRadio.Checked = false;

            label37.Text = " ";
            label39.Text = " ";
            label41.Text = " ";
        }

        // DataGrid içerisine veritabanı listeleme
        public void listele()
        {
            var Calisan = db.CalisanBilgileri.ToList();

            KaldirDataGrid.DataSource = Calisan.ToList();

            ListeleDataGrid.DataSource = Calisan.ToList();

            GuncelleDataGrid.DataSource = Calisan.ToList();
            GuncelleDataGrid.Columns[0].Visible = false;

            E_ListeleDataGridView.DataSource = Calisan.ToList();
        }

        private void CalisanPaneli_Load(object sender, EventArgs e)
        {

            listele();

            // Ekle - Çalışan Listesi Tablo Başlıkları
            #region

            string[] sutunBasliklari = {
            "Çalışan ID", "Çalışan Adı", "Çalışan Soyadı", "Çalışan TC", "Çalışan Cinsiyeti",
            "Çalışan Kullanıcı Adı", "Çalışan Şifre", "Çalışan Tel No", "Çalışan Eposta",
            "Çalışan Adres", "Sisteme Son Girişi" , "Eklenme Tarihi"
            };

            foreach (DataGridView dataGridView in new[] { E_ListeleDataGridView, KaldirDataGrid, GuncelleDataGrid, ListeleDataGrid })
            {
                for (int i = 0; i < sutunBasliklari.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari[i];
                }
            }

            #endregion
        }



        /// Ekle BÖLÜMÜ

        #region

        //Sol Bar Ekle Ikonu
        private void EkleButonu_Click(object sender, EventArgs e)
        {
            eklePaneli.Visible = true;
            label1.Text = "Çalısan Paneli - Çalısanları Ekle";
            eklePaneli.BringToFront();
            temizle();
            listele();
        }

        //Yeni Çalışan Eklemek için Ekle/           Kaydet Butonu
        private void KaydetButonu_Click(object sender, EventArgs e)
        {
            if (C_KimlikNoTXT.Text == "" || C_KullaniciAdiTXT.Text == "" || C_SifreTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK);
            }
            else
            {
                if (C_KimlikNoTXT.Text.Length != 11)
                {
                    MessageBox.Show("Kimlik NO 11 Haneden oluşmalıdır...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    string deger = C_KimlikNoTXT.Text.ToString();
                    bool tcVarMi = db.CalisanBilgileri.Any(x => x.Calisan_Tc == deger);

                    if (tcVarMi == true)
                    {
                        MessageBox.Show("Aynı TC numarasına sahip çalışan bulunuyor...", "Hata", MessageBoxButtons.OK);
                    }
                    else
                    {
                        CalisanBilgileri yeniCalisan = new CalisanBilgileri();
                        yeniCalisan.Calisan_Adi = C_AdiTXT.Text.ToString();
                        yeniCalisan.Calisan_Soyadi = C_SoyadiTXT.Text.ToString();
                        yeniCalisan.Calisan_Tc = C_KimlikNoTXT.Text.ToString();
                        yeniCalisan.Calisan_KullaniciAdi = C_KullaniciAdiTXT.Text.ToString();
                        yeniCalisan.Calisan_Sifre = C_SifreTXT.Text.ToString();
                        yeniCalisan.Calisan_TelNo = C_TelnoTXT.Text.ToString();
                        yeniCalisan.Calisan_Eposta = C_EpostaTXT.Text.ToString();
                        yeniCalisan.Calisan_Adres = C_AdresTXT.Text.ToString();

                        DateTime bugun = DateTime.Now;

                        yeniCalisan.Calisan_EklenmeTarihi = bugun;


                        if (radioE.Checked == true)
                        {
                            yeniCalisan.Calisan_Cinsiyeti = "Erkek";
                        }
                        else if (radioK.Checked == true)
                        {
                            yeniCalisan.Calisan_Cinsiyeti = "Kadın";
                        }

                        db.CalisanBilgileri.Add(yeniCalisan);
                        db.SaveChanges();

                        MessageBox.Show(yeniCalisan.Calisan_Adi + " adlı çalışan başarıyla kaydedildi...", "Başarılı", MessageBoxButtons.OK);

                        listele();
                    }
                }
            }
        }

        //Yeni Çalışan Eklemek için Ekle/             Temizle Butonu
        private void TemizleButonu_Click(object sender, EventArgs e)
        {
            temizle();
        }

        #endregion


        /// Kaldır BÖLÜMÜ

        #region

        //Sol Bar Kaldır  Ikonu
        private void KaldirButonu_Click(object sender, EventArgs e)
        {
            kaldirPaneli.Visible = true;
            label1.Text = "Çalısan Paneli - Çalısanları Kaldır";
            kaldirPaneli.BringToFront();
            temizle();
            listele();
        }

        //Data Grid içerisinde veriye tıklayınca labeli güncelliyor
        private void KaldirDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(KaldirDataGrid.CurrentRow.Cells[0].Value);
            var Calisan = db.CalisanBilgileri.Where(x => x.Calisan_Id == kaldir_secilenId).FirstOrDefault();

            label37.Text = Calisan.Calisan_Id.ToString();            // 37 ID
            label39.Text = Calisan.Calisan_Adi.ToString();           // 39 Adı ve Soyadı
            label39.Text += " " + Calisan.Calisan_Soyadi.ToString();

            if (KaldirDataGrid.CurrentRow.Cells[10].Value == null)
            {
                label41.Text = "";
            }
            else
            {
                DateTime SonGiris = DateTime.Parse(KaldirDataGrid.CurrentRow.Cells[10].Value.ToString());
                label41.Text = SonGiris.ToString();        // 41 En Son Girişi
            }
        }

        //Seçili veriyi onay kutusu ile kaldırma işlemi
        private void KaldirmaButonu_Click(object sender, EventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(KaldirDataGrid.CurrentRow.Cells[0].Value);
            var Calisan = db.CalisanBilgileri.Where(x => x.Calisan_Id == kaldir_secilenId).FirstOrDefault();

            //Onay Kutusu
            DialogResult onay = MessageBox.Show(Calisan.Calisan_Adi + " adlı çalışanı kaldırmak istiyor musun?",
                                                                        "Onaylama İşlemi", MessageBoxButtons.YesNo);
            if (onay == DialogResult.Yes)
            {
                db.CalisanBilgileri.Remove(Calisan);
                db.SaveChanges();
                listele();
            }
            else if (onay == DialogResult.No)
            {

            }
        }

        // TcNo ile Arama TXT
        private void K_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            string arananCalisan = K_TcAraTXT.Text.ToString();
            var bulunanCalisanlar = db.CalisanBilgileri.Where(x => x.Calisan_Tc.Contains(arananCalisan)).ToList();
            KaldirDataGrid.DataSource = bulunanCalisanlar;
        }

        #endregion


        /// Güncelleme Bölümü

        #region

        //Sol Bar Guncelle Ikonu
        private void GuncelleButonu_Click(object sender, EventArgs e)
        {
            guncellePaneli.Visible = true;
            label1.Text = "Çalısan Paneli - Çalısanları Güncelle";
            guncellePaneli.BringToFront();
            temizle();
        }

        //Data Grid içerisinde veriyi textboxlara yerleştiriyor
        private void GuncelleDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            adGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[1].Value.ToString();        // 1 ad
            soyadGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[2].Value.ToString();     // 2 soyad
            tcnoGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[3].Value.ToString();      // 3 tc
            K_adGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[5].Value.ToString();      // 5 k_adi
            sifreGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[6].Value.ToString();     // 6 sifre
            telnoGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[7].Value.ToString();     // 7 telno
            epostaGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[8].Value.ToString();    // 8 eposta
            adresGuncelleTXT.Text = GuncelleDataGrid.CurrentRow.Cells[9].Value.ToString();     // 9 adres
            if(GuncelleDataGrid.CurrentRow.Cells[4].Value == null)
            {
                temizle();
            }
            else
            {
                if (GuncelleDataGrid.CurrentRow.Cells[4].Value.ToString() == "Erkek")
                {
                    erkekRadio.Checked = true;
                    kadinRadio.Checked = false;                                       // 4 Cinsiyet
                }
                else if (GuncelleDataGrid.CurrentRow.Cells[4].Value.ToString() == "Kadın")
                {
                    erkekRadio.Checked = false;
                    kadinRadio.Checked = true;
                }
            }
        }

        //DataGrid ile seçilen verinin guncellenmesi
        private void GuncellemeButonu_Click(object sender, EventArgs e)
        {
            if (tcnoGuncelleTXT.Text == "" || K_adGuncelleTXT.Text == "" || sifreGuncelleTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK);
            }
            else
            {
                if (tcnoGuncelleTXT.Text.Length != 11)
                {
                    MessageBox.Show("Kimlik NO 11 karakterden oluşmalıdır...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    string deger = tcnoGuncelleTXT.Text.ToString();
                    //bool tcVarMi = db.CalisanBilgileri.Any(x => x.Calisan_Tc == deger);

                    var digerKullanicilar = db.CalisanBilgileri.Where(u => u.Calisan_Tc != deger).ToList();
                    bool tcVarMi = digerKullanicilar.Any(u => u.Calisan_Tc == deger);

                    if (tcVarMi == true)
                    {
                        MessageBox.Show("Aynı TC numarasına sahip çalışan bulunuyor...", "Hata", MessageBoxButtons.OK);
                    }
                    else
                    {
                        var guncelle_SecilenId = Convert.ToInt16(GuncelleDataGrid.CurrentRow.Cells[0].Value);
                        var Calisan = db.CalisanBilgileri.Where(x => x.Calisan_Id == guncelle_SecilenId).FirstOrDefault();
                        Calisan.Calisan_Adi = adGuncelleTXT.Text.ToString();
                        Calisan.Calisan_Soyadi = soyadGuncelleTXT.Text.ToString();
                        Calisan.Calisan_Tc = tcnoGuncelleTXT.Text.ToString();
                        Calisan.Calisan_KullaniciAdi = K_adGuncelleTXT.Text.ToString();
                        Calisan.Calisan_Sifre = sifreGuncelleTXT.Text.ToString();
                        Calisan.Calisan_TelNo = telnoGuncelleTXT.Text.ToString();
                        Calisan.Calisan_Eposta = epostaGuncelleTXT.Text.ToString();
                        Calisan.Calisan_Adres = adresGuncelleTXT.Text.ToString();
                        if (kadinRadio.Checked == true)
                        {
                            Calisan.Calisan_Cinsiyeti = "Kadın";
                        }
                        else if (erkekRadio.Checked == true)
                        {
                            Calisan.Calisan_Cinsiyeti = "Erkek";
                        }

                        //Onay Kutusu
                        DialogResult onay = MessageBox.Show(Calisan.Calisan_Adi + " adlı çalışanı güncellemek istiyor musun?",
                                                                                    "Onaylama İşlemi", MessageBoxButtons.YesNo);
                        if (onay == DialogResult.Yes)
                        {
                            db.SaveChanges();
                            listele();
                        }
                        else if (onay == DialogResult.No)
                        {
                            listele();
                        }
                    }
                }
            }
        }

        // TcNo ile Arama TXT
        private void G_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            string arananCalisan = G_TcAraTXT.Text.ToString();
            var bulunanCalisanlar = db.CalisanBilgileri.Where(x => x.Calisan_Tc.Contains(arananCalisan)).ToList();
            GuncelleDataGrid.DataSource = bulunanCalisanlar;
        }

        #endregion


        /// Listeleme Bölümü

        #region

        //Sol Bar Listeleme Ikonu
        private void ListeleButonu_Click(object sender, EventArgs e)
        {
            listelePaneli.Visible = true;
            label1.Text = "Çalısan Paneli - Çalısanları Listele";
            listelePaneli.BringToFront();
            temizle();
        }

        // TcNo ile Arama TXT
        private void L_TcBulTXT_TextChanged(object sender, EventArgs e)
        {
            string arananCalisan = L_TcBulTXT.Text.ToString();
            var bulunanCalisanlar = db.CalisanBilgileri.Where(x => x.Calisan_Tc.Contains(arananCalisan)).ToList();
            ListeleDataGrid.DataSource = bulunanCalisanlar;
        }

        #endregion




        //Kimlik ve Telno'nun sadece sayı
        //Ad ve Soyad kısmının sadece harften oluşmasını sağlayan kısım

        #region

        //üye Ekle Kısmı
        private void C_KimlikNoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void C_TelnoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void C_AdiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void C_SoyadiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        //Kaldır Kısmı
        private void K_UyeTcAraTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }



        //Güncelle Kısmı
        private void adGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void soyadGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void tcnoGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void telnoGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void G_TcAraTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        //Listele Kısmı

        private void L_TcBulTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}