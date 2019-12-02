﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        protected readonly string _connstr;

        public CompanyProfileRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach(CompanyProfilePoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"SELECT [Id], [Registration_Date], [Company_Website], [Contact_Phone], [Contact_Name], [Company_Logo], [Time_Stamp]
                                        FROM [dbo].[Company_Profiles]";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    comm.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    comm.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    comm.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    comm.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<CompanyProfilePoco> companyProfilePocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return companyProfilePocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                        SET [Registration_Date] = @Registration_Date, [Company_Website] = @Company_Website, [Contact_Phone] = @Contact_Phone, [Contact_Name] = @Contact_Name, [Company_Logo] = @Company_Logo
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    comm.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    comm.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    comm.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    comm.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        /* unimplemented interface methods for future iterations */
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
