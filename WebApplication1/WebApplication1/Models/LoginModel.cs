using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string Error { get; set; }
    }
}