using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        QLGiayDepEntities db = new QLGiayDepEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(Account model)
        {
            if (ModelState.IsValid)
            {
                // Thêm logic kiểm tra tên đăng nhập và mật khẩu ở đây
                // Nếu đăng nhập thành công:
                return RedirectToAction("Index", "Home");
            }
            return View(model); // Trả lại model để hiển thị lỗi
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(Account model)
        {
            if (ModelState.IsValid)
            {
                // Thêm logic lưu thông tin người dùng ở đây
                // Sau khi đăng ký, chuyển hướng về trang đăng nhập:
                return RedirectToAction("Login");
            }
            return View(model); // Trả lại model để hiển thị lỗi
        }
    }
}