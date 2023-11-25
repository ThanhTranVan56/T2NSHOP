using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models.EF
{
    [Table("tb_Categogy")]
    public class Categogy : CommonAbstract
    {
        public Categogy()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được bỏ trống")]
        [StringLength(150)]
        public string Titel { get; set; }
        public string Alias { get; set; }
        //[StringLength(150)]
        //public string TypeCode { get; set; }
        //public string Link { get; set; }
        public string Decription { get; set; }
        [StringLength(150)]
        public string SeoTitel { get; set; }
        [StringLength(250)]
        public string SeoDecription { get; set; }
        public string SeoKeywords { get; set; }
        public int Possition { get; set; }
        public bool IsActive { get; set; }

        public ICollection<News> News { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}