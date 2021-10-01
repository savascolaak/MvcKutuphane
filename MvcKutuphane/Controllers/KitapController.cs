using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(string p)
        {
            var deger = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(p))
            {
                deger = deger.Where(x => x.AD.Contains(p));
            }

            return View(deger.Where(x=>x.DURUM==true).ToList());
            
        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> Kategori = (from x in db.TBLKATEGORI.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.Kategori = Kategori;

            List<SelectListItem> Yazar = (from x in db.TBLYAZAR.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD +' '+x.SOYAD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.Yazar = Yazar;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP k)
        {
            var ktg = db.TBLKATEGORI.Where(x => x.ID == k.KATEGORI).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == k.YAZAR).FirstOrDefault();
            k.DURUM = true;
            k.TBLKATEGORI = ktg;
           
            k.TBLYAZAR = yzr;
            db.TBLKITAP.Add(k);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var deger = db.TBLKITAP.Find(id);
            deger.DURUM = false;
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KitapGetir(int id)
        {
            
            var deger = db.TBLKITAP.Find(id);
          
            
            List<SelectListItem> Kategori = (from x in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.AD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Kategorii = Kategori;
            List<SelectListItem> Yazar = (from x in db.TBLYAZAR.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD + ' ' + x.SOYAD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.Yazar = Yazar;

            
            
            return View("KitapGetir",deger);
        }
        [HttpPost]
        public ActionResult KitapGetir(TBLKITAP k)
        {
            var kitap = db.TBLKITAP.Find(k.ID);
            kitap.AD = k.AD;
            kitap.BASIMYIL = k.BASIMYIL;
            kitap.SAYFA = k.SAYFA;
            kitap.YAYINEVI = k.YAYINEVI;
            var ktg = db.TBLKATEGORI.Where(x => x.ID == k.KATEGORI).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID ==k.YAZAR).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            db.SaveChanges();
            return RedirectToAction("Index");


            //
            //var yzr = db.TBLYAZAR.Where(y => y.ID == k.YAZAR).FirstOrDefault();

            //var deger = db.TBLKITAP.Find(k.ID);
            //deger.KATEGORI = ktg.ID;
            //deger.SAYFA = k.SAYFA;
            //deger.YAYINEVI = k.YAYINEVI;
            //deger.YAZAR = yzr.ID;
            //deger.AD = k.AD;
            //db.SaveChanges();


            return View();
        }

    }
}