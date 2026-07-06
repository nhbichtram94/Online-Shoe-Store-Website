using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class GioHang
    {
        QLGiayDepEntities db = new QLGiayDepEntities ();

        int _iMaSP;

        public int IMaSP
        {
            get { return _iMaSP; }
            set { _iMaSP = value; }
        }
        string _sTieuDe;

        public string STieuDes
        {
            get { return _sTieuDe; }
            set { _sTieuDe = value; }
        }
        string _sDuongDanHinhAnh;

        public string SDuongDanHinhAnh
        {
            get { return _sDuongDanHinhAnh; }
            set { _sDuongDanHinhAnh = value; }
        }
        double _dGia;

        public double DGia
        {
            get { return _dGia; }
            set { _dGia = value; }
        }
        int _iSoLuong;

        public int ISoLuong
        {
            get { return _iSoLuong; }
            set { _iSoLuong = value; }
        }
        public double dThanhTien
        {
            get
            {
                return ISoLuong * DGia;
            }
        }

        // Constructor cần ID sản phẩm
        public GioHang(int id)
        {
            IMaSP = id;
            SanPham sp = db.SanPhams.Single(s => s.Id == IMaSP);
            _sTieuDe = sp.TieuDe;
            _sDuongDanHinhAnh = sp.DuongDanHinhAnh;
            _dGia = (double)sp.Gia; // Cast thành double nếu cần
            _iSoLuong = 1; // Mặc định số lượng là 1
        }
    }
}
