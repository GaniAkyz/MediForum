using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class YeniBaslikEklemeModel
    {
        public string Isim { get; set; }
        public int KategoriID  { get; set; }
        public List<tbl_kategori> Kategoriler  { get; set; }
        public int KullaniciID { get; set; }

        public string Error { get; set; }

    }
}