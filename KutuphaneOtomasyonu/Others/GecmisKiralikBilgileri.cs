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
    
    public partial class GecmisKiralikBilgileri
    {
        public int Gecmis_Id { get; set; }
        public Nullable<int> Kirala_Id { get; set; }
        public Nullable<int> Uye_Id { get; set; }
        public Nullable<int> Kitap_Id { get; set; }
        public Nullable<System.DateTime> Kiralama_Tarihi { get; set; }
        public Nullable<System.DateTime> Planlanan_Teslim_Tarihi { get; set; }
        public Nullable<System.DateTime> Teslim_Edilme_Tarihi { get; set; }
        public Nullable<int> Tanimlanan_Ceza { get; set; }
    
        public virtual KitapBilgileri KitapBilgileri { get; set; }
        public virtual UyeBilgileri UyeBilgileri { get; set; }
    }
}