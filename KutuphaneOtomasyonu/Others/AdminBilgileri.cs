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
    
    public partial class AdminBilgileri
    {
        public int Admin_Id { get; set; }
        public string Admin_KullaniciAdi { get; set; }
        public string Admin_Sifre { get; set; }
        public string Admin_Tc { get; set; }
        public string Admin_TelNo { get; set; }
        public string Admin_Eposta { get; set; }
        public string Admin_Adres { get; set; }
        public Nullable<System.DateTime> Admin_LastLogin { get; set; }
        public Nullable<System.DateTime> Admin_EklenmeTarihi { get; set; }
    }
}