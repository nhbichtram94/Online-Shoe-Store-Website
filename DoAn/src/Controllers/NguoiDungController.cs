using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace DoAn.Controllers
{
    public class NguoiDungController : Controller
    {   //
        // GET: /NguoiDung/
       QLGiayDepEntities db  = new QLGiayDepEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                // Thêm khách hàng mới vào cơ sở dữ liệu
                db.KhachHangs.Add(kh);
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                // Thông báo thành công
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";

                // Chuyển hướng đến trang Đăng Nhập
                return RedirectToAction("DangNhap", "DangNhap");
            }

            // Nếu ModelState không hợp lệ, quay lại View để sửa lỗi
            return View(kh);
        }
    }
}