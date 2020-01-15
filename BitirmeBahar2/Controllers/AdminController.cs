using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitirmeBahar2.Models;
namespace BitirmeBahar.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admi
        baharbitirme2 db = new baharbitirme2();
        public ActionResult Index()
        {
            ViewBag.MakaleSayisi = db.Makales.Count();
            ViewBag.YorumSayisi = db.Yorums.Count();
            ViewBag.KategoriSayisi = db.Kategoris.Count();
            ViewBag.UyeSayisi = db.Uyes.Count();
            return View();
        }
    }
}