using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        
        public ActionResult GirisYap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x=>x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);
            
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(p.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();
                Session["KullaniciAdi"] = bilgiler.KULANICIADI.ToString();
                Session["Sifre"] = bilgiler.SIFRE.ToString();
                Session["ID"] = bilgiler.ID.ToString();
                Session["Okul"] = bilgiler.OKUL.ToString();

                return RedirectToAction("Index", "Panelim");
            }
            else
            {
                return View();
            }
            
        }
    }
}