using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class DanhMucMenuController : Controller

    {
        QLGiayDepEntities db = new QLGiayDepEntities();


        // GET: DanhMucMenu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item = db.DanhMucs.OrderBy(X=>X.ViTri).ToList();
            return PartialView("_MenuTop",item );
        }
        public ActionResult MenuTopSanPham()
        {
            var item = db.DanhMucSanPhams.ToList();
            return PartialView("_DMSanPham", item);
        }
        public ActionResult Arrivals()
        {
            var item = db.DanhMucSanPhams.ToList();
            return PartialView("_Arrivals", item);
        }
    }
}