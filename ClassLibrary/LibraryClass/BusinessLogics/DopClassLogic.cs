using System;
using System.Collections.Generic;
using System.Text;

using LibraryClass.BindingModels;
using LibraryClass.ViewModels;
using LibraryClass.Interfaces;

namespace LibraryClass.BusinessLogics
{
    public class DopClassLogic
    {
        private readonly IDopClassStorage dopClassStorage;

        public DopClassLogic(IDopClassStorage dopClassStorage)
        {
            this.dopClassStorage = dopClassStorage;
        }

        public List<DopClassViewModel> Read(DopClassBindingModel model)
        {
            if (model == null)
            {
                return dopClassStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DopClassViewModel> { dopClassStorage.GetElement(model) };
            }
            return dopClassStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DopClassBindingModel model)
        {
            var element = dopClassStorage.GetElement(new DopClassBindingModel { DopName = model.DopName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                dopClassStorage.Update(model);
            }
            else
            {
                dopClassStorage.Insert(model);
            }
        }

        public void Delete(DopClassBindingModel model)
        {
            var element = dopClassStorage.GetElement(new DopClassBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            dopClassStorage.Delete(model);
        }
    }
}
