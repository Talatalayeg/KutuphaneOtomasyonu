//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KutuphaneOtomasyonu.Others
{
    using System;
    using System.Collections.Generic;
    
    public partial class UyeBilgileri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UyeBilgileri()
        {
            this.GecmisKiralikBilgileri = new HashSet<GecmisKiralikBilgileri>();
            this.KiralıkBilgileri = new HashSet<KiralıkBilgileri>();
            this.OdemeBilgileri = new HashSet<OdemeBilgileri>();
        }
    
        public int Uye_Id { get; set; }
        public string Uye_Adi { get; set; }
        public string Uye_Soyadi { get; set; }
        public string Uye_Tc { get; set; }
        public string Uye_Cinsiyeti { get; set; }
        public string Uye_TelNo { get; set; }
        public string Uye_Eposta { get; set; }
        public string Uye_Adres { get; set; }
        public Nullable<int> Uye_Ceza { get; set; }
        public Nullable<int> Uye_SahipOldugu { get; set; }
        public Nullable<System.DateTime> Uye_EklenmeTarihi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GecmisKiralikBilgileri> GecmisKiralikBilgileri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KiralıkBilgileri> KiralıkBilgileri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OdemeBilgileri> OdemeBilgileri { get; set; }
    }
}
