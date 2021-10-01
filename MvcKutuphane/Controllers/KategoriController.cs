using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {

            var deger = db.TBLKATEGORI.Where(x=>x.DURUM == true).ToList();
            return View(deger);
             
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
          
            return View();
        } 
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI k)
        {
            db.TBLKATEGORI.Add(k);
            db.SaveChanges();
            
            return View();
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var deger = db.TBLKATEGORI.Find(id);
            var deger2 = db.TBLKATEGORI.Where(x => x.ID == id).ToList();
            return View(deger2);
        }
       
        [HttpPost]
        public ActionResult KategoriGetir(TBLKATEGORI k)
        {
            var deger = db.TBLKATEGORI.Find(k.ID);
            deger.AD = k.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
        public ActionResult KategoriSil(int id)
        {
            var deger = db.TBLKATEGORI.Find(id);
            //db.TBLKATEGORI.Remove(deger);
            deger.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

       

    }
}