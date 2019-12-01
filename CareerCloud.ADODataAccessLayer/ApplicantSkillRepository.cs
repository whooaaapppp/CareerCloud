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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {

        protected readonly string _connstr;
        public ApplicantSkillRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (ApplicantSkillPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]( [Id], [Applicant], [Skill], [Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year] )
                                        VALUES( @Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year )";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                    comm.Parameters.AddWithValue("@Skill", item.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    comm.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", item.EndYear);
                    //extract rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<ApplicantSkillPoco> appSkillPocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return appSkillPocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach(ApplicantSkillPoco item in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (ApplicantSkillPoco item in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                        SET [Applicant] = @Applicant, [Skill] = @Skill, [Skill_Level] = @Skill_Level, [Start_Month] = @Start_Month, [Start_Year] = @Start_Year, [End_Month] = @End_Month, [End_Year] = @End_Year
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                    comm.Parameters.AddWithValue("@Skill", item.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    comm.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", item.EndYear);
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        /* unimplemented interface methods for future iterations */
        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
