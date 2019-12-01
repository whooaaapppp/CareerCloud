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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        protected readonly string _connstr;
        public CompanyDescriptionRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<CompanyDescriptionPoco> appCompanyDescriptionPocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return appCompanyDescriptionPocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach(CompanyDescriptionPoco item in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach(CompanyDescriptionPoco item in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                        SET [Company] = @Company, [LanguageID] = @LanguageID, [Company_Name] = @Company_Name, [Company_Description] = @Company_Description
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Company", item.Company);
                    comm.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    comm.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    comm.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
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
        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
