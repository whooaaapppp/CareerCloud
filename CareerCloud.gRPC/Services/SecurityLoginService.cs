using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;
        

        public SecurityLoginService()
        {

            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());

        }

        public override Task<SecurityLoginPayload> ReadSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SecurityLoginPayload>(
                () => new SecurityLoginPayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login,
                    Password = poco.Password,
                    Created = Timestamp.FromDateTime(poco.Created),
                    PasswordUpdate = poco.PasswordUpdate is null?null:Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                    AgreementAccepted = poco.AgreementAccepted is null?null:Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                    IsLocked = poco.IsLocked,
                    IsInactive = poco.IsInactive,
                    EmailAddress = poco.EmailAddress,
                    PhoneNumber = poco.PhoneNumber,
                    FullName = poco.FullName,
                    ForceChangePassword = poco.ForceChangePassword,
                    PreferredLanguage = poco.PrefferredLanguage
                }
                ) ;
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = Convert.ToDateTime(request.Created),
                    PasswordUpdate = request.PasswordUpdate is null?DateTime.MinValue:Convert.ToDateTime(request.PasswordUpdate),
                    AgreementAccepted = request.AgreementAccepted is null?DateTime.MinValue:Convert.ToDateTime(request.AgreementAccepted),
                    IsLocked = request.IsLocked,
                    IsInactive = request.IsInactive,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    FullName = request.FullName,
                    ForceChangePassword = request.ForceChangePassword,
                    PrefferredLanguage = request.PreferredLanguage
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateSecurityLogin(request, context);
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new SecurityLoginPoco[] { new SecurityLoginPoco() {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = Convert.ToDateTime(request.Created),
                    PasswordUpdate = request.PasswordUpdate is null?DateTime.MinValue:Convert.ToDateTime(request.PasswordUpdate),
                    AgreementAccepted = request.AgreementAccepted is null?DateTime.MinValue:Convert.ToDateTime(request.AgreementAccepted),
                    IsLocked = request.IsLocked,
                    IsInactive = request.IsInactive,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    FullName = request.FullName,
                    ForceChangePassword = request.ForceChangePassword,
                    PrefferredLanguage = request.PreferredLanguage
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateSecurityLogin(request, context);
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new SecurityLoginPoco[] { new SecurityLoginPoco() {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = Convert.ToDateTime(request.Created),
                    PasswordUpdate = request.PasswordUpdate is null?DateTime.MinValue:Convert.ToDateTime(request.PasswordUpdate),
                    AgreementAccepted = request.AgreementAccepted is null?DateTime.MinValue:Convert.ToDateTime(request.AgreementAccepted),
                    IsLocked = request.IsLocked,
                    IsInactive = request.IsInactive,
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    FullName = request.FullName,
                    ForceChangePassword = request.ForceChangePassword,
                    PrefferredLanguage = request.PreferredLanguage
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteSecurityLogin(request, context);
        }
    }
}
