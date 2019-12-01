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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        protected readonly string _connstr;
        public ApplicantProfileRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                
                foreach(ApplicantProfilePoco item in items)
                {
                    comm.CommandText = @"INSERT INTO dbo.Applicant_Profiles( Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code )
                                        VALUES( @Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code )";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Login", item.Login);
                    comm.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    comm.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    comm.Parameters.AddWithValue("@Currency", item.Currency);
                    comm.Parameters.AddWithValue("@Country_Code", item.Country);
                    comm.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    comm.Parameters.AddWithValue("@Street_Address", item.Street);
                    comm.Parameters.AddWithValue("@City_Town", item.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    

                }
            }
        }

        

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<ApplicantProfilePoco> appProfilePocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return appProfilePocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        /* unimplemented interface methods for future iterations */
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
