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

namespace KutuphaneOtomasyonu
{
    public partial class GirisPaneli : Form
    {
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public GirisPaneli()
        {
            InitializeComponent();
        }

        public void UserType()
        {

        }

        //Giriş Butonu Basıldığında
        private void GirisButonu_Click(object sender, EventArgs e)
        {
            String KullaniciAdiText = KullaniciAdiTextBox.Text;
            String SifreText = SifreTextBox.Text;


            //linq Sorgusu
            var calisan = db.CalisanBilgileri.Where(x => x.Calisan_KullaniciAdi.Equals(KullaniciAdiText) &&
            x.Calisan_Sifre.Equals(SifreText)).FirstOrDefault();

            var admin = db.AdminBilgileri.Where(x => x.Admin_KullaniciAdi.Equals(KullaniciAdiText) &&
            x.Admin_Sifre.Equals(SifreText)).FirstOrDefault();

            //Kullanıcı Adı ve Şifre Kontrolü
            if (admin == null)
            {
                if (calisan == null)
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
                }
                else
                {
                    calisan.Calisan_LastLogin = DateTime.Now;
                    db.SaveChanges();
                    MessageBox.Show("Çalışan Girişi Başarılı");
                    Panelislemleri panel = new Panelislemleri();
                    panel.KullaniciRolu = "Çalışan";
                    panel.KullaniciAdi = calisan.Calisan_Adi.ToString();
                    panel.Show();
                    this.Hide();
                }
            }
            else
            {
                admin.Admin_LastLogin = DateTime.Now;
                db.SaveChanges();
                MessageBox.Show("Admin Girişi Başarılı");
                Panelislemleri panel = new Panelislemleri();
                panel.KullaniciRolu = "Admin";
                panel.KullaniciAdi = admin.Admin_KullaniciAdi.ToString();
                panel.Show();
                this.Hide();
            }
        }
    }
}
