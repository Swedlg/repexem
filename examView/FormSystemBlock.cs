using System;
using System.Collections.Generic;
using System.Windows.Forms;
using examBusinessLogic.BindingModels;
using examBusinessLogic.BusinessLogic;
using examBusinessLogic.ViewModels;
using Unity;

namespace examView
{
    public partial class FormSystemBlock : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly SystemBlockLogic logic;
        private int? id;
        
        public FormSystemBlock(SystemBlockLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSystemBlock_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SystemBlockViewModel view = logic.Read(new SystemBlockBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxBrand.Text = view.Brand;
                        textBoxBlockType.Text = view.BlockType.ToString();                        
                        LoadData();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }           
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSystemBlocksComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {               
                LoadData();
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSystemBlocksComponent>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
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
                        logic.Delete(new SystemBlockBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
    

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBrand.Text))
            {
                MessageBox.Show("Заполните бренд", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxBlockType.Text))
            {
                MessageBox.Show("Заполните тип", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           
            try
            {
                logic.CreateOrUpdate(new SystemBlockBindingModel
                {
                    Id = id,
                    Brand = textBoxBrand.Text,
                    BlockType = textBoxBlockType.Text,                   
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
