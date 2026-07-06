using DoAn.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using System;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
namespace DoAn.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        QLGiayDepEntities db = new QLGiayDepEntities();
        // GET: Admin/SanPham
        public ActionResult DSSanPham()
        {

            List<SanPham> ds = db.SanPhams.Select(t => t).ToList<SanPham>();
            return View(ds);
        }
     

        public ActionResult ThemSP()
        {

            ViewBag.DanhMucSanPham = new SelectList(db.DanhMucSanPhams.ToList(),"id","TieuDe");
            return View();
        }
        [HttpPost]
        public ActionResult ThemSP(SanPham pb, HttpPostedFileBase ImageFile)
        {
            pb.NgayTao = DateTime.Now;
            pb.NgaySua = DateTime.Now;

            // Nếu có lỗi, cập nhật lại danh sách danh mục
            ViewBag.DanhMucSanPham = new SelectList(db.DanhMucSanPhams.ToList(), "Id", "TieuDe", pb.IdDanhMucSanPham);
            ;

            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Hinh"), Path.GetFileName(ImageFile.FileName));
                ImageFile.SaveAs(path);
                pb.DuongDanHinhAnh = "/Hinh/" + Path.GetFileName(ImageFile.FileName);
            }
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(pb);
                db.SaveChanges();
               
                return RedirectToAction("DSSanPham");
            }

            return View(pb);


        }
        public ActionResult SuaSP(int id)
        {
            var byid = db.SanPhams.Find(id);

            ViewBag.DanhMucSanPham = new SelectList(db.DanhMucSanPhams.ToList(), "id", "TieuDe");
            return View(byid);
        }
            
        [HttpPost]
        public ActionResult SuaSP(SanPham p, HttpPostedFileBase ImageFile)
        {

            if (!ModelState.IsValid)
            {
                return View(p); // Trả về view với lỗi nếu model không hợp lệ
            }

            // Tìm sản phẩm trong cơ sở dữ liệu
            var existingProduct = db.SanPhams.Find(p.Id);
            if (existingProduct != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingProduct.TieuDe = p.TieuDe;
                existingProduct.IdDanhMucSanPham = p.IdDanhMucSanPham;
                existingProduct.MoTa = p.MoTa;
                existingProduct.SoLuong = p.SoLuong; // Cập nhật số lượng
                existingProduct.Gia = p.Gia; // Cập nhật giá
                existingProduct.GiaKhuyenMai = p.GiaKhuyenMai; // Cập nhật giá khuyến mãi
                existingProduct.HienThi = p.HienThi; // Cập nhật trạng thái hiển thị
                existingProduct.Hot = p.Hot; // Cập nhật trạng thái hot
                existingProduct.NoiBat = p.NoiBat; // Cập nhật trạng thái nổi bật
                existingProduct.KhuyenMai = p.KhuyenMai; // Cập nhật trạng thái khuyến mãi
                existingProduct.NgaySua = DateTime.Now; // Cập nhật ngày sửa

                // Kiểm tra nếu có hình ảnh mới được tải lên
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Lưu hình ảnh mới
                    string fileName = Path.GetFileName(ImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Hinh"), fileName);
                    ImageFile.SaveAs(path);
                    existingProduct.DuongDanHinhAnh = "/Hinh/" + fileName; // Cập nhật đường dẫn hình ảnh
                }
                // Lưu thay đổi
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("DSSanPham"); // Redirect về danh sách sản phẩm
                }
                catch (DbUpdateException ex)
                {
                    // Thêm thông báo lỗi cho người dùng
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật dữ liệu. Vui lòng kiểm tra và thử lại.");
                    // Log lỗi nếu cần
                    // Logger.LogError(ex); // Ví dụ: sử dụng một logger
                }
            }
            else
            {
                ModelState.AddModelError("", "Sản phẩm không tồn tại.");
            }

            return View(p); // Trả về view với dữ liệu hiện tại nếu có lỗi

        }
    
        public ActionResult XoaSP(int id)
        {
            var item = db.SanPhams.Find(id);
            if (item != null)
            {
                db.SanPhams.Remove(item); // Xóa đối tượng
                db.SaveChanges(); // Lưu thay đổi
                return RedirectToAction("DSSanPham"); // Chuyển hướng về trang danh sách
            }

            // Nếu không tìm thấy, có thể trả về thông báo lỗi
            return HttpNotFound("Sản phẩm không tồn tại.");
        }



    }
}