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
    }
}
