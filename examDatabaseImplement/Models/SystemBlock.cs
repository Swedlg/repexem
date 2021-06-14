using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace examDatabaseImplement.Models
{
    public class SystemBlock
    {
        public int? Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string BlockType { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [ForeignKey("SystemBlockId")]
        public virtual List<Component> Components { get; set; }
    }
}
