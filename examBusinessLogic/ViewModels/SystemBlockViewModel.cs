using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace examBusinessLogic.ViewModels
{
    public class SystemBlockViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Марка")]
        public string Brand { get; set; }
        [DisplayName("Тип блока")]
        public string BlockType { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
    }
}
