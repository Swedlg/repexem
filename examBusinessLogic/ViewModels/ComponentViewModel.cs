using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace examBusinessLogic.ViewModels
{
    public class ComponentViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Цена")]
        public double Price { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Фирма")]
        public string Firm { get; set; }
        public int? SystemBlockId { get; set; }
    }
}
