using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DatabaseImplement.Models
{
    public class DopClass
    {
        public int? Id { get; set; }

        public int ClassId { get; set; }

        [Required]
        public string DopName { get; set; }

        [Required]
        public string DopField { get; set; }

        [Required]
        public int DopField2 { get; set; }

        [Required]
        public DateTime DopDate { get; set; }

        public virtual Class Class { get; set; }
    }
}
