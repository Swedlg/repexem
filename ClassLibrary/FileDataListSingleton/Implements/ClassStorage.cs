using LibraryClass.BindingModels;
using LibraryClass.Interfaces;
using LibraryClass.ViewModels;
using FileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileImplement.Implements
{
    public class ClassStorage : IClassStorage
    {

        private readonly FileDataListSingleton source;

        public ClassStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }


        public List<ClassViewModel> GetFullList()
        {
            return source.Classes.Select(CreateModel).ToList();
        }

        

        public List<ClassViewModel> GetFilteredList(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Classes
                .Where(rec => rec.Date >= model.DateFrom && rec.Date <= model.DateTo)
                .Select(CreateModel)
                .ToList();
        }

        public ClassViewModel GetElement(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var _class = source.Classes
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return _class != null ? CreateModel(_class) : null;
        }

        public void Insert(ClassBindingModel model)
        {
            int maxId = source.Classes.Count > 0 ? source.Classes.Max(rec => rec.Id) : 0;
            var element = new Class { Id = maxId + 1 };
            source.Classes.Add(CreateModel(model, element));
        }


        public void Update(ClassBindingModel model)
        {
            var element = source.Classes.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }


        public void Delete(ClassBindingModel model)
        {
            Class element = source.Classes.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                source.Classes.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Class CreateModel(ClassBindingModel model, Class _class)
        {
            _class.Name = model.Name;
            _class.Date = model.Date;
            _class.Category = model.Category;
            return _class;
        }

        private ClassViewModel CreateModel(Class _class)
        {
            return new ClassViewModel
            {
                Id = _class.Id,
                Name = _class.Name,
                Date = _class.Date,
                Category = _class.Category
            };
        }
    }
}
