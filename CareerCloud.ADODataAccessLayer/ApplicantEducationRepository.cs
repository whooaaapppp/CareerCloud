using CareerCloud.DataAccessLayer;
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
                    comm.Parameters.AddWithValue("@Id", item.Id);
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
            //getall list on the poco
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Major]
                                  ,[Certificate_Diploma]
                                  ,[Start_Date]
                                  ,[Completion_Date]
                                  ,[Completion_Percent]
                                  ,[Time_Stamp]
                                   FROM [dbo].[Applicant_Educations]";
                connection.Open();
                int index = 0;
                SqlDataReader sqlReader = comm.ExecuteReader();
                ApplicantEducationPoco[] appEduPocos = new ApplicantEducationPoco[500];
                //while has sql reader to read
                while (sqlReader.Read())
                {
                    ApplicantEducationPoco appEduPoco = new ApplicantEducationPoco();
                    appEduPoco.Id = sqlReader.GetGuid(0);
                    appEduPoco.Applicant = sqlReader.GetGuid(1);
                    appEduPoco.Major = sqlReader.GetString(2);
                    appEduPoco.CertificateDiploma = sqlReader.GetString(3);
                    appEduPoco.StartDate = (DateTime?)sqlReader.GetDateTime(4);
                    appEduPoco.CompletionDate = (DateTime?)sqlReader.GetDateTime(5);
                    appEduPoco.CompletionPercent = (byte?)sqlReader[6];
                    appEduPoco.TimeStamp = (byte[])sqlReader[7];
                    appEduPocos[index] = appEduPoco;
                    index++;
                }
                connection.Close();
                //return only non-null ApplicationEducationPoco object a and pass it to list
                return appEduPocos.Where(a => a != null).ToList();
            }
            
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<ApplicantEducationPoco> appEduPocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return appEduPocos.Where(where).FirstOrDefault(); 

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
