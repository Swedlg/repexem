using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ViewModels
{
    public class DopClassViewModel
    {

        public int? Id { get; set; }

        public int ClassId { get; set; }

        [DisplayName("Доп Название")]
        public string DopName { get; set; }

        [DisplayName("Доп Поле 1")]
        public string DopField { get; set; }

        [DisplayName("Доп Поле 2")]
        public int DopField2 { get; set; }

        [DisplayName("Доп дата")]
        public DateTime DopDate { get; set; }
    }
}
