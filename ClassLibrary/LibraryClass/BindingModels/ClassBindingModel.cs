using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryClass.BindingModels
{
    public class ClassBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
