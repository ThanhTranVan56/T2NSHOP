using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2NSHOP.Models
{
    public class CommonAbstract
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifedrDate { get; set; }
        public string ModifierBy { get; set; }
    }
}