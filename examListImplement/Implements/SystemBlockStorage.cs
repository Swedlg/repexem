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
    public class SystemBlockStorage : ISystemBlockStorage
    {
        private readonly DataListSingleton source;
        public SystemBlockStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public void Delete(SystemBlockBindingModel model)
        {
            var element = source.SystemBlocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.SystemBlocks.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public SystemBlockViewModel GetElement(SystemBlockBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var computer = source.SystemBlocks.FirstOrDefault(rec => rec.Brand == model.Brand || rec.Id == model.Id);
            return computer != null ? CreateModel(computer) : null;
        }

        public List<SystemBlockViewModel> GetFilteredList(SystemBlockBindingModel model) =>
            model == null ? null : source.SystemBlocks.Where(rec => rec.Brand.Contains(model.Brand)).Select(CreateModel).ToList();

        public List<SystemBlockViewModel> GetFullList()
        {
            return source.SystemBlocks.Select(CreateModel).ToList();
        }

        public void Insert(SystemBlockBindingModel model)
        {
            int maxId = source.SystemBlocks.Count > 0 ? (int)source.SystemBlocks.Max(rec => rec.Id) : 0;
            var element = new SystemBlock { Id = maxId + 1 };
            source.SystemBlocks.Add(CreateModel(model, element));
        }

        public void Update(SystemBlockBindingModel model)
        {
            var element = source.SystemBlocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private SystemBlock CreateModel(SystemBlockBindingModel model, SystemBlock computer)
        {
            computer.Brand = model.Brand;
            computer.BlockType = model.BlockType;
            computer.DateCreate = model.DateCreate;
            return computer;
        }
        private SystemBlockViewModel CreateModel(SystemBlock computer)
        {
            return new SystemBlockViewModel
            {
                Id = (int)computer.Id,
                BlockType = computer.BlockType,
                DateCreate = computer.DateCreate,
                Brand = computer.Brand
            };
        }
    }
}
