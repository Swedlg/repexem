using LibraryClass.BindingModels;
using LibraryClass.Interfaces;
using LibraryClass.ViewModels;
using ListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListImplement.Implements
{
    public class ClassStorage : IClassStorage
    {

        private readonly DataListSingleton source;



        public ClassStorage()
        {
            source = DataListSingleton.GetInstance();
        }


        public List<ClassViewModel> GetFullList()
        {
            List<ClassViewModel> result = source.Classes.Select(rec => CreateModel(rec)).ToList();

            return result;
        }


        public List<ClassViewModel> GetFilteredList(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
        
            List<ClassViewModel> result = source.Classes
                .Where(rec => rec.Name == model.Name)
                .Select(rec => CreateModel(rec)).ToList();

            /*
            List<ClassViewModel> result = new List<ClassViewModel>();
            foreach (var _class in source.Classes)
            {
                if (_class.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(_class));
                }
            }
            */

            return result.Count == 0 ? null : result;
        }

        public ClassViewModel GetElement(ClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }


            ClassViewModel result = source.Classes
                .Where(rec => rec.Id == model.Id || rec.Name == model.Name)
                .Select(rec => CreateModel(rec)).FirstOrDefault();

            /*
            foreach (var _class in source.Classes)
            {
                if (_class.Id == model.Id || _class.Name == model.Name)
                {
                    return CreateModel(_class);
                }
            }
            */

            return result;
        }

        public void Insert(ClassBindingModel model)
        {
            Class tempClass = new Class { Id = 1 };


            if (source.Classes.Count != 0)
            {
                tempClass.Id = source.Classes.Where(rec => rec.Id >= tempClass.Id).Select(rec => rec.Id + 1).LastOrDefault();
            }

            /*
            foreach (var _class in source.Classes)
            {
                if (_class.Id >= tempClass.Id)
                {
                    tempClass.Id = _class.Id + 1;
                }
            }
            */

            source.Classes.Add(CreateModel(model, tempClass));
        }

        public void Update(ClassBindingModel model)
        {
            Class tempClass = source.Classes.Where(rec => rec.Id == model.Id).FirstOrDefault();

            if (tempClass == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempClass);
        }

        public void Delete(ClassBindingModel model)
        {
            source.Classes.RemoveAll(c => c.Id == model.Id);
        }

        private Class CreateModel(ClassBindingModel model, Class _class)
        {
            _class.Name = model.Name;
            _class.Category = model.Category;
            _class.Date = model.Date;

            return _class;
        }

        private ClassViewModel CreateModel(Class _class)
        {
            return new ClassViewModel
            {
                Id = _class.Id,
                Name = _class.Name,
                Category = _class.Category,
                Date = _class.Date
            };
        }


    }
}
