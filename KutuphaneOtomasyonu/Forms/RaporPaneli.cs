using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.AccessControl;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;

using KutuphaneOtomasyonu.Others;

using OfficeOpenXml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KutuphaneOtomasyonu.Forms
{
    public partial class RaporPaneli : Form
    {
        public string RaporPaneliKullaniciRolu;
        public string RaporPaneliKullaniciAdi;

        //Veri Tabanı bağlantısı
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public RaporPaneli()
        {
            // EPPlus Lisans
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;

            InitializeComponent();
        }

        private void RaporPaneli_Load(object sender, EventArgs e)
        {
            Listele();
            kiralikRaporlaPaneli.Visible = false;
            odemeRaporlaPaneli.Visible = false;
        }

        private void Listele()
        {
            var kiraliklar = db.KiralıkBilgileri.ToList();
            guncelKiraliklarDataGrid.DataSource = kiraliklar.ToList();
            guncelKiraliklarDataGrid.Columns[6].Visible = false;
            guncelKiraliklarDataGrid.Columns[7].Visible = false;

            var gecmiskiraliklar = db.GecmisKiralikBilgileri.ToList();
            gecmisKiraliklarDataGrid.DataSource = gecmiskiraliklar.ToList();
            gecmisKiraliklarDataGrid.Columns[8].Visible = false;
            gecmisKiraliklarDataGrid.Columns[9].Visible = false;

            var odemeler = db.OdemeBilgileri.ToList();
            odemelerDataGrid.DataSource = odemeler.ToList();
            odemelerDataGrid.Columns[5].Visible = false;
        }

        // Panel solundaki Geri Don Butonu
        private void GeriDonButonu_Click(object sender, EventArgs e)
        {
            Panelislemleri panel = new Panelislemleri();
            panel.KullaniciRolu = RaporPaneliKullaniciRolu;
            panel.KullaniciAdi = RaporPaneliKullaniciAdi;
            panel.Show();
            this.Hide();
        }

        // Kiralıkları raporla Kısmı

        #region

        //Form soldaki listele butonu
        private void ListeleButonu_Click(object sender, EventArgs e)
        {
            label1.Text = "Rapor Paneli - Kiralıkları Raporla";
            kiralikRaporlaPaneli.BringToFront();
            kiralikRaporlaPaneli.Visible = true;
            Listele();
            guncelKiraliklarDataGrid.Columns.Clear();
            gecmisKiraliklarDataGrid.Columns.Clear();
        }

        // Excel'e aktar butonu
        private void exceleAktarButonu_Click(object sender, EventArgs e)
        {
            DateTime bas = baslangicdateTime.Value;
            DateTime son = bitisdateTime.Value;
            KitapRaporunuOlustur(bas, son);
        }

        // Excel'e aktarma Fonksiyonu
        public void KitapRaporunuOlustur(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            //Güncel Kitap Bilgileri
            var guncelKitaplar = db.KiralıkBilgileri
                           .Where(br => br.Kiralama_Tarihi >= baslangicTarihi && br.Kiralama_Tarihi <= bitisTarihi)
                           .ToList();

            //Geçmiş Kitap Bilgileri
            var gecmisKitaplar = db.GecmisKiralikBilgileri
                           .Where(br => br.Kiralama_Tarihi >= baslangicTarihi && br.Kiralama_Tarihi <= bitisTarihi)
                           .ToList();

            // Excel dosyası oluşturmak için EPPlus kütüphanesini kullanabilirsiniz
            ExcelPackage excelPaket = new ExcelPackage();
            ExcelWorksheet worksheet = excelPaket.Workbook.Worksheets.Add("Kiralanan Kitaplar");

            worksheet.Cells[1, 2].Value = "GüNCEL KIRALIKLAR";
            worksheet.Cells[1, 9].Value = "GEÇMİŞ KIRALIKLAR";

            // Sütun genişliklerini ayarlama
            worksheet.Column(1).Width = 35; // "Kitap Adı" sütunu genişliği
            worksheet.Column(7).Width = 35; //geçmiş

            worksheet.Column(2).Width = 25; // "Üye Adı ve Soyadı" sütunu genişliği
            worksheet.Column(8).Width = 25; //geçmiş

            worksheet.Column(3).Width = 18; // "Kiralama Tarihi" sütunu genişliği
            worksheet.Column(9).Width = 18; //geçmiş

            worksheet.Column(4).Width = 18; // "Teslim Tarihi" sütunu genişliği
            worksheet.Column(10).Width = 18; //geçmiş planlanan Teslim tarihi

            worksheet.Column(11).Width = 18; //geçmiş teslim tarihi

            worksheet.Column(5).Width = 13; // "Ceza Miktarı" sütunu genişliği
            worksheet.Column(12).Width = 13; //geçmiş


            worksheet.Cells[3, 14].Value = "Başlangıç Tarihi";
            worksheet.Cells[3, 15].Value = "Bitiş Tarihi";
            worksheet.Column(14).Width = 20;
            worksheet.Column(15).Width = 20;

            DateTime bas = baslangicdateTime.Value;
            DateTime son = bitisdateTime.Value;

            worksheet.Cells[4, 14].Value = bas;
            worksheet.Cells[4, 14].Style.Numberformat.Format = "dd-mm-yyyy";
            worksheet.Cells[4, 15].Value = son;
            worksheet.Cells[4, 15].Style.Numberformat.Format = "dd-mm-yyyy";

            worksheet.Cells[6, 14].Value = "Güncel Kıralıklar Adeti";
            worksheet.Cells[6, 15].Value = "Geçmiş Kıralıklar Adeti";

            worksheet.Cells[9, 15].Value = "Tanımlanan Toplam Ceza";
            worksheet.Cells[9, 14].Value = "Güncel Toplam Ceza";

            //Güncel Kiralıklar Kısmı

            #region

            // Güncel Kiralıklar Başlıklar
            worksheet.Cells[3, 1].Value = "Kitap Adı";
            worksheet.Cells[3, 2].Value = "Üye Adı ve Soyadı";
            worksheet.Cells[3, 3].Value = "Kiralama Tarihi";
            worksheet.Cells[3, 4].Value = "Teslim Tarihi";
            worksheet.Cells[3, 5].Value = "Ceza Miktarı";



            // Verileri doldurma
            int row = 4;
            int guncelToplam = 0;
            int guncelToplamCeza = 0;

            foreach (var kitap in guncelKitaplar)
            {
                var kitapBilgileri = db.KitapBilgileri.Where(x => x.Kitap_Id.Equals(kitap.Kitap_Id)).FirstOrDefault();
                var uyeBilgileri = db.UyeBilgileri.Where(x => x.Uye_Id.Equals(kitap.Uye_Id)).FirstOrDefault();

                worksheet.Cells[row, 1].Value = kitapBilgileri.Kitap_Adi.ToString();                //Kitap Adı
                worksheet.Cells[row, 2].Value = uyeBilgileri.Uye_Adi.ToString() + " " + uyeBilgileri.Uye_Soyadi.ToString(); //Üye adı ve soyadı
                worksheet.Cells[row, 3].Value = kitap.Kiralama_Tarihi;
                worksheet.Cells[row, 3].Style.Numberformat.Format = "dd-mm-yyyy";

                worksheet.Cells[row, 4].Value = kitap.Teslim_Tarihi;
                worksheet.Cells[row, 4].Style.Numberformat.Format = "dd-mm-yyyy";

                worksheet.Cells[row, 5].Value = kitap.Kalan_Gun * -10 + " TL";
                guncelToplamCeza += Convert.ToInt16(kitap.Kalan_Gun * -10);
                guncelToplam++;
                row++;
            }

            worksheet.Cells[10, 14].Value = guncelToplamCeza + " Tl";
            worksheet.Cells[7, 14].Value = guncelToplam;

            #endregion


            //Geçmiş Kiralıklar Kısmı

            #region

            // Geçmiş Kiralıklar Başlıklar
            worksheet.Cells[3, 7].Value = "Kitap Adı";
            worksheet.Cells[3, 8].Value = "Üye Adı ve Soyadı";
            worksheet.Cells[3, 9].Value = "Kiralama Tarihi";
            worksheet.Cells[3, 10].Value = "Planlanan Tarihi";
            worksheet.Cells[3, 11].Value = "Teslim Edilme Tarihi";
            worksheet.Cells[3, 12].Value = "Tanımlanan Ceza";

            // Verileri doldurma
            int gecmisRow = 4;
            int gecmisColumn = 7;
            int gecmisToplam = 0;
            int toplamCeza = 0;

            foreach (var kitap in gecmisKitaplar)
            {
                var kitapBilgileri = db.KitapBilgileri.Where(x => x.Kitap_Id == kitap.Kitap_Id).FirstOrDefault();
                var uyeBilgileri = db.UyeBilgileri.Where(x => x.Uye_Id == kitap.Uye_Id).FirstOrDefault();

                worksheet.Cells[gecmisRow, gecmisColumn].Value = kitapBilgileri.Kitap_Adi.ToString();                //Kitap Adı
                
                worksheet.Cells[gecmisRow, gecmisColumn + 1].Value = uyeBilgileri.Uye_Adi.ToString() + " " + uyeBilgileri.Uye_Soyadi.ToString(); //Üye adı ve soyadı
                
                worksheet.Cells[gecmisRow, gecmisColumn + 2].Value = kitap.Kiralama_Tarihi;
                worksheet.Cells[gecmisRow, gecmisColumn + 2].Style.Numberformat.Format = "dd-mm-yyyy";

                worksheet.Cells[gecmisRow, gecmisColumn + 3].Value = kitap.Planlanan_Teslim_Tarihi;
                worksheet.Cells[gecmisRow, gecmisColumn + 3].Style.Numberformat.Format = "dd-mm-yyyy";

                worksheet.Cells[gecmisRow, gecmisColumn + 4].Value = kitap.Teslim_Edilme_Tarihi;
                worksheet.Cells[gecmisRow, gecmisColumn + 4].Style.Numberformat.Format = "dd-mm-yyyy";

                worksheet.Cells[gecmisRow, gecmisColumn + 5].Value = kitap.Tanimlanan_Ceza + " TL";
                toplamCeza += Convert.ToInt16(kitap.Tanimlanan_Ceza);
                gecmisToplam++;
                gecmisRow++;
            }
            worksheet.Cells[7, 15].Value = gecmisToplam;
            worksheet.Cells[10, 15].Value = toplamCeza + " TL";

            #endregion

            // Excel dosyasını kaydetme
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Dosyası|*.xlsx";
            saveDialog.Title = "Excel Dosyasını Kaydet";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                excelPaket.SaveAs(new FileInfo(saveDialog.FileName));
                MessageBox.Show("Rapor başarıyla oluşturuldu ve kaydedildi.");
            }
        }

        // Kiralık kitapların tamamını göster
        private void hepsiniGosterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox.Checked == true)
            {
                var kiraliklar = db.KiralıkBilgileri.ToList();
                guncelKiraliklarDataGrid.DataSource = kiraliklar;
            }
            else if (hepsiniGosterCheckBox.Checked == false)
            {
                guncelKiraliklarDataGrid.Columns.Clear();
            }
        }

        // Geçniş Kiralık kitapların tamamını göster
        private void hepsiniGosterCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (hepsiniGosterCheckBox2.Checked == true)
            {
                var gecmisKiraliklar = db.GecmisKiralikBilgileri.ToList();
                gecmisKiraliklarDataGrid.DataSource = gecmisKiraliklar;
            }
            else if (hepsiniGosterCheckBox2.Checked == false)
            {
                gecmisKiraliklarDataGrid.Columns.Clear();
            }
        }

        // Guncel Kiralık Kirala ID Arama
        private void guncelKiralikTXT_TextChanged(object sender, EventArgs e)
        {
            if (guncelKiralikTXT.Text == "")
            {
                guncelKiraliklarDataGrid.Columns.Clear();
            }
            else
            {
                int arananKitapId = Convert.ToInt16(guncelKiralikTXT.Text);
                var bulunanIdler = db.KiralıkBilgileri.Where(x => x.Kitap_Id.Equals(arananKitapId)).ToList();
                guncelKiraliklarDataGrid.DataSource = bulunanIdler;
            }
        }

        // Geçmiş Kiralık Kirala ID Arama
        private void gecmisKiralikTXT_TextChanged(object sender, EventArgs e)
        {
            if (gecmisKiralikTXT.Text == "")
            {
                gecmisKiraliklarDataGrid.Columns.Clear();
            }
            else
            {
                int arananKiralaId = Convert.ToInt16(gecmisKiralikTXT.Text);
                var bulunanIdler = db.GecmisKiralikBilgileri.Where(x => x.Kirala_Id == arananKiralaId).ToList();
                gecmisKiraliklarDataGrid.DataSource = bulunanIdler;
            }
        }
        
        #endregion

        // Ödeme raporla Kısmı

        #region

        // Form soldaki odeme Raporla Butonu
        private void odemeRaporlaButonu_Click(object sender, EventArgs e)
        {
            label1.Text = "Rapor Paneli - Ödemeleri Raporla";
            odemeRaporlaPaneli.BringToFront();
            odemeRaporlaPaneli.Visible = true;
            odemelerDataGrid.Columns.Clear();
        }

        //Ödemeleri aktarma butonu
        private void odemeleriAktar_Click(object sender, EventArgs e)
        {
            DateTime bas = odeme_baslangicDate.Value;
            DateTime son = odeme_bitisDate.Value;
            OdemeRaporunuOLustur(bas, son);
        }

        // Excel'e aktarma Fonksiyonu
        public void OdemeRaporunuOLustur(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            // Burada belirtilen tarih aralığındaki kiralanan kitapları veritabanından alabilirsiniz
            var odemeler = db.OdemeBilgileri
                           .Where(br => br.Odeme_Tarihi >= baslangicTarihi && br.Odeme_Tarihi <= bitisTarihi)
                           .ToList();

            // Excel dosyası oluşturmak için EPPlus kütüphanesini kullanabilirsiniz
            ExcelPackage excelPaket = new ExcelPackage();
            ExcelWorksheet worksheet = excelPaket.Workbook.Worksheets.Add("Yapılan Ödemeler");

            // Başlıklar
            worksheet.Cells[1, 1].Value = "Üye Adı ve Soyadı";
            worksheet.Cells[1, 2].Value = "Ödeme No";
            worksheet.Cells[1, 3].Value = "Ödenen Tutar";
            worksheet.Cells[1, 4].Value = "Ödeme Tarihi";


            worksheet.Cells[1, 7].Value = "Başlangıç Tarihi";
            worksheet.Cells[1, 8].Value = "Bitiş Tarihi";
            worksheet.Column(7).Width = 20;
            worksheet.Column(8).Width = 20;
            worksheet.Cells[4, 7].Value = "Toplam Ödeme Tutarı";
            worksheet.Cells[4, 8].Value = "Toplam Adet";


            DateTime bas = odeme_baslangicDate.Value;
            DateTime son = odeme_bitisDate.Value;
            worksheet.Cells[2, 7].Value = bas;
            worksheet.Cells[2, 7].Style.Numberformat.Format = "dd-mm-yyyy";
            worksheet.Cells[2, 8].Value = son;
            worksheet.Cells[2, 8].Style.Numberformat.Format = "dd-mm-yyyy";


            // Verileri doldurma
            int row = 2;

            int toplamOdeme = 0, toplamAdet = 0;

            foreach (var odeme in odemeler)
            {
                var uyeBilgileri = db.UyeBilgileri.Where(x => x.Uye_Id == odeme.Uye_Id).FirstOrDefault();

                worksheet.Cells[row, 1].Value = uyeBilgileri.Uye_Adi.ToString() + " " + uyeBilgileri.Uye_Soyadi.ToString(); //Üye adı ve soyadı
                worksheet.Cells[row, 2].Value = odeme.Odeme_No;

                worksheet.Cells[row, 3].Value = odeme.Odenen_Tutar;
                toplamOdeme += Convert.ToInt16(odeme.Odenen_Tutar);

                worksheet.Cells[row, 4].Value = odeme.Odeme_Tarihi;
                worksheet.Cells[row, 4].Style.Numberformat.Format = "dd-mm-yyyy";
                toplamAdet++;

                row++;
            }

            worksheet.Cells[5, 7].Value = toplamOdeme +" TL";
            worksheet.Cells[5, 8].Value = toplamAdet;

            // Sütun genişliklerini ayarlama
            worksheet.Column(1).Width = 25; // "Üye Adı ve Soyadı" sütunu genişliği
            worksheet.Column(2).Width = 20; // "Ödeme No" sütunu genişliği
            worksheet.Column(3).Width = 18; // "Ödenen Tutar" sütunu genişliği
            worksheet.Column(4).Width = 18; // "Ödeme Tarihi" sütunu genişliği

            // Excel dosyasını kaydetme
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Dosyası|*.xlsx";
            saveDialog.Title = "Excel Dosyasını Kaydet";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                excelPaket.SaveAs(new FileInfo(saveDialog.FileName));
                MessageBox.Show("Rapor başarıyla oluşturuldu ve kaydedildi.");
            }
        }

        // Geçmiş Ödeme TC Arama
        private void G_TcAraTXT_TextChanged(object sender, EventArgs e)
        {
            if (G_TcAraTXT.Text == "")
            {
                odemelerDataGrid.Columns.Clear();
            }
            else
            {
                string arananNo = G_TcAraTXT.Text;
                var bulunanOdemeler = db.OdemeBilgileri.Where(x => x.Odeme_No.Contains(arananNo)).ToList();
                odemelerDataGrid.DataSource = bulunanOdemeler;
            }
        }

        // Ödeme hepsini göster CheckBox
        private void gecmisOdemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gecmisOdemeCheckBox.Checked == true)
            {
                var odemeler = db.OdemeBilgileri.ToList();
                odemelerDataGrid.DataSource = odemeler;
            }
            else if (gecmisOdemeCheckBox.Checked == false)
            {
                odemelerDataGrid.Columns.Clear();
            }
        }

        #endregion

    }
}
