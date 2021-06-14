using examBusinessLogic.BindingModels;
using examBusinessLogic.HelperModels;
using examBusinessLogic.Interfaces;
using examBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace examBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly ISystemBlockStorage _systemBlockStorage;
        public ReportLogic(ISystemBlockStorage systemBlockStorage, IComponentStorage componentStorage)
        {
            _systemBlockStorage = systemBlockStorage;
            _componentStorage = componentStorage;
        }

        public List<ReportViewModel> GetSysBlocks(ReportBindingModel model)
        {
            return _componentStorage.GetFilteredList(new ComponentBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportViewModel
            {
                Brand = _systemBlockStorage.GetElement(new SystemBlockBindingModel
                {
                    Id = x.SystemBlockId
                }).Brand,
                Firm = x.Firm,
                SystemBlockDateCreate = _systemBlockStorage.GetElement(new SystemBlockBindingModel
                {
                    Id = x.SystemBlockId
                }).DateCreate,
                ComponentName = x.Name,
                ComponentDateCreate = x.DateCreate
            })
           .ToList();
        }

        [Obsolete]
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                SystemBlocks = GetSysBlocks(model)
            });
        }

    }
}
