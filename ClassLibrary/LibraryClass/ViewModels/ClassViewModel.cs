using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LibraryClass.Helpers;

namespace LibraryClass.ViewModels
{
    public class ClassViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название")]
        [Class(title: "Название", width: 150)]

        public string Name { get; set; }

        [DisplayName("Категория")]
        [Class(title: "Категория", width: 70)]

        public string Category { get; set; }

        [DisplayName("Дата")]
        [Class(title: "Дата", width: 150)]

        public DateTime Date { get; set; }
    }
}
