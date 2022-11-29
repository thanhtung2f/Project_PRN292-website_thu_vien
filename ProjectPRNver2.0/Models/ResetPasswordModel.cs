using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="Nhập mật khẩu", AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Mật khẩu mới và xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}