namespace ProjectPRNver2._0.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new HashSet<Order>();
            WishLists = new HashSet<WishList>();
        }

        public int UserId { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        public string PassWord { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string UserPhone { get; set; }

        [StringLength(50)]
        public string UserMail { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public int Roleid { get; set; }

        public Guid? ActiveCode { get; set; }

        [StringLength(100)]
        public string ResetPasswordCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
