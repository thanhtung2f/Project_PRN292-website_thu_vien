namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? BookId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
