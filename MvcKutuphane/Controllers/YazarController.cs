using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;


namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        
        public ActionResult Index(string p)
        {
            var deger = from k in db.TBLYAZAR select k;
            if (!string.IsNullOrEmpty(p))
            {
                deger = deger.Where(x => x.AD.Contains(p));
            }

            return View(deger.ToList());

            //var deger = db.TBLYAZAR.ToList();
            
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR y)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            var deger = db.TBLYAZAR.Add(y);
            db.SaveChanges();
            

            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult YazarGetir(int id)
        {
            var deger = db.TBLYAZAR.Find(id);
            var deger2 = db.TBLYAZAR.Where(x => x.ID == id).ToList();
            return View(deger2);

        }
        [HttpPost]
        public ActionResult YazarGetir(TBLYAZAR y)
        {
            var deger = db.TBLYAZAR.Find(y.ID);
            deger.DETAY = y.DETAY;
            deger.AD = y.AD;
            deger.SOYAD = y.SOYAD;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        { 
            var deger = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(deger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitapGoruntule(int id)
        {
            var deger = db.TBLYAZAR.Where(x => x.ID == id).Select(y => y.AD +" "+ y.SOYAD).First();
            ViewBag.YazarAdi = deger;
            var deger2 = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();
            return View(deger2);
        }

    }
}