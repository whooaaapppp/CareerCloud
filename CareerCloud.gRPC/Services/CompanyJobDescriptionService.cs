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
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

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
                    JobName = poco.JobName,
                    JobDescriptions = poco.JobDescriptions
                    
                    
                }
                );
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),
                    JobName = request.JobName,
                    JobDescriptions = request.JobDescriptions
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateCompanyJobDescription(request, context);
        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {

            try
            {
                _logic.Update(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),
                    JobName = request.JobName,
                    JobDescriptions = request.JobDescriptions
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.UpdateCompanyJobDescription(request, context);
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),
                    JobName = request.JobName,
                    JobDescriptions = request.JobDescriptions
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteCompanyJobDescription(request, context);
        }
    }
}
