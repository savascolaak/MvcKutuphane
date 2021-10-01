using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();

            var mesajar = db.TBLMESAJLAR.Where(x=>x.ALICI == uyemail).ToList();
            return View(mesajar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {

            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR p)
        {
           
            p.GONDEREN = (string)Session["Mail"].ToString();
            p.TARIH = DateTime.Now;
            db.TBLMESAJLAR.Add(p);
            db.SaveChanges();

            return RedirectToAction("YeniMesaj");
        }
    }
}