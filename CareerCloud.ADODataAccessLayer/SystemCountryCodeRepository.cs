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
    protected readonly string _connstr;
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        protected readonly string _connstr;
        public SystemCountryCodeRepository()
        {
            //create Config Builder
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
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
        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
