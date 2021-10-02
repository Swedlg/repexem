using System;
using System.Collections.Generic;
using System.Text;

using LibraryClass.BindingModels;
using LibraryClass.ViewModels;
using LibraryClass.Interfaces;

namespace LibraryClass.BusinessLogics
{
    public class ClassLogic
    {
        private readonly IClassStorage classStorage;

        public ClassLogic(IClassStorage classStorage)
        {
            this.classStorage = classStorage;
        }

        public List<ClassViewModel> Read(ClassBindingModel model)
        {
            if (model == null)
            {
                return classStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ClassViewModel> { classStorage.GetElement(model) };
            }
            return classStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ClassBindingModel model)
        {
            var element = classStorage.GetElement(new ClassBindingModel { Name = model.Name });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                classStorage.Update(model);
            }
            else
            {
                classStorage.Insert(model);
            }
        }

        public void Delete(ClassBindingModel model)
        {
            var element = classStorage.GetElement(new ClassBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            classStorage.Delete(model);
        }
    }
}
