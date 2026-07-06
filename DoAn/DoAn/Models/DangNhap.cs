using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn.Models
{
    public partial class DangNhap
    {
        [Required(ErrorMessage = "Tên tài khoản là bắt buộc.")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
