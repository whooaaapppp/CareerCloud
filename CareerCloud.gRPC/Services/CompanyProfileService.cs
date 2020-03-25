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
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;
        

        public CompanyProfileService()
        {

            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());

        }

        public override Task<CompanyProfilePayload> ReadCompanyProfile(IdRequestCompanyProfile request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyProfilePayload>(
                () => new CompanyProfilePayload()
                {
                    Id = poco.Id.ToString(),
                    RegistrationDate = Timestamp.FromDateTime(poco.RegistrationDate),
                    CompanyWebsite = poco.CompanyWebsite,
                    ContactPhone = poco.ContactPhone,
                    ContactName = poco.ContactName,
                    CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)
                }
                ) ;
        }
    }
}
