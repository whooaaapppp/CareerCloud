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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        protected readonly string _connstr;
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connstr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                foreach (SecurityLoginPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"INSERT INTO [dbo].[Security_Logins]( [Id], [Login], [Password], [Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked], [Is_Inactive], [Email_Address], [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language] )
                                        VALUES( @Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language )";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Login", item.Login);
                    comm.Parameters.AddWithValue("@Password", item.Password);
                    comm.Parameters.AddWithValue("@Created_Date", item.Created);
                    comm.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    comm.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    comm.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    comm.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    comm.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    comm.Parameters.AddWithValue("@Full_Name", item.FullName);
                    comm.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    comm.Parameters.AddWithValue("@Preffered_Language", item.PrefferredLanguage);
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;

                

            }
        }

        

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            /* https://docs.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=netframework-4.8 */
            IQueryable<SecurityLoginPoco> securityLoginPocos = GetAll().AsQueryable();
            /* return first element of a sequence or a default value if the seq contains no elements that satisfy the where predicate */
            return securityLoginPocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityLoginPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                         WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    //rows affected
                    connection.Open();
                    int rowAffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connstr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityLoginPoco item in items)
                {
                    //prepping the sql query
                    comm.CommandText = @"UPDATE [dbo].[Security_Logins]
                                        SET [Login] = @Login, [Password] = @Password, [Created_Date] = @Created_Date, [Password_Update_Date] = @Password_Update_Date, [Agreement_Accepted_Date] = @Agreement_Accepted_Date, [Is_Locked] = @Is_Locked, [Is_Inactive] = @Is_Inactive, [Email_Address] = @Email_Address, [Phone_Number] = @Phone_Number, [Full_Name] = @Full_Name, [Force_Change_Password] = @Force_Change_Password, [Prefferred_Language] = @Prefferred_Language
                                        WHERE [Id] = @Id";
                    comm.Parameters.AddWithValue("@Id", item.Id);
                    comm.Parameters.AddWithValue("@Login", item.Login);
                    comm.Parameters.AddWithValue("@Password", item.Password);
                    comm.Parameters.AddWithValue("@Created_Date", item.Created);
                    comm.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    comm.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    comm.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    comm.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    comm.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    comm.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    comm.Parameters.AddWithValue("@Full_Name", item.FullName);
                    comm.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    comm.Parameters.AddWithValue("@Preffered_Language", item.PrefferredLanguage);


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
        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
