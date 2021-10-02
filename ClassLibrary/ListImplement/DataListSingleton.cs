using ListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Class> Classes { get; set; }
        public List<DopClass> DopClasses { get; set; }

        private DataListSingleton()
        {
            Classes = new List<Class>();
            DopClasses = new List<DopClass>();
        }


        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
