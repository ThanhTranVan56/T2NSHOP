using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models.EF
{
    [Table("tb_ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string IdUser { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsPay { get; set; }
        public bool IsDelete { get; set; }
        public string Code { get; set; }
    }
}