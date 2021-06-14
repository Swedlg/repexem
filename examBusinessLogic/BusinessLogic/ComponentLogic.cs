using examBusinessLogic.BindingModels;
using examBusinessLogic.Interfaces;
using examBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace examBusinessLogic.BusinessLogic
{
    public class ComponentLogic
    {
        private readonly IComponentStorage _componentStorage;
        public ComponentLogic(IComponentStorage componentStorage)
        {
            _componentStorage = componentStorage;
        }
        public List<ComponentViewModel> Read(ComponentBindingModel model)
        {
            if (model == null)
            {
                return _componentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ComponentViewModel> { _componentStorage.GetElement(model) };
            }
            return _componentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ComponentBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _componentStorage.Update(model);
            }
            else
            {
                _componentStorage.Insert(model);
            }
        }
        public void Delete(ComponentBindingModel model)
        {
            var element = _componentStorage.GetElement(new ComponentBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _componentStorage.Delete(model);
        }
    }
}
