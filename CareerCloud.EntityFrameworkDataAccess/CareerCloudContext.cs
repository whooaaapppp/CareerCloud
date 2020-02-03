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
                .HasMany(x => x.CompanyDescriptions)
                .WithOne(x => x.SystemLanguageCodes)
                .HasForeignKey(x => x.LanguageId);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(x => x.ApplicantWorkHistory)
                .WithOne(x => x.SystemCountryCodes)
                .HasForeignKey(x => x.CountryCode);            

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(x => x.ApplicantProfiles)
                .WithOne(x => x.SystemCountryCodes)
                .HasForeignKey(x => x.Country);            

            modelBuilder.Entity<SecurityRolePoco>()
                .HasMany(x => x.SecurityLoginsRoles)
                .WithOne(x => x.SecurityRoles)
                .HasForeignKey(x => x.Id);
           
            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(x => x.SecurityLoginsLog)
                .WithOne(x => x.SecurityLogins)
                .HasForeignKey(x=>x.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(x=> x.ApplicantProfiles)
                .WithOne(x=> x.SecurityLogins)
                .HasForeignKey(x=>x.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(x => x.SecurityLoginsRoles)
                .WithOne(x => x.SecurityLogins)
                .HasForeignKey(x => x.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(x => x.ApplicantSkills)
                .WithOne(x => x.ApplicantProfiles)
                .HasForeignKey(x => x.Applicant);


            //don't map timestamp, with optimistic concurrency detection -> .Property(t => t.TimeStamp).IsRowVersion() or .Ignore(t => t.TimeStamp);
            modelBuilder.Entity<ApplicantProfilePoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<ApplicantEducationPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<ApplicantSkillPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyDescriptionPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyJobSkillPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyJobPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyJobEducationPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyLocationPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<CompanyProfilePoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<SecurityLoginPoco>().Property(t => t.TimeStamp).IsRowVersion();
            modelBuilder.Entity<SecurityLoginsRolePoco>().Property(t => t.TimeStamp).IsRowVersion();














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
