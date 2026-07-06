using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class DangNhapController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(DangNhap kh)
        {
            if (ModelState.IsValid)
            {
          
                // Check if the user exists
                var user = db.KhachHangs.SingleOrDefault(u => u.TAIKHOAN == kh.TaiKhoan && u.MATKHAU == kh.MatKhau); // Adjust according to your model

                if (user != null)
                {
                    // User exists, set a success message
                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    Session["TaiKhoan"] = user.TAIKHOAN;
                    Session["MaKhachHang"] = user.MAKH;
                    return RedirectToAction("GioHang", "GioHang"); // Redirect to a home page or another action
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác.");
                }



            }
            return View(kh);

        }
    }
}