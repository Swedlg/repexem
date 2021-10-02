using LibraryClass.BindingModels;
using LibraryClass.Interfaces;
using LibraryClass.ViewModels;
using DatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DatabaseImplement.Implements
{
    public class ClassStorage : IClassStorage
    {
        public List<ClassViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Classes
                    .Select(rec => new ClassViewModel
                    {
                        Id = rec.Id,
                        Name = rec.Name,
                        Category = rec.Category,
                        Date = rec.Date
                    })
                    .ToList();
            }
        }

        public List<ClassViewModel> GetFilteredList(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.Classes
                    .Where(rec => rec.Date >= model.DateFrom && rec.Date <= model.DateTo)
                    .Select(rec => new ClassViewModel
                    {
                        Id = rec.Id,
                        Name = rec.Name,
                        Category = rec.Category,
                        Date = rec.Date 
                    })
                .ToList();
            }
        }

        public ClassViewModel GetElement(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var _class = context.Classes.FirstOrDefault(rec => rec.Id == model.Id);

                return _class != null ?
                new ClassViewModel
                {
                    Id = _class.Id,
                    Name = _class.Name,
                    Category = _class.Category,
                    Date = _class.Date
                } : null;
            }
        }

        public void Insert(ClassBindingModel model)
        {
            using (var context = new Database())
            {
                context.Classes.Add(CreateModel(model, new Class()));
                context.SaveChanges();
            }
        }

        public void Update(ClassBindingModel model)
        {
            using (var context = new Database())
            {
                var element = context.Classes.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(ClassBindingModel model)
        {
            using (var context = new Database())
            {
                Class element = context.Classes.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Classes.Remove(element);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Class CreateModel(ClassBindingModel model, Class _class)
        {
            //_class.Id = (int)model.Id;
            _class.Name = model.Name;
            _class.Category = model.Category;
            _class.Date = model.Date;
            return _class;
        }
    }
}
