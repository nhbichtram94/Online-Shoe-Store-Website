using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class GioHangController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();

        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {
            var lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int ms, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            var sanPham = db.SanPhams.Find(ms);
            if (sanPham != null)
            {
                var sp = lstGioHang.Find(p => p.IMaSP == ms);
                if (sp == null)
                {
                    sp = new GioHang(ms);
                    lstGioHang.Add(sp);
                }
                else
                {
                    sp.ISoLuong++;
                }
            }

            return Redirect(strURL ?? "DefaultUrl");
        }

        int TongSoLuong()
        {
            return LayGioHang().Sum(p => p.ISoLuong);
        }

        public double TongThanhTien(List<GioHang> lstGioHang, List<string> selectedItems)
        {
            if (selectedItems == null || !selectedItems.Any())
            {
                return 0;
            }

            return lstGioHang
                .Where(g => selectedItems.Contains(g.IMaSP.ToString()))
                .Sum(g => g.dThanhTien);
        }

        public ActionResult GioHang()
        {
            var lstGioHang = LayGioHang();
            if (lstGioHang == null || !lstGioHang.Any())
            {
                return RedirectToAction("SanPham", "Home");
            }

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = lstGioHang.Sum(p => p.dThanhTien);

            return View(lstGioHang);
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            var lstGioHang = LayGioHang();
            var sp = lstGioHang.SingleOrDefault(s => s.IMaSP == MaSP);
            if (sp != null)
            {
                lstGioHang.Remove(sp);
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang_ALL()
        {
            LayGioHang().Clear();
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int MaSP, int SoLuongMoi)
        {
            var lstGioHang = LayGioHang();
            var sp = lstGioHang.SingleOrDefault(s => s.IMaSP == MaSP);

            if (sp != null)
            {
                sp.ISoLuong = SoLuongMoi;
            }

            ViewBag.TongSoLuong = lstGioHang.Sum(p => p.ISoLuong);
            ViewBag.TongThanhTien = lstGioHang.Sum(p => p.dThanhTien);

            return RedirectToAction("GioHang");
        }
        public async Task<ActionResult> Checkout()
        {
            List<GioHang> lstGioHang = LayGioHang();

            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            if (Session["MaKhachHang"] == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            // Khởi tạo đơn hàng mới
            DonHang dh = new DonHang
            {
                NgayDat = DateTime.Now,
                MaKH = int.Parse(Session["MaKhachHang"].ToString())
            };

            // Thêm đơn hàng vào DbContext
            db.DonHangs.Add(dh);

            // Thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                var chiTiet = new ChiTietDonHang
                {
                    ID = item.IMaSP,
                    SoLuong = item.ISoLuong,
                    Gia = decimal.Parse(item.DGia.ToString())
                };
                dh.ChiTietDonHangs.Add(chiTiet);
            }

            db.SaveChanges();
            // Xóa giỏ hàng sau khi thanh toán
            lstGioHang.Clear();

            // Chuyển hướng đến trang danh sách đơn hàng
            return RedirectToAction("DanhSachDonHang", "DonHang");
        }
    }
}


    
