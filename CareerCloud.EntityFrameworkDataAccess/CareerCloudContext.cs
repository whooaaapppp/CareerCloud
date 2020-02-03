using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        

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

        //EF6 uses DbModelBuilder while EFCore uses ModelBuilder https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.onmodelcreating?view=efcore-3.1
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(c => c.CompanyDescriptions)
                .WithOne(d => d.SystemLanguageCodes)
                .HasForeignKey(e => e.LanguageId);


            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.ApplicantWorkHistory)
                .WithOne(d => d.SystemCountryCodes)
                .HasForeignKey(e => e.CountryCode);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.ApplicantProfiles)
                .WithOne(d => d.SystemCountryCodes)
                .HasForeignKey(e => e.Country);









            base.OnModelCreating(modelBuilder);
        }

        //setting DbSet properties for each Poco per CareerCloud.Pocos
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


    }
}
