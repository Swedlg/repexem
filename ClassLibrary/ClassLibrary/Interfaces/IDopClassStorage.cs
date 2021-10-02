using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.ViewModels;
using ClassLibrary.BindingModels;

namespace ClassLibrary.Interfaces
{
    public interface IDopClassStorage
    {

        List<DopClassViewModel> GetFullList();

        List<DopClassViewModel> GetFilteredList(DopClassBindingModel model);

        DopClassViewModel GetElement(DopClassBindingModel model);

        void Insert(DopClassBindingModel model);

        void Update(DopClassBindingModel model);

        void Delete(DopClassBindingModel model);
    }
}
