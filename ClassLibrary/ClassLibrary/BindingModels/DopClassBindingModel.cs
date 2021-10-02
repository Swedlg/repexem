using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.BindingModels
{
    public class DopClassBindingModel
    {
        public int? Id { get; set; }

        public int ClassId { get; set; }

        public string DopName { get; set; }

        public string DopField { get; set; }

        public int DopField2 { get; set; }

        public DateTime DopDate { get; set; }

    }
}
