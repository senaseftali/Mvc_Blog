using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitirmeBahar2.Models;

namespace BitirmeBahar2.Controllers
{
    public class DefaultController : Controller
    {
       baharbitirme2 db = new baharbitirme2();
        // GET: Default
        public ActionResult Index()
        {
            var makale = db.Makales.OrderByDescending(m => m.MakaleId).ToList();
            return View(makale);
        }
        public ActionResult Profil()
        {
            return View();
        }
        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.Makales.Where(m => m.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m => m.Tarih));
        }
        public ActionResult MakaleDetay(int id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);

        }
        public JsonResult YorumYap(string yorum, int Makaleid)
        {

            var uyeid = Session["uyeid"];
            if (yorum != null)
            {

                db.Yorums.Add(new Yorum { UyeId = Convert.ToInt32(uyeid), MakaleId = Makaleid, Icerik = yorum, Tarih = DateTime.Now });
                db.SaveChanges();
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorums.Where(y => y.YorumId == id).SingleOrDefault();
            var makale = db.Makales.Where(m => m.MakaleId == yorum.MakaleId).SingleOrDefault();
            if (yorum.UyeId == Convert.ToInt32(uyeid))
            {
                db.Yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay", "Default", new { id = makale.MakaleId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.Makales.Where(m => m.MakaleId == Makaleid).SingleOrDefault();
            makale.Okunma += 1;
            db.SaveChanges();
            return View();
        }
        public ActionResult CokOkunan()
        {
            return View(db.Makales.OrderByDescending(m => m.Okunma).Take(3));
        }
        public ActionResult EnYeniler()
        {
            return View(db.Makales.OrderByDescending(b => b.Baslik).Take(3));
        }
    }

}