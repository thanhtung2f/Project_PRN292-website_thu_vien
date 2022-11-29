using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { set; get; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        [Required(ErrorMessage = "Yêu cầu nhập lại mật khẩu")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên")]
        public string FullName { set; get; }

        [Display(Name = "Số điện thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại không đúng")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string Phone { set; get; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [EmailAddress(ErrorMessage = "Email không đúng")]
        public string Email { set; get; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string Address { set; get; }

    }
}