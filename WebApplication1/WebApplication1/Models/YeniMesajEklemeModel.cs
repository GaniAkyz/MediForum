using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class YeniMesajEklemeModel
    {
        
        public int BaslikID  { get; set; }
        public string  Mesaj  { get; set; }
        public int KullaniciID { get; set; }
        

        public string Error { get; set; }

    }
}