using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.ViewModels;
using ClassLibrary.BindingModels;

namespace ClassLibrary.Interfaces
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
