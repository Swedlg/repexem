using examBusinessLogic.BindingModels;
using examBusinessLogic.Interfaces;
using examBusinessLogic.ViewModels;
using examListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace examListImplement.Implements
{
    public class ComponentStorage : IComponentStorage
    {
        private readonly DataListSingleton source;

        public ComponentStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(ComponentBindingModel model)
        {
            var element = source.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Components.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ComponentViewModel GetElement(ComponentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var component = source.Components.FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return component != null ? CreateModel(component) : null;
        }

        public List<ComponentViewModel> GetFilteredList(ComponentBindingModel model) =>
            model == null ? null : source.Components.Where(rec => rec.Name.Contains(model.Name)).Select(CreateModel).ToList();

        public List<ComponentViewModel> GetFullList() => source.Components.Select(CreateModel).ToList();

        public void Insert(ComponentBindingModel model)
        {
            int maxId = source.Components.Count > 0 ? (int)source.Components.Max(rec => rec.Id) : 0;
            var element = new Component { Id = maxId + 1 };
            source.Components.Add(CreateModel(model, element));
        }

        public void Update(ComponentBindingModel model)
        {
            var element = source.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private Component CreateModel(ComponentBindingModel model, Component compoment)
        {
            compoment.Name = model.Name;
            compoment.SystemBlockId = model.SystemBlockId;
            compoment.DateCreate = model.DateCreate;
            compoment.Price = model.Price;
            compoment.Firm = model.Firm;
            return compoment;
        }

        private ComponentViewModel CreateModel(Component component)
        {
            return new ComponentViewModel
            {
                Id = (int)component.Id,
                Name = component.Name,
                DateCreate = component.DateCreate,
                Firm = component.Firm,
                SystemBlockId = component.SystemBlockId
            };
        }
    }
}
