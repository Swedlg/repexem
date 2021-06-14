using System;
using System.Collections.Generic;
using System.Text;

namespace examBusinessLogic.ViewModels
{
    public class ReportViewModel
    {
        public string Brand { get; set; }
        public string ComponentName { get; set; }
        public string Firm { get; set; }
        public DateTime ComponentDateCreate { get; set; }
        public DateTime SystemBlockDateCreate { get; set; }
    }
}
