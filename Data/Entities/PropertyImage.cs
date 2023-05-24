using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PropertyImage
    {
        [Key]
        public Guid IdPropertyImage { get; set; }
        [StringLength(100)]
        public string file { get; set; }
        public bool Enabled { get; set; }
        public Guid IdProperty { get; set; }

        [ForeignKey(nameof(IdProperty))]
        public Property Property { get; set; }
    }
}
