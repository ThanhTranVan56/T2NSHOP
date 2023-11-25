﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Titel { get; set; }
        [Required]
        [StringLength(150)]
        public string Alias { get; set; }
        [StringLength(500)]
        public string Decription { get; set; }
        [StringLength(250)]
        public string Icon { get; set; }
        [StringLength(250)]
        public string SeoTitel { get; set; }
        [StringLength(500)]
        public string SeoDecription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}