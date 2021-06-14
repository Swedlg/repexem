using examListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace examListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<SystemBlock> SystemBlocks { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            SystemBlocks = new List<SystemBlock>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null) instance = new DataListSingleton();

            return instance;
        }
    }
}
