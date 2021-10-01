using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var deger = db.TBLPERSONEL.ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var deger = db.TBLPERSONEL.Find(id);
            db.TBLPERSONEL.Remove(deger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public ActionResult PersonelGetir(int id)
        {
            var deger = db.TBLPERSONEL.Find(id);
            var deger2 = db.TBLPERSONEL.Where(x => x.ID == deger.ID).ToList();
            return View(deger2);
        }
        [HttpPost]
        public ActionResult PersonelGetir(TBLPERSONEL p)
        {
            var deger = db.TBLPERSONEL.Find(p.ID);
            deger.PERSONEL = p.PERSONEL;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
       

    }
}