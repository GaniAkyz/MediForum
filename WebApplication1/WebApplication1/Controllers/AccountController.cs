using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginmodel)
        {
            LoginModel model = new LoginModel();


            // Kullanıcı kayıtlımı diye kontrol ediyorum.
            var user = (from x in db.tbl_kullanicis
                        where x.Eposta == loginmodel.Eposta
                        && x.Sifre == loginmodel.Sifre
                        select x).FirstOrDefault();

            if(user == null)
            {
                model.Error = "E-Posta ve Şifreyle eşleşen Kullanıcı bulunamadı. Lütfen tekrar deneyiniz.";
            }
            else
            {
                //Kullanıcı varsa session başlatıyorum.
                String[] GirisBilgileri = new String[2];
                GirisBilgileri[0] = user.Id.ToString();
                GirisBilgileri[1] = user.Isim + " " + user.Soyisim;
                Session["Giris"] = GirisBilgileri;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterModel register = new RegisterModel();
            return View(register);
        }


        [HttpPost]
        public ActionResult Register(RegisterModel registermodel)
        {
            RegisterModel model = new RegisterModel();


            // Kullanıcı kayıtlımı diye kontrol ediyorum.
            var user = (from x in db.tbl_kullanicis
                        where x.Eposta == registermodel.Eposta
                        select x).FirstOrDefault();

            if (user != null)
            {
                model.Error = "E-Posta zaten kullanılıyor. Lütfen Farklı bir E-Posta Adresi deneyiniz.";
            }
            else
            {
                // Kayıt Ediyoruz.
                tbl_kullanici yeniuser = new tbl_kullanici();
                yeniuser.Isim = registermodel.Ad;
                yeniuser.Soyisim = registermodel.Soyad;
                yeniuser.Sifre = registermodel.Sifre;
                yeniuser.DogumTarihi = registermodel.DogumTarihi;
                yeniuser.Eposta = registermodel.Eposta;
                yeniuser.FakulteIsim = registermodel.OkuduguFakulte;
                yeniuser.Hakkinda = "";

                db.tbl_kullanicis.InsertOnSubmit(yeniuser);
                db.SubmitChanges();

                //Kullanıcı varsa session başlatıyorum.
                String[] GirisBilgileri = new String[2];
                GirisBilgileri[0] = yeniuser.Id.ToString();
                GirisBilgileri[1] = yeniuser.Isim + " " + yeniuser.Soyisim;
                Session["Giris"] = GirisBilgileri;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult ForgottenPassword()
        {
            ForgottenPasswordModel forgottenpassword = new ForgottenPasswordModel();
            return View(forgottenpassword);
        }


        [HttpPost]
        public ActionResult ForgottenPassword(ForgottenPasswordModel forgottenmodel)
        {
            ForgottenPasswordModel model = new ForgottenPasswordModel();


            // Kullanıcı kayıtlımı diye kontrol ediyorum.
            var user = (from x in db.tbl_kullanicis
                        where x.Eposta == forgottenmodel.Eposta
                        select x).FirstOrDefault();

            if (user == null)
            {
                model.Error = "Bu E-Posta Sistemde Kayıtlı Değil. Farklı bir E-Posta adresi deneyiniz.";
            }
            else
            {
                var fromAddress = new MailAddress("akyuzgani99@gmail.com", "Sistem Yöneticisi");
                var toAddress = new MailAddress(forgottenmodel.Eposta, user.Isim + " "+ user.Soyisim);
                const string fromPassword = "12991923Abcd";
                string subject = "MediForum - Şifremi Unuttum";
                string body = "Merhabalar. Sistemde şifrenizi unuttuğunuzu söylediniz. Şifreniz: "+user.Sifre;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                model.Error = "Şifreniz E-Posta Adresinize Gönderildi.";
            }
            return View(model);
        }
    }
}