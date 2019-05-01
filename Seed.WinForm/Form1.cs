using Seed.DataAccess;
using Seed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seed.WinForm
{
    public partial class Form1 : Form
    {
        private readonly CityContext context;
        public static City city;
        public Form1()
        {
            InitializeComponent();
            context = new CityContext();
            context.Cities.Load();

            dataGridView1.DataSource = context.Cities.Local.ToBindingList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Hide();
            DataForm dataForm = new DataForm();
            dataForm.Text = "Добавление";

            dataForm.ShowDialog();
            Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Hide();
            DataForm dataForm = new DataForm();
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = int.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                city = context.Cities.Find(id);
                dataForm.textBoxName.Text = city.Name;
                dataForm.textBoxCode.Text = city.PhoneCode;
                dataForm.Text = "Изменение";
                
                dataForm.ShowDialog();
            }
            Close();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = int.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                City city = context.Cities.Find(id);
                try
                {
                    context.Cities.Remove(city);
                    context.SaveChanges();
                    dataGridView1.Refresh();
                    MessageBox.Show("Объект удален", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if(dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBoxCity.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
