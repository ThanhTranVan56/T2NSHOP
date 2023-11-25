using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models.EF
{
    [Table("tb_Adv")]
    public class Adv : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Titel { get; set; }
        [StringLength(500)]
        public string Decription { get; set; }
        [StringLength(500)]
        public string Image { get; set; }
        public int Type { get; set; }
        [StringLength(500)]
        public string Link { get; set; }
    }
}