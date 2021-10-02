using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ClassLibrary.ViewModels
{
    public class ClassViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Категория")]
        public string Category { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }
    }
}
