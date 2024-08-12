using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultilEcommer.Models.Contact
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phải nhập {0}")]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ Email")]
        [Display(Name = "Địa chỉ Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Display(Name = "Ngày gửi")]
        public DateTime DateSend { get; set; }

        [Display(Name = "Nội dung")]
        public string? Message { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Phải là số điện thoại")]
        public string? Phone { get; set; }
    }
}