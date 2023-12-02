using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2NSHOP.Models.EF
{
    [Table("tb_AddressCustomer")]
    public class AddressCustomer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public virtual ProfileCustomer Profile { get; set; }
    }
}