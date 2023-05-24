using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MillionAndUpApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class BaseTests
    {
        protected ApplicationDbContext BuildContext(string nameDB)
        {
            IConfiguration _configuration = new ConfigurationBuilder().Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nameDB).Options;
            var dbcontext = new ApplicationDbContext(options, _configuration);
            return dbcontext;
        }
    }
}
