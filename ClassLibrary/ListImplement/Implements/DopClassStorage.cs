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
    public class DopClassStorage : IDopClassStorage
    {

        private readonly DataListSingleton source;

        public DopClassStorage()
        {
            source = DataListSingleton.GetInstance();
        }


        public List<DopClassViewModel> GetFullList()
        {
            //List<ClassViewModel> result = new List<ClassViewModel>();


            List<DopClassViewModel> result = source.DopClasses.Select(rec => CreateModel(rec)).ToList();


            /*
            foreach (var _class in source.Classes)
            {
                result.Add(CreateModel(_class));
            }
            */

            return result;
        }

        public List<DopClassViewModel> GetFilteredList(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            List<DopClassViewModel> result = source.DopClasses
               .Where(rec => rec.DopName == model.DopName)
               .Select(rec => CreateModel(rec)).ToList();

            /*
            List<DopClassViewModel> result = new List<DopClassViewModel>();

            foreach (var dopClass in source.DopClasses)
            {
                if (dopClass.DopName.Contains(model.DopName))
                {
                    result.Add(CreateModel(dopClass));
                }
            }
            */


            return result.Count == 0 ? null : result;
        }

        public DopClassViewModel GetElement(DopClassBindingModel model)
        {
            if (model == null)
            {
                return null;
            }


            DopClassViewModel result = source.DopClasses
               .Where(rec => rec.Id == model.Id || rec.DopName == model.DopName)
               .Select(rec => CreateModel(rec)).FirstOrDefault();


            /*
            foreach (var dopClass in source.DopClasses)
            {
                if (dopClass.Id == model.Id || dopClass.DopName == model.DopName)
                {
                    return CreateModel(dopClass);
                }
            }
            */

            return result;
        }

        public void Insert(DopClassBindingModel model)
        {
            DopClass tempDopClass = new DopClass { Id = 1 };

            if (source.DopClasses.Count != 0)
            {
                tempDopClass.Id = source.DopClasses.Where(rec => rec.Id >= tempDopClass.Id).Select(rec => rec.Id + 1).LastOrDefault();
            }

            /*
            foreach (var dopClass in source.DopClasses)
            {
                if (dopClass.Id >= tempDopClass.Id)
                {
                    tempDopClass.Id = dopClass.Id + 1;
                }
            }
            */

            source.DopClasses.Add(CreateModel(model, tempDopClass));
        }

        public void Update(DopClassBindingModel model)
        {
            DopClass tempDopClass = source.DopClasses.Where(rec => rec.Id == model.Id).FirstOrDefault();

            /*
            foreach (var dopClass in source.DopClasses)
            {
                if (dopClass.Id == model.Id)
                {
                    tempDopClass = dopClass;
                }
            }
            */

            if (tempDopClass == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tempDopClass);
        }


        public void Delete(DopClassBindingModel model)
        {

            source.DopClasses.RemoveAll(c => c.Id == model.Id);

            /*
            for (int i = 0; i < source.DopClasses.Count; ++i)
            {
                if (source.DopClasses[i].Id == model.Id.Value)
                {
                    source.DopClasses.RemoveAt(i);
                    return;
                }
            }
            */
            throw new Exception("Элемент не найден");
        }


        private DopClass CreateModel(DopClassBindingModel model, DopClass dopClass)
        {
            dopClass.DopName = model.DopName;
            dopClass.ClassId = model.ClassId;
            dopClass.DopDate = model.DopDate;
            dopClass.DopField = model.DopField;
            dopClass.DopField2 = model.DopField2;

            return dopClass;
        }

        private DopClassViewModel CreateModel(DopClass dopClass)
        {
            return new DopClassViewModel
            {
                Id = dopClass.Id,
                DopName = dopClass.DopName,
                ClassId = dopClass.ClassId,
                DopDate = dopClass.DopDate,
                DopField = dopClass.DopField,
                DopField2 = dopClass.DopField2
            };
        }
    }
}
