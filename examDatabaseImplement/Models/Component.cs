using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace examDatabaseImplement.Models
{
    public class Component
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public string Firm { get; set; }
        public int? SystemBlockId { get; set; }
        public virtual SystemBlock SystemBlock { get; set; }
    }
}
