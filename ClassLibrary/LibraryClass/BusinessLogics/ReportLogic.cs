using LibraryClass.BindingModels;
//using LibraryClass.HelperModels;
using LibraryClass.Interfaces;
using LibraryClass.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace LibraryClass.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IClassStorage classStorage;
        private readonly IDopClassStorage dopClassStorage;

        public ReportLogic(IClassStorage classStorage, IDopClassStorage dopClassStorage)
        {
            this.classStorage = classStorage;
            this.dopClassStorage = dopClassStorage;
        }

        public List<ReportViewModel> GetClassDopClass(DateTime _from, DateTime _to)
        {

            return (from _class in classStorage.GetFilteredList(new ClassBindingModel { DateFrom = _from, DateTo = _to })
                   join dopClass in dopClassStorage.GetFullList()
                   on _class.Id equals dopClass.ClassId
                   select new ReportViewModel { 
                       Name = _class.Name,
                       Date = _class.Date,
                       DopName = dopClass.DopName,
                       DopField = dopClass.DopField,
                       DopDate = dopClass.DopDate
                   }).ToList();

            /*
            List<ClassViewModel> classes = classStorage.GetFilteredList(new ClassBindingModel
            {
                DateFrom = from,
                DateTo = to
            });
            */

            /*
            classes.Join(dopClassStorage.GetFullList)


            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                JewelName = x.JewelName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
            */


            //return;
        }

        [Obsolete]
        public void SaveToPdf(DateTime _from, DateTime _to, string path)
        {
            List<ReportViewModel> reports_list = GetClassDopClass(_from, _to);

            Document document = new Document();

            Section section = document.AddSection();

            foreach (var report in reports_list)
            {
                Paragraph paragraph = section.AddParagraph();

                string rep = report.Name + " " + report.Date + " "
                    + report.DopName + " " + report.DopField + " " + report.DopDate;

                paragraph.AddText(rep);
            }
            
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
            renderer.Document = document;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(path);

            /*
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
            */
        }
    }
}
