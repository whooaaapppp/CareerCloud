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
using static CareerCloud.gRPC.Protos.SystemCountryCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;
        

        public SystemCountryCodeService()
        {

            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());

        }

        public override Task<SystemCountryCodePayload> ReadSystemCountryCode(IdRequestSystemCountryCode request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Code);

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SystemCountryCodePayload>(
                () => new SystemCountryCodePayload()
                {
                    Code = poco.Code,
                    Name = poco.Name
                    
                }
                ) ;
        }

        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new SystemCountryCodePoco[] { new SystemCountryCodePoco() {
                Code = request.Code,
                Name = request.Name
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
           


            return base.CreateSystemCountryCode(request, context);
        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new SystemCountryCodePoco[] { new SystemCountryCodePoco() {
                Code = request.Code,
                Name = request.Name
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateSystemCountryCode(request, context);
        }

        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new SystemCountryCodePoco[] { new SystemCountryCodePoco() {
                Code = request.Code,
                Name = request.Name
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteSystemCountryCode(request, context);
        }
    }
}
