using examBusinessLogic.BindingModels;
using examBusinessLogic.Interfaces;
using examBusinessLogic.ViewModels;
using examDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace examDatabaseImplement.Implements
{
    public class SystemBlockStorage : ISystemBlockStorage
    {
        public void Delete(SystemBlockBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            using (var context = new ExamDatabase())
            {
                SystemBlock element = context.SystemBlocks.FirstOrDefault(rec => rec.Id == model.Id);
                if(element != null)
                { 
                    context.SystemBlocks.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public SystemBlockViewModel GetElement(SystemBlockBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ExamDatabase())
            {
                var element = context.SystemBlocks
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return element != null ?
                    new SystemBlockViewModel
                    {
                        Id = element.Id,
                        Brand = element.Brand,
                        BlockType = element.BlockType,
                        DateCreate = element.DateCreate
                    } : null;
            }
        }

        public List<SystemBlockViewModel> GetFilteredList(SystemBlockBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ExamDatabase())
            {
                return context.SystemBlocks
                    .Where(rec => rec.Brand == model.Brand)
                    .Select(rec => new SystemBlockViewModel
                    {
                        Id = rec.Id,
                        Brand = rec.Brand,
                        BlockType = rec.BlockType,
                        DateCreate = rec.DateCreate,
                    })
                    .ToList();
            }
        }

        public List<SystemBlockViewModel> GetFullList()
        {
            using (var context = new ExamDatabase())
            {
                return context.SystemBlocks
                    .Select(rec => new SystemBlockViewModel
                    {
                        Id = rec.Id,
                        Brand = rec.Brand,
                        BlockType = rec.BlockType,
                        DateCreate = rec.DateCreate,
                    })
                    .ToList();
            }
        }

        public void Insert(SystemBlockBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                context.SystemBlocks.Add(CreateModel(model, new SystemBlock()));
                context.SaveChanges();
            }
        }

        public void Update(SystemBlockBindingModel model)
        {
            using (var context = new ExamDatabase())
            {
                var element = context.SystemBlocks.FirstOrDefault(rec => rec.Id == model.Id);
                if(element != null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        private SystemBlock CreateModel(SystemBlockBindingModel model, SystemBlock systemBlock)
        {
            if (model.Id.HasValue)
            {
                systemBlock.Brand = model.Brand;
                systemBlock.BlockType = model.BlockType;
                systemBlock.DateCreate = model.DateCreate;
            }
            return systemBlock;
        }
    }
}
