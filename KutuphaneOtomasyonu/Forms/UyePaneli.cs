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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace KutuphaneOtomasyonu.Forms
{
    public partial class UyePaneli : Form
    {
        public string UyePaneliKullaniciRolu;
        public string UyePaneliKullaniciAdi;
        public UyePaneli()
        {
            InitializeComponent();
            eklePaneli.Visible = false;
            kaldirPaneli.Visible = false;
            guncellePaneli.Visible = false;
            listelePaneli.Visible = false;
        }

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();


        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = UyePaneliKullaniciRolu;
            panel.KullaniciAdi = UyePaneliKullaniciAdi;
            panel.Show();
            this.Hide();

        }
        //Form'daki verileri ve Önizleme'deki değerleri sıfırlar
        private void Uye_temizle()
        {
            U_AdiTXT.Text = "";
            U_SoyadiTXT.Text = "";
            U_KimlikNoTXT.Text = "";
            U_TelnoTXT.Text = "";
            U_EpostaTXT.Text = "";
            U_AdresTXT.Text = "";
            U_radioE.Checked = false;
            U_radioK.Checked = false;


            label37.Text = " ";
            label39.Text = " ";
            label41.Text = " ";
            label14.Text = " ";
            label26.Visible = false;
            label27.Visible = false;
            U_kitapSayisiTXT.Visible = false;
        }

        public void listele()
        {
            var Uye = db.UyeBilgileri.ToList();
            U_EDataGridView.DataSource = Uye.ToList();
            U_EDataGridView.Columns[11].Visible = false;

            UyeKaldirDataGrid.DataSource = Uye.ToList();
            UyeKaldirDataGrid.Columns[11].Visible = false;

            UyeListeleDataGrid.DataSource = Uye.ToList();
            UyeListeleDataGrid.Columns[11].Visible = false;

            UyeGuncelleDataGrid.DataSource = Uye.ToList();
            UyeGuncelleDataGrid.Columns[11].Visible = false;

        }

        private void UyePaneli_Load(object sender, EventArgs e)
        {
            listele();

            string[] sutunBasliklari =
            {
                "Üye ID","Üye Adı","Üye Soyadı","Üye TC","Üye Cinsiyeti","Üye Tel No","Üye Eposta"
                ,"Üye Adres","Üye'nin Cezası(TL)","Sahip Oldugu Kitaplar","Eklenme Tarihi"
            };

            foreach (DataGridView dataGridView in new[] { UyeKaldirDataGrid, UyeListeleDataGrid, U_EDataGridView, UyeGuncelleDataGrid })
            {
                for (int i = 0; i < sutunBasliklari.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari[i];
                }
            }
        }





        /// Uye Ekle Bölümü 
        
        #region

        //Sol Bar Ekle Ikonu
        private void UyeEkleButonu_Click(object sender, EventArgs e)
        {
            eklePaneli.Visible = true;
            label1.Text = "Üye Paneli - Üye Ekle";
            eklePaneli.BringToFront();
            Uye_temizle();
            U_EDataGridView.Columns.Clear();
        }

        //Yeni üye Eklemek için Ekle/           Kaydet Butonu
        private void KaydetButonu_Click(object sender, EventArgs e)
        {

            if (U_KimlikNoTXT.Text == "" || U_TelnoTXT.Text == "" || U_EpostaTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK);
            }
            else
            {
                if (U_KimlikNoTXT.Text.Length != 11)
                {
                    MessageBox.Show("Kimlik NO 11 karakterden oluşmalıdır...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    string deger = U_KimlikNoTXT.Text.ToString();
                    bool tcVarMi = db.UyeBilgileri.Any(x => x.Uye_Tc == deger);

                    if (tcVarMi == true)
                    {
                        MessageBox.Show("Aynı TC numarasına sahip üye bulunuyor...", "Hata", MessageBoxButtons.OK);
                    }
                    else
                    {
                        UyeBilgileri yeniUye = new UyeBilgileri();
                        yeniUye.Uye_Adi = U_AdiTXT.Text.ToString();
                        yeniUye.Uye_Soyadi = U_SoyadiTXT.Text.ToString();
                        yeniUye.Uye_Tc = U_KimlikNoTXT.Text.ToString();
                        yeniUye.Uye_TelNo = U_TelnoTXT.Text.ToString();
                        yeniUye.Uye_Eposta = U_EpostaTXT.Text.ToString();
                        yeniUye.Uye_Adres = U_AdresTXT.Text.ToString();
                        yeniUye.Uye_Ceza = 0;
                        yeniUye.Uye_SahipOldugu = 0;

                        DateTime bugun = DateTime.Now;

                        yeniUye.Uye_EklenmeTarihi = bugun;

                        if (U_radioE.Checked == true)
                        {
                            yeniUye.Uye_Cinsiyeti = "Erkek";
                        }
                        else if (U_radioK.Checked == true)
                        {
                            yeniUye.Uye_Cinsiyeti = "Kadın";
                        }
                        db.UyeBilgileri.Add(yeniUye);
                        db.SaveChanges();

                        MessageBox.Show(yeniUye.Uye_Adi + " adlı üye başarıyla kaydedildi...", "Başarılı", MessageBoxButtons.OK);

                        listele();
                    }
                }
            }
        }

        //Yeni Üye Eklemek için Ekle/           Temizle Butonu
        private void TemizleButonu_Click(object sender, EventArgs e)
        {
            Uye_temizle();
        }

        //Üye TC ile arama
        private void E_TcBulTXT_TextChanged(object sender, EventArgs e)
        {
            if (E_TcBulTXT.Text == "")
            {
                U_EDataGridView.Columns.Clear();
            }
            else
            {
                string arananUye = E_TcBulTXT.Text.ToString();
                var bulunanUye = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananUye)).ToList();
                U_EDataGridView.DataSource = bulunanUye;
            }
        }

        // EKle kısmı hepsini göster check box
        private void hepsiniGosterCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox4.Checked == true)
            {
                var uyeler = db.UyeBilgileri.ToList();
                U_EDataGridView.DataSource = uyeler;
            }
            else if (hepsiniGosterCheckBox4.Checked == false)
            {
                U_EDataGridView.Columns.Clear();
            }
        }
        #endregion

        /// Uye Kaldır BÖLÜMÜ

        #region

        //Sol Bar Kaldır  Ikonu
        private void UyeKaldirButonu_Click(object sender, EventArgs e)
        {
            kaldirPaneli.Visible = true;
            label1.Text = "Üye Paneli - Üye Kaldır";
            kaldirPaneli.BringToFront();
            Uye_temizle();
            listele();
            UyeKaldirDataGrid.Columns.Clear();
        }

        //Data Grid içerisinde veriye tıklayınca labeli güncelliyor
        private void UyeKaldirDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(UyeKaldirDataGrid.CurrentRow.Cells[0].Value);
            var uye = db.UyeBilgileri.Where(x => x.Uye_Id == kaldir_secilenId).FirstOrDefault();

            label37.Text = uye.Uye_Id.ToString();                     // 37 ID
            label39.Text = uye.Uye_Adi.ToString();                    // 39 Adı ve Soyadı
            label39.Text += " " + uye.Uye_Soyadi.ToString();

            label41.Text = uye.Uye_SahipOldugu.ToString();           // 41 Sahip oldugu kitaplar
            label14.Text = uye.Uye_Ceza.ToString() + " TL";           // 14 Ceza

        }

        //Seçili veritabanı verisini kaldırır
        private void KaldirmaButonu_Click(object sender, EventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(UyeKaldirDataGrid.CurrentRow.Cells[0].Value);
            var Uye = db.UyeBilgileri.Where(x => x.Uye_Id == kaldir_secilenId).FirstOrDefault();
            if (Uye.Uye_Ceza != 0)
            {
                MessageBox.Show("Uyenin Cezası Bulunmaktadır\n" +
                    "Lütfen Ceza ödemesi tamamlandıktan sonra kaldırınız...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (Uye.Uye_SahipOldugu != 0)
                {
                    MessageBox.Show("Uyenin Kiraladığı Kitaplar Bulunuyor\n" +
                        "Lütfen kitapları geri alınız ve ardından kaldırınız...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult onay = MessageBox.Show(Uye.Uye_Adi + " adlı üyeyi kaldırmak istiyor musun?",
                                                                        "Onaylama İşlemi", MessageBoxButtons.YesNo);
                    if (onay == DialogResult.Yes)
                    {
                        db.UyeBilgileri.Remove(Uye);
                        db.SaveChanges();
                        listele();
                    }
                    else if (onay == DialogResult.No)
                    {

                    }
                }
            }

        }

        // TcNo ile Arama TXT
        private void U_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            if (U_TcAraTXT.Text == "")
            {
                UyeKaldirDataGrid.Columns.Clear();
            }
            else
            {
                string arananUye = U_TcAraTXT.Text.ToString();
                var bulunanUye = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananUye)).ToList();
                UyeKaldirDataGrid.DataSource = bulunanUye;
            }
        }

        // Hepsini Göster Check Box
        private void hepsiniGosterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox.Checked == true)
            {
                var uyeler = db.UyeBilgileri.ToList();
                UyeKaldirDataGrid.DataSource = uyeler;
            }
            else if (hepsiniGosterCheckBox.Checked == false)
            {
                UyeKaldirDataGrid.Columns.Clear();
            }
        }

        #endregion

        /// Uye Guncelle Bölümü

        #region

        //Sol Bar Guncelle Ikonu
        private void UyeGuncelleButonu_Click(object sender, EventArgs e)
        {
            guncellePaneli.Visible = true;
            label1.Text = "Üye Paneli - Üye Güncelle";
            guncellePaneli.BringToFront();
            Uye_temizle();
            UyeGuncelleDataGrid.Columns.Clear();
        }

        //Data Grid içerisinde veriyi textboxlara yerleştiriyor
        private void UyeGuncelleDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            U_adGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[1].Value.ToString();         // 1 ad
            U_soyadGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[2].Value.ToString();      // 2 soyad
            U_tcnoGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[3].Value.ToString();      // 3 tc
            U_telnoGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[5].Value.ToString();      // 5 telno
            U_epostaGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[6].Value.ToString();     // 6 eposta
            U_adresGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[7].Value.ToString();     // 7 adres
            U_cezaGuncelleTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[8].Value.ToString();     // 8 ceza
            U_kitapSayisiTXT.Text = UyeGuncelleDataGrid.CurrentRow.Cells[9].Value.ToString();     // 9 kitap sayisi

            if (UyeGuncelleDataGrid.CurrentRow.Cells[4].Value == null)
            {
                UG_erkekRadio.Checked = false;
                UG_kadinRadio.Checked = false;
            }
            else if (UyeGuncelleDataGrid.CurrentRow.Cells[4].Value.ToString() == "Erkek")
            {
                UG_erkekRadio.Checked = true;                                        // 4 Cinsiyet
            }
            else if (UyeGuncelleDataGrid.CurrentRow.Cells[4].Value.ToString() == "Kadın")
            {
                UG_kadinRadio.Checked = true;
            }
        }

        //DataGrid ile seçilen verinin guncellenmesi
        private void GuncellemeButonu_Click(object sender, EventArgs e)
        {
            if (U_tcnoGuncelleTXT.Text == "" || U_telnoGuncelleTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK);
            }
            else
            {
                string deger = U_tcnoGuncelleTXT.Text.ToString();
                //bool tcVarMi = db.UyeBilgileri.Any(x => x.Uye_Tc == deger);

                var digerKullanicilar = db.UyeBilgileri.Where(u => u.Uye_Tc != deger).ToList();
                bool tcVarMi = digerKullanicilar.Any(u => u.Uye_Tc == deger);

                if (tcVarMi == true)
                {
                    MessageBox.Show("Aynı TC numarasına sahip üye bulunuyor...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    var guncelle_SecilenId = Convert.ToInt16(UyeGuncelleDataGrid.CurrentRow.Cells[0].Value);
                    var Uye = db.UyeBilgileri.Where(x => x.Uye_Id == guncelle_SecilenId).FirstOrDefault();
                    Uye.Uye_Adi = U_adGuncelleTXT.Text.ToString();
                    Uye.Uye_Soyadi = U_soyadGuncelleTXT.Text.ToString();
                    Uye.Uye_Tc = U_tcnoGuncelleTXT.Text.ToString();
                    Uye.Uye_TelNo = U_telnoGuncelleTXT.Text.ToString();
                    Uye.Uye_Eposta = U_epostaGuncelleTXT.Text.ToString();
                    Uye.Uye_Adres = U_adresGuncelleTXT.Text.ToString();
                    Uye.Uye_Ceza = Convert.ToInt16(U_cezaGuncelleTXT.Text);
           //         Uye.Uye_SahipOldugu = Convert.ToInt16(U_kitapSayisiTXT.Text);
                    if (UG_kadinRadio.Checked == true)
                    {
                        Uye.Uye_Cinsiyeti = "Kadın";
                    }
                    else if (UG_erkekRadio.Checked == true)
                    {
                        Uye.Uye_Cinsiyeti = "Erkek";
                    }

                    //Onay Kutusu
                    DialogResult onay = MessageBox.Show(Uye.Uye_Adi + " adlı üyeyi güncellemek istiyor musun?",
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

        // TcNo ile Arama TXT
        private void UG_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            if (UG_TcAraTXT.Text == "")
            {
                UyeGuncelleDataGrid.Columns.Clear();
            }
            else
            {
                string arananUye = UG_TcAraTXT.Text.ToString();
                var bulunanUye = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananUye)).ToList();
                UyeGuncelleDataGrid.DataSource = bulunanUye;
            }
        }

        //Hepsini göster check box
        private void hepsiniGosterCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox2.Checked == true)
            {
                var uyeler = db.UyeBilgileri.ToList();
                UyeGuncelleDataGrid.DataSource = uyeler;
            }
            else if (hepsiniGosterCheckBox2.Checked == false)
            {
                UyeGuncelleDataGrid.Columns.Clear();
            }
        }
        #endregion


        /// Üye Listeleme Bölümü
        #region

        //Sol Bar Listeleme Ikonu
        private void UyeListeleButonu_Click(object sender, EventArgs e)
        {
            listelePaneli.Visible = true;
            label1.Text = "Üye Paneli - Üye Listele";
            listelePaneli.BringToFront();
            Uye_temizle();
            UyeListeleDataGrid.Columns.Clear();
        }

        // TcNo ile Arama TXT
        private void L_TcBulTXT_TextChanged(object sender, EventArgs e)
        {
            string arananUye = L_TcBulTXT.Text.ToString();
            var bulunanUye = db.UyeBilgileri.Where(x => x.Uye_Tc.Contains(arananUye)).ToList();
            UyeListeleDataGrid.DataSource = bulunanUye;
        }

        //Hepsini göster check box
        private void hepsiniGosterCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox3.Checked == true)
            {
                var uyeler = db.UyeBilgileri.ToList();
                UyeListeleDataGrid.DataSource = uyeler;
            }
            else if (hepsiniGosterCheckBox3.Checked == false)
            {
                UyeListeleDataGrid.Columns.Clear();
            }
        }
        #endregion


        //Kimlik ve Telno'nun sadece sayı
        //Ad ve Soyad kısmının sadece harften oluşmasını sağlayan kısım

        #region

        // EKle Bölümü
        private void U_KimlikNoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_TelnoTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_AdiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_SoyadiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        // Kaldır Bölümü
        private void U_TcAraTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        //Guncelle Bölümü
        private void U_adGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_soyadGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_tcnoGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void U_telnoGuncelleTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        //Listele Bölümü
        private void L_TcBulTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
         {
             if (checkBox1.Checked == true)
             {
                 label26.Visible = true;
                 label27.Visible = true;
                 U_kitapSayisiTXT.Visible = true;
             }
             else if (checkBox1.Checked == false)
             {
                 label26.Visible = false;
                 label27.Visible = false;
                 U_kitapSayisiTXT.Visible = false;
             }
         }

        #endregion

    }
}