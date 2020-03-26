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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;
        

        public SecurityLoginsLogService()
        {

            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());

        }

        public override Task<SecurityLoginsLogPayload> ReadSecurityLoginsLog(IdRequestSecurityLoginsLog request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SecurityLoginsLogPayload>(
                () => new SecurityLoginsLogPayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    SourceIP = poco.SourceIP,
                    LogonDate = Timestamp.FromDateTime(poco.LogonDate),
                    IsSuccesful = poco.IsSuccesful
        }
                ) ;
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = Convert.ToDateTime(request.LogonDate),
                IsSuccesful = request.IsSuccesful
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateSecurityLoginsLog(request, context);
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = Convert.ToDateTime(request.LogonDate),
                IsSuccesful = request.IsSuccesful
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateSecurityLoginsLog(request, context);
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {

            try
            {
                _logic.Delete(new SecurityLoginsLogPoco[] { new SecurityLoginsLogPoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = Convert.ToDateTime(request.LogonDate),
                IsSuccesful = request.IsSuccesful
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.DeleteSecurityLoginsLog(request, context);
        }
    }
}
