using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T2NSHOP.Models.EF
{
    [Table("tb_Posts")]
    public class Posts : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Alias { get; set; }
        public string Decription { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        public int ViewCount { get; set; }
        public int CategoryID { get; set; }
        public string SeoTitel { get; set; }
        [StringLength(500)]
        public string SeoDecription { get; set; }
        [StringLength(200)]
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Categogy Categogy { get; set; }
    }
}