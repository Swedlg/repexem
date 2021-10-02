using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FileImplement.Models;

namespace FileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string ClassFileName = "Class.xml";

        private readonly string DopClassFileName = "DopClass.xml";

        public List<Class> Classes { get; set; }

        public List<DopClass> DopClasses { get; set; }

        private FileDataListSingleton()
        {
            Classes = LoadClasses();
            DopClasses = LoadDopClasses();

        }

        private List<Class> LoadClasses()
        {
            var list = new List<Class>();

            if (File.Exists(ClassFileName))
            {
                XDocument xDocument = XDocument.Load(ClassFileName);
                var xElements = xDocument.Root.Elements("Class").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Class
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        Category = elem.Element("Category").Value,
                        Date = Convert.ToDateTime(elem.Element("Date").Value)
                    });
                }
            }
            return list;
        }


        private List<DopClass> LoadDopClasses()
        {
            var list = new List<DopClass>();
            if (File.Exists(DopClassFileName))
            {
                XDocument xDocument = XDocument.Load(DopClassFileName);
                var xElements = xDocument.Root.Elements("DopClass").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new DopClass
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DopName = elem.Element("DopName").Value,
                        DopDate = Convert.ToDateTime(elem.Element("DopDate").Value),
                        DopField = elem.Element("DopField").Value,
                        DopField2 = Convert.ToInt32(elem.Element("DopField2").Value),
                        ClassId = Convert.ToInt32(elem.Element("ClassId").Value)
                    });
                }
            }
            return list;
        }


        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveClasses();
            SaveDopClasses();
        }

        private void SaveClasses()
        {
            if (Classes != null)
            {
                var xElement = new XElement("Classes");
                foreach (var _class in Classes)
                {
                    xElement.Add(new XElement("Class",
                    new XAttribute("Id", _class.Id),
                    new XElement("Name", _class.Name),
                    new XElement("Date", _class.Date),
                    new XElement("Category", _class.Category)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClassFileName);
            }
        }

        private void SaveDopClasses()
        {
            if (DopClasses != null)
            {
                var xElement = new XElement("DopClasses");
                foreach (var dopClass in DopClasses)
                {
                    xElement.Add(new XElement("DopClass",
                    new XAttribute("Id", dopClass.Id),
                    new XElement("DopName", dopClass.DopName),
                    new XElement("DopDate", dopClass.DopDate),
                    new XElement("DopField", dopClass.DopField),
                    new XElement("DopField2", dopClass.DopField2),
                    new XElement("ClassId", dopClass.ClassId)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DopClassFileName);
            }
        }
    }
}
