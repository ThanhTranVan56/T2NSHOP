using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models.EF
{
    [Table("tb_ProductAttri")]
    public class ProductAttri
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Color { get; set; }
        public string Alias { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal? PriceOverValue { get; set; }
        public virtual Product Product { get; set; }
    }
}