using System;
using System.Collections.Generic;
using System.Text;

using LibraryClass.BindingModels;
using LibraryClass.ViewModels;

namespace LibraryClass.Interfaces
{
    public interface IClassStorage
    {
        List<ClassViewModel> GetFullList();

        List<ClassViewModel> GetFilteredList(ClassBindingModel model);

        ClassViewModel GetElement(ClassBindingModel model);

        void Insert(ClassBindingModel model);

        void Update(ClassBindingModel model);

        void Delete(ClassBindingModel model);
    }
}
