using System;
using System.Collections.Generic;
using System.Text;

namespace examListImplement.Models
{
    public class Component
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateCreate { get; set; }
        public string Firm { get; set; }
        public int? SystemBlockId { get; set; }
    }
}
