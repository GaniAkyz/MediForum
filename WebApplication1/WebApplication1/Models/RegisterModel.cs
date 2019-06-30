using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RegisterModel
    {
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string OkuduguFakulte { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Error { get; set; }
    }
}