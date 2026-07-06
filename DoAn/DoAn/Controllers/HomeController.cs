using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult gioithieu()
        {
            return View();
        }
        public ActionResult lienhe()
        {
            return View();
        }
        public ActionResult sanpham(int? id)
        {
            var allCategories = db.DanhMucSanPhams.OrderBy(t => t.TieuDe).ToList();
            var selectedId = id;

            List<SanPham> products;

            if (id.HasValue)
            {
                // Lấy sản phẩm theo danh mục
                products = db.SanPhams.Where(sp => sp.IdDanhMucSanPham == id.Value).ToList();
            }
            else
            {
                // Nếu không có id, lấy tất cả sản phẩm
                products = db.SanPhams.ToList();
            }

            ViewBag.AllCategories = allCategories;
            ViewBag.SelectedId = selectedId;

            return View(products); // Trả về danh sách sản phẩm cho view
        }



    }
}