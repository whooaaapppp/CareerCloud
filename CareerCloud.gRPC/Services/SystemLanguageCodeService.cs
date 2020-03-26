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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;
        

        public SystemLanguageCodeService()
        {

            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());

        }

        public override Task<SystemLanguageCodePayload> ReadSystemLanguageCode(IdRequestSystemLanguageCode request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageID);

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SystemLanguageCodePayload>(
                () => new SystemLanguageCodePayload()
                {
                    LanguageID = poco.LanguageID,
                    Name = poco.Name,
                    NativeName = poco.NativeName
                    
                }
                ) ;
        }
    }
}
