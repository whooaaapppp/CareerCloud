using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        protected readonly string _connstr;
        public ApplicantEducationRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }


        public void Add(params ApplicantEducationPoco[] items)
        {
            //creating sql con
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantEducationPoco item in items)
                {
                    //preppping the sql payloader
                    comm.CommandText = @"INSERT INTO[dbo].[Applicant_Educations]
                                        ([Id], 
                                         [Applicant], 
                                         [Major], 
                                         [Certificate_Diploma], 
                                         [Start_Date], 
                                         [Completion_Date], 
                                         [Completion_Percent]
                                        )
                                        VALUES
                                        (@Id,
                                         @Applicant,
                                         @Major,
                                         @Certificate_Diploma,
                                         @Start_Date,
                                         @Completion_Date,
                                         @Completion_Percent
                                        )";
                    comm.Parameters.AddWithValue("@Id",item.Id);
                    comm.Parameters.AddWithValue("@Applicant", item.Applicant);
                    comm.Parameters.AddWithValue("@Major", item.Major);
                    comm.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    comm.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    comm.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    comm.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                    //sql open execute connection sequence
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                    
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            throw new NotImplementedException();
        }
    }
}
