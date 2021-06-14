using examBusinessLogic.BindingModels;
using examBusinessLogic.Interfaces;
using examBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace examBusinessLogic.BusinessLogic
{
    public class SystemBlockLogic
    {
        private readonly ISystemBlockStorage _printedStorage;
        public SystemBlockLogic(ISystemBlockStorage printedStorage)
        {
            _printedStorage = printedStorage;
        }
        public List<SystemBlockViewModel> Read(SystemBlockBindingModel model)
        {
            if (model == null)
            {
                return _printedStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<SystemBlockViewModel> { _printedStorage.GetElement(model) };
            }
            return _printedStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(SystemBlockBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _printedStorage.Update(model);
            }
            else
            {
                _printedStorage.Insert(model);
            }
        }
        public void Delete(SystemBlockBindingModel model)
        {
            var element = _printedStorage.GetElement(new SystemBlockBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _printedStorage.Delete(model);
        }
    }
}
