using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        //GET: Uye
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(int sayfa = 1)
        {

            // var deger = db.TBLUYELER.ToList();
            var deger = db.TBLUYELER.ToList().ToPagedList(sayfa, 3);
            return View(deger);

        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");

            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
           
        }
        [HttpGet]
        public ActionResult UyeGetir(int id)
        {
            var deger = db.TBLUYELER.Where(x => x.ID == id).Select(y => y.AD + " " + y.SOYAD).FirstOrDefault();
            ViewBag.UyeAdi = deger;
            var deger2 = db.TBLUYELER.Where(x => x.ID == id).ToList();
            return View(deger2);
        }
        [HttpPost]
        public ActionResult UyeGetir(TBLUYELER p)
        {
            var deger = db.TBLUYELER.Find(p.ID);
            deger.KULANICIADI = p.KULANICIADI;
            deger.MAIL = p.MAIL;
            deger.AD = p.AD;
            deger.SIFRE = p.SIFRE;
            deger.SOYAD = p.SOYAD;
            deger.TELEFON = p.TELEFON;
            deger.OKUL = p.OKUL;
            deger.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       public ActionResult UyeSil(int id)
       {
            var deger = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(deger);
            db.SaveChanges();
            return RedirectToAction("Index");
       }
        public ActionResult UyeKitapGecmis(int id)
        {
            var deger2 = db.TBLUYELER.Where(x => x.ID == id).Select(y=>y.AD + y.SOYAD).FirstOrDefault();
            ViewBag.Uye = deger2;
            var deger = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(deger);
        }
    }
}