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
    public class DopClassStorage : IDopClassStorage
    {    
        public List<DopClassViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.DopClasses
                    .Select(rec => new DopClassViewModel
                    {
                        Id = rec.Id,
                        DopName = rec.DopName,
                        DopDate = rec.DopDate,
                        DopField = rec.DopField,                   
                        DopField2 = rec.DopField2,
                        ClassId = rec.ClassId
                    })
                    .ToList();
            }
        }

        public List<DopClassViewModel> GetFilteredList(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.DopClasses
                    .Where(rec => rec.DopName.Equals(model.DopName))
                    .Select(rec => new DopClassViewModel
                    {
                        Id = rec.Id,
                        DopName = rec.DopName,
                        DopDate = rec.DopDate,
                        DopField = rec.DopField,
                        DopField2 = rec.DopField2,
                        ClassId = rec.ClassId
                    })
                .ToList();
            }
        }

        public DopClassViewModel GetElement(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var dopClass = context.DopClasses.FirstOrDefault(rec => rec.Id == model.Id);

                return dopClass != null ?
                new DopClassViewModel
                {
                    Id = dopClass.Id,
                    DopName = dopClass.DopName,
                    DopDate = dopClass.DopDate,
                    DopField = dopClass.DopField,
                    DopField2 = dopClass.DopField2,
                    ClassId = dopClass.ClassId
                } : null;
            }
        }

        public void Insert(DopClassBindingModel model)
        {
            using (var context = new Database())
            {
                context.DopClasses.Add(CreateModel(model, new DopClass()));
                context.SaveChanges();
            }
        }

        public void Update(DopClassBindingModel model)
        {
            using (var context = new Database())
            {
                var element = context.DopClasses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(DopClassBindingModel model)
        {
            using (var context = new Database())
            {
                DopClass element = context.DopClasses.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.DopClasses.Remove(element);

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private DopClass CreateModel(DopClassBindingModel model, DopClass dopClass)
        {
            //dopClass.Id = (int)model.Id;
            dopClass.DopName = model.DopName;
            dopClass.DopDate = model.DopDate;
            dopClass.DopField = model.DopField;
            dopClass.DopField2 = model.DopField2;
            dopClass.ClassId = model.ClassId;
            return dopClass;
        }
    }
}
