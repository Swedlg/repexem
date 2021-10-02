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
using System.Text.RegularExpressions;

namespace WindowsFormsApp
{
    public partial class FormDopClass : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly DopClassLogic dopClassLogic;

        private readonly ClassLogic classLogic;

        public FormDopClass(DopClassLogic dopClassLogic, ClassLogic classLogic)
        {
            InitializeComponent();
            this.dopClassLogic = dopClassLogic;
            this.classLogic = classLogic;
        }

        private void FormDopClass_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var listOfDopClasses = dopClassLogic.Read(null);
                if (listOfDopClasses != null)
                {
                    dataGridView.DataSource = listOfDopClasses;
                    dataGridView.Columns[0].Visible = true;
                    dataGridView.Columns[1].Visible = true;
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[3].Visible = true;
                    dataGridView.Columns[4].Visible = true;
                    dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }


                var listOfClasses = classLogic.Read(null);
                if (listOfClasses != null)
                {
                    comboBoxClassId.DataSource = listOfClasses;
                    comboBoxClassId.DisplayMember = "Name";
                    comboBoxClassId.ValueMember = "Id";
                    comboBoxClassId.SelectedItem = null;
                }
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
            if (string.IsNullOrEmpty(textBoxField1.Text) || !Regex.Match(textBoxName.Text, @"^[a-zA-Zа-яА-Я0-9]+$").Success)
            {
                MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxField2.Text) || !Regex.Match(textBoxName.Text, @"^[0-9]+$").Success)
            {
                MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker.Value == null)
            {
                MessageBox.Show("Заполните дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClassId.SelectedValue == null)
            {
                MessageBox.Show("Заполните дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                dopClassLogic.CreateOrUpdate(new DopClassBindingModel
                {
                    DopName = textBoxName.Text,
                    ClassId = Convert.ToInt32(comboBoxClassId.SelectedValue),
                    DopDate = dateTimePicker.Value,
                    DopField = textBoxField1.Text,
                    DopField2 = Convert.ToInt32(textBoxField2.Text)
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
            if (string.IsNullOrEmpty(textBoxField1.Text))
            {
                MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxField2.Text))
            {
                MessageBox.Show("Заполните поле", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dateTimePicker.Value == null)
            {
                MessageBox.Show("Заполните дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClassId.SelectedValue == null)
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
                dopClassLogic.CreateOrUpdate(new DopClassBindingModel
                {
                    Id = id,
                    DopName = textBoxName.Text,
                    ClassId = Convert.ToInt32(comboBoxClassId.SelectedValue),
                    DopDate = dateTimePicker.Value,
                    DopField = textBoxField1.Text,
                    DopField2 = Convert.ToInt32(textBoxField2.Text)
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
                        dopClassLogic.Delete(new DopClassBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }      
    }
}
