using Seed.DataAccess;
using Seed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seed.WinForm
{
    public partial class DataForm : Form
    {
        private readonly CityContext context;
        private City city;
        public DataForm()
        {
            InitializeComponent();
            context = new CityContext();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxName.Text.Length > 0 &&
                    textBoxCode.Text.Length > 0)
                {
                    if (Text == "Добавление")
                    {
                        city = new City
                        {
                            Name = textBoxName.Text,
                            PhoneCode = textBoxCode.Text
                        };
                        if (DuplicationCheck(city.Name, city.PhoneCode))
                            throw new Exception("Город с такими данными уже существует");
                        context.Cities.Add(city);
                    }
                    else if (Text == "Изменение")
                    {
                        city = context.Cities
                            .Where(c => c.Id == Form1.city.Id).FirstOrDefault();

                        city.Name = textBoxName.Text;
                        city.PhoneCode = textBoxCode.Text;
                        if (DuplicationCheck(city))
                            throw new Exception("Город с такими данными уже существует");
                    }
                    
                    context.SaveChanges();
                    MessageBox.Show("Операция прошла успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    throw new Exception("Один или обе полей пустые, отказано в действии");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Hide();
                Form1 form = new Form1();
                form.ShowDialog();
                Close();
            }
        }

        private bool DuplicationCheck(City city)
        {
            var result = context.Cities
                .Where(c => (c.Name == city.Name) && (c.PhoneCode == city.PhoneCode))
                .Where(c => c.Id != city.Id).FirstOrDefault();
            if (result != null)
                return true;
            return false;
        }

        private bool DuplicationCheck(string name, string code)
        {
            var result = context.Cities
                .Where(c => (c.Name == name) && (c.PhoneCode == code)).FirstOrDefault();
            if (result != null)
                return true;
            return false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }
    }
}
