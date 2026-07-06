using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class SanPhamController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_ItemId (int id)
        {
            var items = db.SanPhams.Where(x => x.IdDanhMucSanPham== id).ToList();
            return PartialView("_Partial_ItemId", items);
        }
        public ActionResult GetAllProducts()
        {
            var products = db.SanPhams.ToList(); // Hoặc điều kiện của bạn
            return PartialView("SPPartial", products);
        }

        public ActionResult SPPartial()
        {
            List<SanPham> sp = db.SanPhams.Select(s => s).ToList<SanPham>();
            return View(sp);
        }
        public ActionResult SPDanhMuc(int id) 
        { 
            List<SanPham> lst = db.SanPhams.Where(t => t.IdDanhMucSanPham == id).ToList();
            return View(lst); 
        }
        public ActionResult Xemchitiet(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            SanPham ctsp = db.SanPhams.FirstOrDefault(t => t.Id == id.Value); 
            if (ctsp == null)
            {
                return HttpNotFound(); 
            }
            return View(ctsp);
        }
        public ActionResult TimKiem(string tenSanPham)
        {
            System.Diagnostics.Debug.WriteLine("Tìm kiếm với từ khóa: " + tenSanPham);

            if (string.IsNullOrWhiteSpace(tenSanPham))
            {
                return View(new List<SanPham>()); // Trả về danh sách rỗng nếu không có từ khóa
            }

            ViewBag.SearchTerm = tenSanPham; // Ghi nhận từ khóa tìm kiếm
            var products = db.SanPhams
                .Where(p => p.TieuDe.ToLower().Contains(tenSanPham.ToLower()))
                .ToList();

            return View(products);
        }
    }
}