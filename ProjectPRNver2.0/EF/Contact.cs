namespace ProjectPRNver2._0.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContactId { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [StringLength(30)]
        public string Address { get; set; }
    }
}
