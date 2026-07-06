using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();
        // GET: Admin/DanhMucSanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DSDMSanPham()
        {

            List<DanhMucSanPham> ds = db.DanhMucSanPhams.Select(t => t).ToList<DanhMucSanPham>();
            return View(ds);
        }
        public ActionResult ThemDM()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemDM(DanhMucSanPham p)
        {
            if (ModelState.IsValid)
            {
                p.NgayTao = DateTime.Now;
                p.NgaySua = DateTime.Now;
                db.DanhMucSanPhams.Add(p);
                db.SaveChanges();
                return RedirectToAction("DSDMSanPham");
            }

            return View(p);

         
        }
        public ActionResult XoaDM(int id)
        {
            var item = db.DanhMucSanPhams.Find(id);
            if (item != null)
            {
                // Kiểm tra xem có sản phẩm nào liên kết với danh mục này không
                var productsInCategory = db.SanPhams.Where(sp => sp.IdDanhMucSanPham == id).Any();
                if (productsInCategory)
                {
                    // Nếu có sản phẩm, bạn có thể thêm một thông báo lỗi hoặc xử lý khác
                    ModelState.AddModelError("", "Không thể xóa danh mục này vì nó đang có sản phẩm liên kết.");
                    return RedirectToAction("DSDMSanPham");
                }

                db.DanhMucSanPhams.Remove(item); // Xóa đối tượng

                try
                {
                    db.SaveChanges(); // Lưu thay đổi
                    TempData["SuccessMessage"] = "Danh mục đã được xóa thành công.";
                    return RedirectToAction("DSDMSanPham"); // Chuyển hướng về trang danh sách
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi xóa danh mục: " + ex.InnerException?.Message);
                }
            }

            // Nếu không tìm thấy, trả về thông báo lỗi
            return HttpNotFound("Danh mục sản phẩm không tồn tại.");
        }
        public ActionResult SuaDMSP(int id)
        {
            var byid = db.DanhMucSanPhams.Find(id);
            return View(byid);
        }

        [HttpPost]
        public ActionResult SuaDMSP(DanhMucSanPham p)
        {
            if (!ModelState.IsValid)
            {
                return View(p); // Trả về view với lỗi nếu model không hợp lệ
            }

            // Tìm danh mục trong cơ sở dữ liệu
            var a = db.DanhMucSanPhams.Find(p.Id);
            if (a != null)
            {
                // Cập nhật các thuộc tính cần thiết
                a.TieuDe = p.TieuDe;
                

                a.MoTa = p.MoTa;
                a.NgaySua = DateTime.Now; // Cập nhật ngày sửa

                // Lưu thay đổi
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("DSDMSanPham");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật dữ liệu. Vui lòng kiểm tra và thử lại.");
                    // Bạn có thể log lỗi ở đây nếu cần
                }
            }
            else
            {
                ModelState.AddModelError("", "Danh mục không tồn tại.");
            }

            return View(p);

        }

    }
}