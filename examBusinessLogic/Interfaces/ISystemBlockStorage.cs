using examBusinessLogic.BindingModels;
using examBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace examBusinessLogic.Interfaces
{
    public interface ISystemBlockStorage
    {
        List<SystemBlockViewModel> GetFullList();
        List<SystemBlockViewModel> GetFilteredList(SystemBlockBindingModel model);
        SystemBlockViewModel GetElement(SystemBlockBindingModel model);
        void Insert(SystemBlockBindingModel model);
        void Update(SystemBlockBindingModel model);
        void Delete(SystemBlockBindingModel model);
    }
}
