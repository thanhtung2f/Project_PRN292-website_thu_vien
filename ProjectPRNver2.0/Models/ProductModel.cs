using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.Models
{
    public class ProductModel
    {
        [Key]
        public int BookId { get; set; }

        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string BookName { get; set; }

        [Display(Name = "Mô tả sách")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string BookDescription { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public decimal? BookPrice { get; set; }

        [Display(Name = "Số lượng sách")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int? BookQuantity { get; set; }

        [Display(Name = "Tên tác giả")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int AuthorId { get; set; }

        [Display(Name = "Chủ đề")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int CategoryId { get; set; }

        [Display(Name = "Số trang")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public int? NumberOfPage { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name = "Ngày phát hành")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string imgPath { get; set; }

    }
}