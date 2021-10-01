using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{

    public class OduncController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Odunc
        public ActionResult Index()
        {
            var deger = db.TBLHAREKET.Where(x=>x.ISLEMDURUM==false).ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> Kitap = (from x in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.Kitap = Kitap;

            List<SelectListItem> Personel = (from x in db.TBLPERSONEL.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.PERSONEL,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Personel = Personel;

            List<SelectListItem> Uye = (from x in db.TBLUYELER.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.AD + " " + x.SOYAD,
                                            Value = x.ID.ToString()
                                        }).ToList();
            ViewBag.Uye = Uye;

            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var uye = db.TBLUYELER.Where(x => x.ID == p.UYE).FirstOrDefault();
            var personel = db.TBLPERSONEL.Where(x => x.ID == p.PERSONEL).FirstOrDefault();
            var kitap = db.TBLKITAP.Where(x => x.ID == p.KITAP).FirstOrDefault();
            p.TBLKITAP = kitap;
            p.TBLUYELER = uye;
            p.PERSONEL = personel.ID;
            p.ALISTARIH = DateTime.Now;
            p.IADETARIH = DateTime.Now.AddDays(7);
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Kitap");
        }
        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;

            return View("Odunciade", odn);
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETİRTARİH = p.UYEGETİRTARİH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}