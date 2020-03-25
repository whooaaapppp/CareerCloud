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


namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescriptionService : CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionService()
        {

            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());

        }

        public override Task<CompanyJobDescriptionPayload> ReadCompanyJobDescription(IdRequestCompanyJobDescription request, ServerCallContext context)
        {


            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyJobDescriptionPayload>(
                () => new CompanyJobDescriptionPayload()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    JobString = poco.JobString,
                    JobDescriptions = poco.JobDescriptions
                    
                    
                }
                );
        }
    }
}
