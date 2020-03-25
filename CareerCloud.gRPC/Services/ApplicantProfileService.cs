using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {

            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());

        }

        public override Task<ApplicantProfilePayload> ReadApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<ApplicantProfilePayload>(
                () => new ApplicantProfilePayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    Currency = poco.Currency,
                    CurrentSalary = poco.CurrentSalary is null ? 0 : (double)poco.CurrentSalary,
                    CurrentRate = poco.CurrentRate is null ? 0 : (double)poco.CurrentRate,
                    Country = poco.Country,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                }
                ) ;
        }
    }
}
