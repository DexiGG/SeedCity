using Seed.DataAccess;
using Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seed.WinForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var context = new CityContext())
            {
                context.Cities.Add(new City
                {
                    Name = "Алматы",
                    PhoneCode = "+7(727)"
                });
                context.SaveChanges();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
