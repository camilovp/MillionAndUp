using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Property
    {
        [Key]
        public Guid IdProperty { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public int Price { get; set; }
        [StringLength(50)]
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Guid IdOwner { get; set; }

        [ForeignKey(nameof(IdOwner))]
        public Owner Owner { get; set; }
        public ICollection<PropertyImage> PropertyImage { get; set; }
        public ICollection<PropertyTrace> PropertyTrace { get; set; }
    }
}
