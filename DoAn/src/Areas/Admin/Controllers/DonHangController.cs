using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang

        QLGiayDepEntities qlbh = new QLGiayDepEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DonHang()
        {
            // Kiểm tra nếu admin đã đăng nhập (tuỳ chọn, có thể bỏ qua nếu không cần kiểm tra)
            //if (Session["Admin"] == null)
            //{
            //    return RedirectToAction("DangNhap", "Admin");
            //}

            // Lấy toàn bộ đơn hàng, bao gồm thông tin giao hàng
            var db = qlbh.DonHangs
                                     .OrderByDescending(dh => dh.NgayDat) // Sắp xếp theo ngày đặt, mới nhất trước
                                     .ToList();

            // Truyền danh sách đơn hàng vào view
            return View(db);
        }
        public ActionResult ChiTietDonHang(int id)
        {
            // Kiểm tra nếu admin đã đăng nhập (tuỳ chọn)
            //if (Session["Admin"] == null)
            //{
            //    return RedirectToAction("DangNhap", "Admin");
            //}

            // Tìm đơn hàng dựa vào ID
            var donHang = qlbh.DonHangs.FirstOrDefault(dh => dh.MaDonHang == id);
            if (donHang == null)
            {
                // Nếu không tìm thấy đơn hàng, trả về lỗi 404
                return HttpNotFound("Không tìm thấy đơn hàng.");
            }

            // Lấy danh sách chi tiết đơn hàng
            var chiTietDonHang = donHang.ChiTietDonHangs.ToList();

            // Truyền chi tiết đơn hàng vào view
            return View(chiTietDonHang);
        }


    }
}