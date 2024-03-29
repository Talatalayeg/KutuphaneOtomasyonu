﻿using KutuphaneOtomasyonu.Others;
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
    public partial class KitapPaneli : Form
    {
        public string KitapPaneliKullaniciRolu;
        public string KitapPaneliKullaniciAdi;

        public KitapPaneli()
        {
            InitializeComponent();

            label3.Text = "";
            label4.Text = "";
            label5.Text = "";

            eklePaneli.Visible = false;
            kaldirPaneli.Visible = false;
            guncellePaneli.Visible = false;
            listelePaneli.Visible = false;

            listele();
        }

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();


        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = KitapPaneliKullaniciRolu;
            panel.KullaniciAdi = KitapPaneliKullaniciAdi;
            panel.Show();
            this.Hide();

        }

        private void temizle()
        {
            K_AdiTXT.Text = "";
            K_ISBNTXT.Text = "";
            K_YazariTXT.Text = "";
            K_DiliTXT.Text = "";
            K_SayfasayisiTXT.Text = "";
            K_BasimyiliTXT.Text = "";
            K_YayineviTXT.Text = "";
            K_CevirmenTXT.Text = "";

            comboBox1.Items.Clear();

            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
        }

        public void listele()
        {
            var kitaplar = db.KitapBilgileri.ToList();

            ekleKitaplarDataGridView.DataSource = kitaplar.ToList();
            ekleKitaplarDataGridView.Columns[11].Visible = false;

            kaldirKitaplarDataGridView.DataSource = kitaplar.ToList();
            kaldirKitaplarDataGridView.Columns[11].Visible = false;

            GuncelleDataGridView.DataSource = kitaplar.ToList();
            GuncelleDataGridView.Columns[11].Visible = false;

            ListeleDataGridView.DataSource = kitaplar.ToList();
            ListeleDataGridView.Columns[11].Visible = false;
        }

        private void KitapPaneli_Load(object sender, EventArgs e)
        {
            listele();

            // Formdaki bütün dataGrid sutun baslikları
            string[] sutunBasliklari =
            {
                "Kitap ID","Kitap ISBN","Kitap Adı","Kitap Yazarı","Kitap Dili","Sayfa Sayısı","Basım Yılı"
                ,"Yayınevi","Kitap Çevirmeni","Odunc Durumu","Eklenme Tarihi"
            };

            foreach (DataGridView dataGridView in new[] { ekleKitaplarDataGridView , kaldirKitaplarDataGridView,
                                                          GuncelleDataGridView     , ListeleDataGridView })
            {
                for (int i = 0; i < sutunBasliklari.Length; i++)
                {
                    dataGridView.Columns[i].HeaderText = sutunBasliklari[i];
                }
            }

        }



        /// Ekle Bölümü        

        //Sol Bar Ekle Butonu
        private void EkleButonu_Click(object sender, EventArgs e)
        {
            eklePaneli.Visible = true;
            label1.Text = "Kitap Paneli - Kitap Ekle";
            eklePaneli.BringToFront();
            temizle();
        }

        //Kaydetme Butonu
        private void KaydetmeButonu_Click(object sender, EventArgs e)
        {
            if (K_AdiTXT.Text == "" || K_ISBNTXT.Text == "" || K_YayineviTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string deger = K_ISBNTXT.Text.ToString();
                bool ISBNVarMi = db.KitapBilgileri.Any(x => x.Kitap_ISBN == deger);

                if (ISBNVarMi == true)
                {
                    MessageBox.Show("Aynı ISBN numarasına sahip Kitap bulunuyor...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    KitapBilgileri yeniKitap = new KitapBilgileri();
                    yeniKitap.Kitap_Adi = K_AdiTXT.Text.ToString();
                    yeniKitap.Kitap_ISBN = K_ISBNTXT.Text.ToString();
                    yeniKitap.Kitap_Yazari = K_YazariTXT.Text.ToString();
                    yeniKitap.Kitap_Dili = K_DiliTXT.Text.ToString();
                    yeniKitap.Kitap_SayfaSayisi = K_SayfasayisiTXT.Text.ToString();
                    yeniKitap.Kitap_BasimYili = K_BasimyiliTXT.Text.ToString();
                    yeniKitap.Kitap_YayinEvi = K_YayineviTXT.Text.ToString();
                    yeniKitap.Kitap_Cevirmen = K_CevirmenTXT.Text.ToString();
                    yeniKitap.K_OduncDurumu = "Hazır";


                    DateTime bugun = DateTime.Now;

                    yeniKitap.Kitap_EklenmeTarihi = bugun;

                    db.KitapBilgileri.Add(yeniKitap);
                    db.SaveChanges();


                    MessageBox.Show(yeniKitap.Kitap_Adi + " adlı kitap başarıyla kaydedildi...", "Başarılı", MessageBoxButtons.OK);

                    listele();
                }
            }
        }

        //Girilen Değerleri Temizlemek için
        private void TemizlemeButonu_Click(object sender, EventArgs e)
        {
            temizle();
        }






        /// Kaldırma Bölümü

        /// Sol Bar Kaldır Butonu
        private void KaldirButonu_Click(object sender, EventArgs e)
        {
            kaldirPaneli.Visible = true;
            label1.Text = "Kitap Paneli - Kitap Kaldır";
            kaldirPaneli.BringToFront();
            temizle();
        }

        /// DataGrid Seçili bilgiye label ile guncelleme
        private void kaldirKitaplarDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(kaldirKitaplarDataGridView.CurrentRow.Cells[0].Value);
            var kitap = db.KitapBilgileri.Where(x => x.Kitap_Id == kaldir_secilenId).FirstOrDefault();

            label4.Text = kitap.Kitap_Adi;            // 3 Kitap Adı
            label3.Text = kitap.Kitap_ISBN;           // 4 Kitap ISBN
            label5.Text = kitap.Kitap_YayinEvi;       // 5 Kitap Yayınevi
        }

        /// Kaldır Butonu
        private void KaldirmaButonu_Click(object sender, EventArgs e)
        {
            int kaldir_secilenId = Convert.ToInt16(kaldirKitaplarDataGridView.CurrentRow.Cells[0].Value);
            var kitap = db.KitapBilgileri.Where(x => x.Kitap_Id == kaldir_secilenId).FirstOrDefault();

            // 9 OduncDurumu


            if (kaldirKitaplarDataGridView.CurrentRow.Cells[9].Value.ToString() == "Kiralık")
            {
                MessageBox.Show("Seçili Kitap Kiralık Durumdadır. \nKitabı güncelleyin ya da Üyeye ile İletişime Geçin...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kaldirKitaplarDataGridView.CurrentRow.Cells[9].Value.ToString() == "Hazır")
            {
                //Onay Kutusu
                DialogResult onay = MessageBox.Show(kitap.Kitap_Adi + " adlı kitabı kaldırmak istiyor musun?",
                                                                            "Onaylama İşlemi", MessageBoxButtons.YesNo);
                if (onay == DialogResult.Yes)
                {
                    db.KitapBilgileri.Remove(kitap);
                    db.SaveChanges();
                    listele();
                }
                else if (onay == DialogResult.No)
                {
                    listele();
                }
            }
        }

        // Kitap Adı ile Arama
        private void K_AdiBulTXT_TextChanged(object sender, EventArgs e)
        {
            string arananKitap = K_AdiBulTXT.Text.ToString();
            var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitap)).ToList();
            kaldirKitaplarDataGridView.DataSource = bulunanKitaplar;
        }






        /// Guncelleme Bölümü

        // Sol Bar Guncelleme Butonu
        private void GuncelleButonu_Click(object sender, EventArgs e)
        {
            guncellePaneli.Visible = true;
            label1.Text = "Kitap Paneli - Kitap Güncelle";
            guncellePaneli.BringToFront();
            temizle();
            comboBox1.Items.Add("Kiralık");
            comboBox1.Items.Add("Hazır");
        }

        // DataGrid verileri TXTBoxlara yerleştirir
        private void GuncelleDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KG_AdiTXT.Text = GuncelleDataGridView.CurrentRow.Cells[2].Value.ToString();          // 2 Adi
            KG_ISBNTXT.Text = GuncelleDataGridView.CurrentRow.Cells[1].Value.ToString();         // 1 ISBN
            KG_YazariTXT.Text = GuncelleDataGridView.CurrentRow.Cells[3].Value.ToString();       // 3 Yazari
            KG_DiliTXT.Text = GuncelleDataGridView.CurrentRow.Cells[4].Value.ToString();         // 4 Dili
            KG_SayisiTXT.Text = GuncelleDataGridView.CurrentRow.Cells[5].Value.ToString();       // 5 S.Sayisi
            KG_BasimYiliTXT.Text = GuncelleDataGridView.CurrentRow.Cells[6].Value.ToString();    // 6 Basimyili
            KG_YayineviTXT.Text = GuncelleDataGridView.CurrentRow.Cells[7].Value.ToString();     // 7 Yayinevi
            KG_CevirmenTXT.Text = GuncelleDataGridView.CurrentRow.Cells[8].Value.ToString();     // 8 Cevirmen
            if (GuncelleDataGridView.CurrentRow.Cells[9].Value.ToString() == "Kiralık")
            {
                comboBox1.Text = "Kiralık";
            }                                                                // 9 Odunc Durumu
            else
            {
                comboBox1.Text = "Hazır";
            }
        }

        // TXTBox'daki veriyi veritabanına günceller
        private void GuncellemeButonu_Click(object sender, EventArgs e)
        {
            if (KG_AdiTXT.Text == "" || KG_ISBNTXT.Text == "" || KG_YayineviTXT.Text == "")
            {
                MessageBox.Show("Kırmızı İşaretli alanları doldurunuz...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string deger = KG_ISBNTXT.Text.ToString();
                //bool ISBNVarMi = db.KitapBilgileri.Any(x => x.Kitap_ISBN == deger);

                var digerKitaplar = db.KitapBilgileri.Where(u => u.Kitap_ISBN != deger).ToList();
                bool ISBNVarMi = digerKitaplar.Any(u => u.Kitap_ISBN == deger);

                if (ISBNVarMi == true)
                {
                    MessageBox.Show("Aynı ISBN numarasına sahip Kitap bulunuyor...", "Hata", MessageBoxButtons.OK);
                }
                else
                {
                    var guncelle_SecilenId = Convert.ToInt16(GuncelleDataGridView.CurrentRow.Cells[0].Value);
                    var kitap = db.KitapBilgileri.Where(x => x.Kitap_Id == guncelle_SecilenId).FirstOrDefault();

                    kitap.Kitap_Adi = KG_AdiTXT.Text.ToString();
                    kitap.Kitap_ISBN = KG_ISBNTXT.Text.ToString();
                    kitap.Kitap_Yazari = KG_YazariTXT.Text.ToString();
                    kitap.Kitap_Dili = KG_DiliTXT.Text.ToString();
                    kitap.Kitap_SayfaSayisi = KG_SayisiTXT.Text.ToString();
                    kitap.Kitap_BasimYili = KG_BasimYiliTXT.Text.ToString();
                    kitap.Kitap_YayinEvi = KG_YayineviTXT.Text.ToString();
                    kitap.Kitap_Cevirmen = KG_CevirmenTXT.Text.ToString();
                    if (comboBox1.Text == "Kiralık")
                    {
                        kitap.K_OduncDurumu = "Kiralık";
                    }
                    else if (comboBox1.Text == "Hazır")
                    {
                        kitap.K_OduncDurumu = "Hazır";
                    }

                    //Onay Kutusu
                    DialogResult onay = MessageBox.Show(kitap.Kitap_Adi + " adlı kitabı güncellemek istiyor musun?",
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

        // Kitap Adı ile Arama
        private void KG_AdiAraTXT_TextChanged(object sender, EventArgs e)
        {
            string arananKitap = KG_AdiAraTXT.Text.ToString();
            var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitap)).ToList();
            GuncelleDataGridView.DataSource = bulunanKitaplar;
        }





        /// Listeleme Bölümü

        // Sol Bar Listeleme Butonu
        private void ListeleButonu_Click(object sender, EventArgs e)
        {
            listelePaneli.Visible = true;
            label1.Text = "Kitap Paneli - Kitap Listele";
            listelePaneli.BringToFront();
            temizle();
        }

        // Kitap adıyla arama
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            string arananKitap = listeleAraTXT.Text;
            var bulunanKitaplar = db.KitapBilgileri.Where(x => x.Kitap_Adi.Contains(arananKitap)).ToList();
            ListeleDataGridView.DataSource = bulunanKitaplar;
        }








        //ISBN'in sadece sayı
        //Ad ve Soyad kısmının sadece harften oluşmasını sağlayan kısım

        // EKle Bölümü

        private void K_ISBNTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void K_SayfasayisiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void K_BasimyiliTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void K_DiliTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        // Güncelle Bölümü

        private void KG_ISBNTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void KG_SayisiTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void KG_BasimYiliTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void KG_DiliTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}
