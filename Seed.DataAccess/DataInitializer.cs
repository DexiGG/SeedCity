using Seed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.DataAccess
{
    public class DataInitializer : CreateDatabaseIfNotExists<CityContext>
    {
        protected override void Seed(CityContext context)
        {
            context.Cities.AddRange(new List<City>
            {
                new City
                {
                    Name = "Алматы",
                    PhoneCode = "+7(727)"
                },
                new City
                {
                    Name = "Семей",
                    PhoneCode = "+7(7222)"
                },
                new City
                {
                    Name = "Актау",
                    PhoneCode = "+7(7292)"
                },
                new City
                {
                    Name = "Шымкент",
                    PhoneCode = "+7(7252)"
                }
            });
        }
    }
}
