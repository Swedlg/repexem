using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


using LibraryClass.BusinessLogics;
using LibraryClass.Interfaces;

using LibraryClass.Helpers;

//using ListImplement.Implements;
//using DatabaseImplement.Implements;

using FileImplement.Implements;



namespace WindowsFormsApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormClass>());
        }



        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClassStorage, ClassStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDopClassStorage, DopClassStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ClassLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DopClassLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }


















        public static void ConfigGrid<T>(List<T> data, DataGridView grid)
        {
            var type = typeof(T);
            var config = new List<string>();
            grid.Columns.Clear();
            foreach (var prop in type.GetProperties())
            {
                // получаем список атрибутов
                var attributes =
                prop.GetCustomAttributes(typeof(ClassAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    foreach (var attr in attributes)
                    {
                        // ищем нужный нам атрибут
                        if (attr is ClassAttribute columnAttr)
                        {
                            config.Add(prop.Name);
                            var column = new DataGridViewTextBoxColumn
                            {
                                Name = prop.Name,
                                ReadOnly = true,
                                HeaderText = columnAttr.Title,
                                Visible = columnAttr.Visible,
                                Width = columnAttr.Width
                            };                    
                            grid.Columns.Add(column);
                        }
                    }
                }
            }
            // добавляем строки
            foreach (var elem in data)
            {
                List<object> objs = new List<object>();
                foreach (var conf in config)
                {
                    var value =
                    elem.GetType().GetProperty(conf).GetValue(elem);
                    objs.Add(value);
                }
                grid.Rows.Add(objs.ToArray());
            }
        }



    }
}
