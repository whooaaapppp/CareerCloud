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

    
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        protected readonly string _connstr;
        public CompanyLocationRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach(CompanyLocationPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"";
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (CompanyLocationPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"";
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (CompanyLocationPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"";
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
        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
