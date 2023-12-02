using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2NSHOP.Models.EF
{
    [Table("tb_Review")]
    public class ReviewProduct
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime Createdate { get; set; }
        public string Avata { get; set; }
        public bool IsActive { get; set; }
        public virtual Product Product { get; set; }

    }
}