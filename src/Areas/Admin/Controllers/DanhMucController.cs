using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
// Adjust the namespace if needed


namespace DoAn.Areas.Admin.Controllers
{

    public class DanhMucController : Controller
    {

        QLGiayDepEntities db = new QLGiayDepEntities();
        // GET: Admin/DanhMuc

        public ActionResult DanhMuc()
        {

            List<DanhMuc> ds = db.DanhMucs.Select(t => t).ToList<DanhMuc>();
            return View(ds);
        }
        public ActionResult ThemDM()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemDM(DanhMuc p)
        {
            if (ModelState.IsValid)
            {
                p.NgayTao = DateTime.Now;
                p.NgaySua = DateTime.Now;
                db.DanhMucs.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(p);
        }
        public ActionResult XoaDM(int id)
        {
            var item = db.DanhMucs.Find(id);
            if (item != null)
            {
                db.DanhMucs.Remove(item); // Xóa đối tượng
                db.SaveChanges(); // Lưu thay đổi
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách
            }

            // Nếu không tìm thấy, có thể trả về thông báo lỗi
            return HttpNotFound("Danh mục không tồn tại.");
        }

        public ActionResult SuaDM(int id)
        {
            var byid = db.DanhMucs.Find(id);
            return View(byid);
        }
        [HttpPost]
        public ActionResult SuaDM(DanhMuc p)
        {
            if (!ModelState.IsValid)
            {
                return View(p); // Trả về view với lỗi nếu model không hợp lệ
            }

            // Tìm danh mục trong cơ sở dữ liệu
            var a = db.DanhMucs.Find(p.Id);
            if (a != null)
            {
                // Cập nhật các thuộc tính cần thiết
                a.TieuDe = p.TieuDe;
                a.ViTri = p.ViTri;
              
                a.MoTa = p.MoTa;
                a.NgaySua = DateTime.Now; // Cập nhật ngày sửa

                // Lưu thay đổi
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
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