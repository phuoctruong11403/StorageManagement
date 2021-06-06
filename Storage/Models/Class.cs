using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string ClassName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
