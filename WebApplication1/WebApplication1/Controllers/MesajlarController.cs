using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MesajlarController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Mesajlar(int BaslikId)
        {
            MesajlarSayfasiModel model = new MesajlarSayfasiModel();
            if (Session["Giris"] == null)
            {
                // Giriş yapılmadıysa.
                model.Id = "0";
                model.Kullanici = "";
            }
            else
            {
                // Session dolu geliyor.
                // İçindeki dataları alıyorum.
                String[] girisbilgileri = Session["Giris"] as string[];
                model.Id = girisbilgileri[0];
                model.Kullanici = girisbilgileri[1];
            }
            model.Baslik = (from x in db.tbl_basliks
                            where x.Id == BaslikId
                            select x.Isim).FirstOrDefault();
            //sağdaki parametre , soldaki sayfaya gönderdiğimiz

            model.BaslikId = BaslikId;



            model.Mesajlar = (from x in db.tbl_mesajs
                               where x.BaslikId == BaslikId
                               orderby x.EklenmeTarihi ascending
                               select new MesajModel
                               {
                                   Mesaj = x.MesajYazısı,
                                   Id = x.Id,
                                   Kullanici = x.tbl_kullanici.Isim + " " + x.tbl_kullanici.Soyisim,
                                   Tarih = x.EklenmeTarihi
                               }).ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult YeniMesajEkleme(int BaslikId)
        {
            YeniMesajEklemeModel sonucmodel = new YeniMesajEklemeModel();
            sonucmodel.BaslikID = BaslikId;

            return View(sonucmodel);
        }



        [HttpPost]
        public ActionResult YeniMesajEkleme(int BaslikId, YeniMesajEklemeModel gelenmodel)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            YeniMesajEklemeModel sonucmodel = new YeniMesajEklemeModel();

            

            if (Session["Giris"] != null)
            {
                String[] girisbilgileri = Session["Giris"] as string[];
                int UserId = Convert.ToInt32(girisbilgileri[0]);

                tbl_mesaj yenimesaj = new tbl_mesaj();
                yenimesaj.EklenmeTarihi = DateTime.Now;
                yenimesaj.MesajYazısı = gelenmodel.Mesaj;
                yenimesaj.BaslikId = BaslikId;
                yenimesaj.KullanıcıId = UserId;

                db.tbl_mesajs.InsertOnSubmit(yenimesaj);
                db.SubmitChanges();

                sonucmodel.Error = "Mesaj başarıyla eklendi.";
            }
            else
            {
                sonucmodel.Error = "Lütfen İlk önce Siteye giriş yapınız.";
            }


            return RedirectToAction("Mesajlar", "Mesajlar",new { BaslikId = gelenmodel.BaslikID });
        }
    }
}