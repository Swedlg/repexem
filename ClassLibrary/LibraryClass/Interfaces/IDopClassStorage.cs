using System;
using System.Collections.Generic;
using System.Text;

using LibraryClass.BindingModels;
using LibraryClass.ViewModels;

namespace LibraryClass.Interfaces
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
