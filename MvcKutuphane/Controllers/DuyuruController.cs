using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Duyuru
        public ActionResult Index()
        {
            var deger = db.TBLDUYURULAR.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURULAR p)
        {
            db.TBLDUYURULAR.Add(p);
            p.TARIH = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            db.TBLDUYURULAR.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DuyuruDetay(TBLDUYURULAR p)
        {
            var deger = db.TBLDUYURULAR.Find(p.ID);
            
            return View("DuyuruDetay",deger);
        }
        public ActionResult DuyuruGuncelle(TBLDUYURULAR p)
        {
            var duyuru = db.TBLDUYURULAR.Find(p.ID);
            duyuru.ICERIK = p.ICERIK;
            duyuru.KATEGORI = p.KATEGORI;
            duyuru.TARIH = p.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}