using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
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
                               }).Take(20).ToList();

            model.Mesajlar = (from x in db.tbl_mesajs
                              orderby x.EklenmeTarihi descending
                              select new MesajModel
                              {
                                  Mesaj = x.MesajYazısı,
                                  Id = x.Id,
                                  Baslik = x.tbl_baslik.Isim,
                                  Kullanici = x.tbl_kullanici.Isim + " " + x.tbl_kullanici.Soyisim,
                                  Tarih = x.EklenmeTarihi
                              }).Take(20).ToList();

            model.Kategoriler = (from x in db.tbl_kategoris
                                 where x.Aktif == true
                                 select new KategoriModel
                                 {
                                     KategoriAdi = x.Isim,
                                     Id = x.Id,
                                     BaslikSayisi = x.tbl_basliks.Count
                                 }).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Hakkimizda()
        {
            HakkimizdaModel model = new HakkimizdaModel();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}