﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.BindingModels
{
    public class ClassBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }
    }
}
