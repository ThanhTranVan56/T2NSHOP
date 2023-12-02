using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace T2NSHOP.Models.EF
{
    [Table("tb_News")]
    public class News : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không để trống tiêu đề tin")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Decription { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public string SeoTitel { get; set; }
        public string SeoDecription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Categogy Categogy { get; set; }

    }
}