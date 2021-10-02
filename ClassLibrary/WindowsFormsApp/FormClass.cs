using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Unity;

using LibraryClass.BindingModels;
using LibraryClass.BusinessLogics;
using LibraryClass.ViewModels;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApp
{
    public partial class FormClass : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ClassLogic classLogic;

        private readonly ReportLogic reportLogic;

        public FormClass(ClassLogic classLogic, ReportLogic reportLogic)
        {
            InitializeComponent();
            this.classLogic = classLogic;
            this.reportLogic = reportLogic;
        }

        private void FormClass_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(classLogic.Read(null), dataGridView);

                /*
                var listOfClasses = classLogic.Read(null);
                if (listOfClasses != null)
                {
                    dataGridView.DataSource = listOfClasses;
                    dataGridView.Columns[0].Visible = true;
                    dataGridView.Columns[1].Visible = true;
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || !Regex.Match(textBoxName.Text, @"^[a-zA-Zа-яА-Я0-9]+$").Success)
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCategory.Text) || !Regex.Match(textBoxName.Text, @"^[a-zA-Zа-яА-Я0-9]+$").Success)
            {
                MessageBox.Show("Заполните заполните категорию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker.Value == null )
            {
                MessageBox.Show("Заполните дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                classLogic.CreateOrUpdate(new ClassBindingModel
                {
                    Name = textBoxName.Text,
                    Category = textBoxCategory.Text,
                    Date = dateTimePicker.Value
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCategory.Text))
            {
                MessageBox.Show("Заполните заполните категорию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker.Value == null)
            {
                MessageBox.Show("Заполните дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? id = null;

            if (dataGridView.SelectedRows.Count == 1)
            {
                id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
            }

            try
            {
                classLogic.CreateOrUpdate(new ClassBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text,
                    Category = textBoxCategory.Text,
                    Date = dateTimePicker.Value
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        classLogic.Delete(new ClassBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonDopClasses_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDopClass>();
            form.Visible = true;
        }

        [Obsolete]
        private void buttonPDF_Click(object sender, EventArgs e)
        {
            SavePDF();
        }

        [Obsolete]
        private void SavePDF()
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        reportLogic.SaveToPdf(dateTimePickerFrom.Value, dateTimePickerTo.Value, dialog.FileName);

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ToXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ClassViewModel>));

            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, classLogic.Read(null));
                Console.WriteLine("Объект сериализован");
            }

            using (FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate))
            {
                List<ClassViewModel> list = (List<ClassViewModel>)formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
            }
        }

        [Obsolete]
        private void buttonToXML_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(ToXML);
            thread1.Start();

            SavePDF();            
        }
    }
}
