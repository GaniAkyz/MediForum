using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BaslikModel
    {
        public int Id { get; set; }
        public string Kullanici { get; set; }
        public int MesajSayisi { get; set; }
        public string Baslik { get; set; }
        public DateTime Tarihi { get; set; }
    }
}