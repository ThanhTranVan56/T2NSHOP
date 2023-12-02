using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2NSHOP.Models.EF
{
    [Table("tb_ProfileCustomer")]
    public class ProfileCustomer
    {
        public ProfileCustomer()
        {
            this.AddressCustomers = new HashSet<AddressCustomer>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TypePayment { get; set; }
        public int TypePaymentVN { get; set; }
        public virtual ICollection<AddressCustomer> AddressCustomers { get; set; }
    }
}