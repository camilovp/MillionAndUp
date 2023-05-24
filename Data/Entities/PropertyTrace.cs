using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PropertyTrace
    {
        [Key]
        public Guid IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        public int Tax { get; set; }
        public Guid IdProperty { get; set; }

        [ForeignKey(nameof(IdProperty))]
        public Property Property { get; set; }
    }
}
