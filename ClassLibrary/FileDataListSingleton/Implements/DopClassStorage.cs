using LibraryClass.BindingModels;
using LibraryClass.Interfaces;
using LibraryClass.ViewModels;
using FileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileImplement.Implements
{
    public class DopClassStorage : IDopClassStorage
    {

        private readonly FileDataListSingleton source;

        public DopClassStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }


        public List<DopClassViewModel> GetFullList()
        {
            return source.DopClasses.Select(CreateModel).ToList();
        }



        public List<DopClassViewModel> GetFilteredList(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.DopClasses
                .Where(rec => rec.DopName.Contains(model.DopName))
                .Select(CreateModel)
                .ToList();
        }

        public DopClassViewModel GetElement(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var dopClass = source.DopClasses
                .FirstOrDefault(rec => rec.DopName == model.DopName || rec.Id == model.Id);
            return dopClass != null ? CreateModel(dopClass) : null;
        }

        public void Insert(DopClassBindingModel model)
        {
            int maxId = source.DopClasses.Count > 0 ? source.DopClasses.Max(rec => rec.Id) : 0;
            var element = new DopClass { Id = maxId + 1 };
            source.DopClasses.Add(CreateModel(model, element));
        }


        public void Update(DopClassBindingModel model)
        {
            var element = source.DopClasses.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }


        public void Delete(DopClassBindingModel model)
        {
            DopClass element = source.DopClasses.FirstOrDefault(rec => rec.Id == model.Id);

            if (element != null)
            {
                source.DopClasses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private DopClass CreateModel(DopClassBindingModel model, DopClass dopClass)
        {
            dopClass.DopName = model.DopName;
            dopClass.DopDate = model.DopDate;
            dopClass.DopField = model.DopField;
            dopClass.DopField2 = model.DopField2;
            dopClass.ClassId = model.ClassId;
            return dopClass;
        }

        private DopClassViewModel CreateModel(DopClass dopClass)
        {
            return new DopClassViewModel
            {
                Id = dopClass.Id,
                DopName = dopClass.DopName,
                DopDate = dopClass.DopDate,
                DopField = dopClass.DopField,
                DopField2 = dopClass.DopField2,
                ClassId = dopClass.ClassId
            };
        }
    }
}
