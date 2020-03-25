using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService()
        {

            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());

        }

        public override Task<CompanyLocationPayload> ReadCompanyLocation(IdRequestCompanyLocation request, ServerCallContext context)
        {


            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyLocationPayload>(
                () => new CompanyLocationPayload()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    CountryCode = poco.CountryCode,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                }
                );
        }
    }
}
