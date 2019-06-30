using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BaslikController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Basliklar()
        {
            BasliklarSayfasiModel model = new BasliklarSayfasiModel();
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

            model.Basliklar = (from x in db.tbl_basliks
                               where x.Onay == true
                               orderby x.EklenmeTarihi descending
                               select new BaslikModel
                               {
                                   Baslik = x.Isim,
                                   Id = x.Id,
                                   MesajSayisi = x.tbl_mesajs.Count,
                                   Kullanici = x.tbl_kullanici.Isim + " " + x.tbl_kullanici.Soyisim,
                                   Tarihi = x.EklenmeTarihi
                               }).ToList();
            return View(model);
        }



        [HttpGet]
        public ActionResult YeniBaslikEkle()
        {
            YeniBaslikEklemeModel yenimodel = new YeniBaslikEklemeModel();
            DataClasses1DataContext db = new DataClasses1DataContext();

            yenimodel.Kategoriler = (from x in db.tbl_kategoris
                                     where x.Aktif == true
                                     orderby x.Isim ascending
                                     select x).ToList();

            return View(yenimodel);
        }


        [HttpPost]
        public ActionResult YeniBaslikEkle(YeniBaslikEklemeModel gelenmodel)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            YeniBaslikEklemeModel sonucmodel = new YeniBaslikEklemeModel();

            sonucmodel.Kategoriler = (from x in db.tbl_kategoris
                                     where x.Aktif == true
                                     orderby x.Isim ascending
                                     select x).ToList();


            if (Session["Giris"] != null)
            {
                String[] girisbilgileri = Session["Giris"] as string[];
                int UserId = Convert.ToInt32(girisbilgileri[0]);

                tbl_baslik yenibaslik = new tbl_baslik();
                yenibaslik.EklenmeTarihi = DateTime.Now;
                yenibaslik.Isim = gelenmodel.Isim;
                yenibaslik.KategoriId = gelenmodel.KategoriID;
                yenibaslik.KullaniciId = UserId;
                yenibaslik.Onay = false;

                db.tbl_basliks.InsertOnSubmit(yenibaslik);
                db.SubmitChanges();

                sonucmodel.Error = "Başlık başarıyla eklendi. Lütfen Admin'in onayı için bekleyiniz.";
            }
            else
            {
                sonucmodel.Error = "Lütfen İlk önce Siteye giriş yapınız.";
            }
           

            return View(sonucmodel);
        }
    }
}