using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        // GET: Panelim
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);

            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var deger = (string)Session["MAIL"];
            var deger2 = db.TBLUYELER.FirstOrDefault(x => x.MAIL == deger);
            deger2.AD = p.AD;
            deger2.SOYAD = p.SOYAD;
            deger2.MAIL = p.MAIL;
            deger2.OKUL = p.OKUL;
            deger2.SIFRE = p.SIFRE;
            deger2.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SifreGuncelle(TBLUYELER p)
        {
           
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Kitaplarim()
        {
            var uyemail = (string)Session["Mail"];
            var deger = db.TBLHAREKET.Where(x=>x.TBLUYELER.MAIL == uyemail).ToList();
            return View(deger);
        }
        public ActionResult Duyurular()
        {
            var duyuruListesi = db.TBLDUYURULAR.ToList();
            return View(duyuruListesi);

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");

        }
        
    }
}