using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class DonHangController : Controller
    {
        QLGiayDepEntities qlbh = new QLGiayDepEntities();
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachDonHang()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            int maKH = int.Parse(Session["MaKhachHang"].ToString()); 

            var danhSachDonHang = qlbh.DonHangs
                           .Where(dh => dh.MaKH == maKH)
                           .OrderByDescending(dh => dh.NgayDat)  
                           .ThenByDescending(dh => dh.MaDonHang) 
                           .ToList();

            return View(danhSachDonHang);
        }
        public ActionResult ChiTietDonHang(int id)
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            DonHang donHang = qlbh.DonHangs
                                  .Where(dh => dh.MaDonHang == id)
                                  .FirstOrDefault();

            if (donHang == null)
            {
                return HttpNotFound();
            }

            var chiTietDonHang = donHang.ChiTietDonHangs.ToList();

            return View(chiTietDonHang);
        }

    }
}