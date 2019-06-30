using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeModel
    {
        public string Id { get; set; }
        public string Kullanici { get; set; }

        public List<BaslikModel> Basliklar { get; set; }
        public List<KategoriModel> Kategoriler { get; set; }
        public List<MesajModel> Mesajlar { get; set; }
    }
}