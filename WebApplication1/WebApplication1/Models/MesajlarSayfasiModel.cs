using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MesajlarSayfasiModel
    {
        public string Id { get; set; }
        public string Kullanici { get; set; }
        public string Baslik { get; set; }
        public List<MesajModel> Mesajlar { get; set; }
        public int BaslikId { get; set; }
    }
}