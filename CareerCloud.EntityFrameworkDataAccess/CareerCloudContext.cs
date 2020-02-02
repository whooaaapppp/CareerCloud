using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

            //pass db sql config on the optionsBuilder
            optionsBuilder.UseSqlServer(_connstr);

            base.OnConfiguring(optionsBuilder);

        }
    }
}
