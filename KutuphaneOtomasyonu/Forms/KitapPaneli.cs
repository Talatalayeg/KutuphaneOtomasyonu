using KutuphaneOtomasyonu.Others;
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


using ZXing;
using ZXing.Rendering;
using ZXing.Presentation;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Data.Entity.Validation;

namespace KutuphaneOtomasyonu.Forms
{
    public partial class KitapPaneli : Form
    {
        public string KitapPaneliKullaniciRolu;
        public string KitapPaneliKullaniciAdi;
        public bool baglantiVar = false;

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        public KitapPaneli()
        {
            InitializeComponent();
            //ÇaprazişParçacağı sorunu için kod
            Control.CheckForIllegalCrossThreadCalls = false;

            label3.Text = "";
            label4.Text = "";
            label5.Text = "";

            eklePaneli.Visible = false;
            kaldirPaneli.Visible = false;
            guncellePaneli.Visible = false;
            listelePaneli.Visible = false;

            //internet bağlantısı
            label40.Text = "";
            
            // Bağlantı durumunu kontrol etmek için
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);

            // Bağlantı durumunu başlangıçta kontrol et
            CheckConnectionStatus();

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
            K_YazariTXT.Text = "";
            K_YayineviTXT.Text = "";
            K_BasimyiliTXT.Text = "";
            K_ISBNTXT.Text = "";
            K_DiliTXT.Text = "";
            K_CevirmenTXT.Text = "";
            K_SayfasayisiTXT.Text = "";

            comboBox1.Items.Clear();
            isbnSiteCombo.Items.Clear();
            isbnSiteCombo.Items.Add("Google Books");
            isbnSiteCombo.Items.Add("ISBNDB");

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

        public void KitapPaneli_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;

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

        //internet bağlantısı kontrolü
        #region

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            // Bağlantı durumu değiştiğinde bu olay tetiklenir
            CheckConnectionStatus();
        }

        private void CheckConnectionStatus()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // İnternet bağlantısı varsa, çevrimiçi simgesini yükle
                baglantiVar = true;
                label40.Text = "Bağlantı Çevrimiçi";
                label40.ForeColor = Color.Green;
            }
            else
            {
                // İnternet bağlantısı yoksa, çevrimdışı simgesini yükle
                baglantiVar = false;
                label40.Text = "Bağlantı Yok";
                label40.ForeColor = Color.Red;
            }
        }

        #endregion

        /// Ekle Bölümü        

        #region

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

                    try
                    {
                        db.KitapBilgileri.Add(yeniKitap);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                MessageBox.Show("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }


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

        //Kamera ile ISBN Okuma
        #region

        private void ISBNReadButton_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }
        
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void KitapPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                 ZXing.BarcodeReader barcode = new ZXing.BarcodeReader();
                 Result result = barcode.Decode((Bitmap)pictureBox1.Image);
                 if(result != null)
                 {
                    K_ISBNTXT.Text = result.ToString();
                    timer1.Stop();
                    if (captureDevice.IsRunning)
                        captureDevice.Stop();
                 }
            }
        }

        #endregion

        //google api ile siteden veri çekme
        private async void sitedenVeriCek_Click(object sender, EventArgs e)
        {
            string isbn = K_ISBNTXT.Text.Trim();
            if(K_ISBNTXT == null || K_ISBNTXT.TextLength != 13 || K_ISBNTXT.Text == "" || K_ISBNTXT.Text == null)
            {
                MessageBox.Show("Aranacak Kitabın ISBN Numarasını Giriniz..."
                                , "Hata"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
            }
            else
            {
                if (baglantiVar != true)
                {
                    MessageBox.Show("İnternet Bağlantısı Bulunmamaktadır..."
                                    , "Hata"
                                    , MessageBoxButtons.OK
                                    , MessageBoxIcon.Error);
                }
                else
                {
                    string selectedText = isbnSiteCombo.SelectedItem.ToString();
                    if (selectedText == "ISBNDB")//ISBNDB
                    {
                        string baslik = "", yazar = "1", dil = "", yayinevi = "", yayinlanmaTarihi = "", yayinlanmaYili;
                        int sayfaSayisi = 0;

                        //ISBNDB apikey
                        string ISBNapiKey = "53375_86d96560e3a2500a2848943e4281bfca";

                        string WEBSERVICE_URL = "https://api2.isbndb.com/book/" + isbn;

                        try
                        {
                            var webRequest = WebRequest.Create(WEBSERVICE_URL);

                            if (webRequest != null)
                            {
                                webRequest.Method = "GET";
                                webRequest.ContentType = "application/json";
                                webRequest.Headers["Authorization"] = ISBNapiKey;

                                //Get the response 
                                WebResponse wr = webRequest.GetResponseAsync().Result;
                                Stream receiveStream = wr.GetResponseStream();
                                StreamReader reader = new StreamReader(receiveStream);

                                string content = reader.ReadToEnd();

                                JObject json = JObject.Parse(content); 
                                JObject items = (JObject)json["book"];
                                JObject bookObject = (JObject)json["book"];

                                baslik = items["title"].ToString();
                                yayinevi = items["publisher"].ToString(); 
                                yazar = items["authors"] != null ? items["authors"].ToString() : " ";
                                yazar = yazar.Substring(6);
                                yazar = yazar.Substring(0, yazar.Length - 4);
                                sayfaSayisi = items["pages"] != null ? (int)items["pages"] : 0;
                                dil = items["language"] != null ? items["language"].ToString() : " ";
                                yayinlanmaTarihi = items["date_published"] != null ? items["date_published"].ToString() : " ";

                                //Onay Kutusu
                                DialogResult onay = MessageBox.Show("ISBN Numarası = " + isbn + "\n" +
                                                                    "Kitap Adı = " + baslik + "\n" +
                                                                    "Sayfa Sayısı = " + sayfaSayisi + "\n" +
                                                                    "Yazar(lar) = " + yazar + "\n" +
                                                                    "Dil = " + dil + "\n" +
                                                                    "Yayınevi = " + yayinevi + "\n" +
                                                                    "Basım Tarihi = " + yayinlanmaTarihi + "\n" +
                                                                    "Yerleştirme İşlemini Onaylıyor Musunuz?"
                                                                    , "Onaylama İşlemi"
                                                                    , MessageBoxButtons.YesNo);
                                if (onay == DialogResult.Yes)
                            {
                                K_AdiTXT.Text = baslik;
                                K_YazariTXT.Text = yazar;
                                K_YayineviTXT.Text = yayinevi;

                                DateTime date = DateTime.Parse(yayinlanmaTarihi);
                                yayinlanmaYili = date.Year.ToString();

                                K_BasimyiliTXT.Text = yayinlanmaYili;
                                K_DiliTXT.Text = dil;
                                K_SayfasayisiTXT.Text = sayfaSayisi.ToString();
                            }
                            else if (onay == DialogResult.No)
                            {
                                K_ISBNTXT.Text = "";
                                temizle();
                            }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else if (selectedText == "Google Books")//GOOGLE BOOKS
                    {
                        //Google api key
                        string googleapiKey = "AIzaSyDQFs40RX94VPXKF7-PSgvSDx852ywgmj4";
                        //isbn numarası
                        string url = $"https://www.googleapis.com/books/v1/volumes?q=isbn:{isbn}&key={googleapiKey}";

                        string baslik = "", yazar = "", dil = "", yayinevi = "", yayinlanmaTarihi = "", yayinlanmaYili;
                        int sayfaSayisi = 0;

                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.GetAsync(url);
                            response.EnsureSuccessStatusCode();
                            string responseBody = await response.Content.ReadAsStringAsync();

                            JObject json = JObject.Parse(responseBody);
                            JArray items = (JArray)json["items"];

                            foreach (var item in items)
                            {
                                baslik = item["volumeInfo"]["title"].ToString();
                                sayfaSayisi = item["volumeInfo"]["pageCount"] != null ? (int)item["volumeInfo"]["pageCount"] : 0;
                                yazar = item["volumeInfo"]["authors"] != null ? string.Join(", ", item["volumeInfo"]["authors"]) : " ";
                                dil = item["volumeInfo"]["language"] != null ? item["volumeInfo"]["language"].ToString() : " ";
                                yayinevi = item["volumeInfo"]["publisher"] != null ? item["volumeInfo"]["publisher"].ToString() : " ";
                                yayinlanmaTarihi = item["volumeInfo"]["publishedDate"] != null ? item["volumeInfo"]["publishedDate"].ToString() : " ";
                            }

                            //Onay Kutusu
                            DialogResult onay = MessageBox.Show("ISBN Numarası = " + isbn + "\n" +
                                                                "Kitap Adı = " + baslik + "\n" +
                                                                "Sayfa Sayısı = " + sayfaSayisi + "\n" +
                                                                "Yazar(lar) = " + yazar + "\n" +
                                                                "Dil = " + dil + "\n" +
                                                                "Yayınevi = " + yayinevi + "\n" +
                                                                "Basım Tarihi = " + yayinlanmaTarihi + "\n" +
                                                                "Yerleştirme İşlemini Onaylıyor Musunuz?"
                                                                , "Onaylama İşlemi"
                                                                , MessageBoxButtons.YesNo);
                            if (onay == DialogResult.Yes)
                            {
                                K_AdiTXT.Text = baslik;
                                K_YazariTXT.Text = yazar;
                                K_YayineviTXT.Text = yayinevi;

                                DateTime date = DateTime.Parse(yayinlanmaTarihi);
                                yayinlanmaYili = date.Year.ToString();

                                K_BasimyiliTXT.Text = yayinlanmaYili;
                                K_DiliTXT.Text = dil;
                                K_SayfasayisiTXT.Text = sayfaSayisi.ToString();
                            }
                            else if (onay == DialogResult.No)
                            {
                                K_ISBNTXT.Text = "";
                                temizle();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Site Seçiniz..."
                                        , "Hata"
                                        , MessageBoxButtons.OK
                                        , MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion


        /// Kaldırma Bölümü

        #region

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

        #endregion


        /// Guncelleme Bölümü

        #region

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

        #endregion


        /// Listeleme Bölümü
        
        #region

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

        #endregion






        //ISBN'in sadece sayı
        //Ad ve Soyad kısmının sadece harften oluşmasını sağlayan kısım

        #region

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

        #endregion

        
    }
}