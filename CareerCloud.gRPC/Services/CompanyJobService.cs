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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {

            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());

        }

        public override Task<CompanyJobPayload> ReadCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {


            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyJobPayload>(
                () => new CompanyJobPayload()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    ProfileCreated = Timestamp.FromDateTime(poco.ProfileCreated),
                    IsInactive = poco.IsInactive,
                    IsCompanyHidden = poco.IsCompanyHidden,
                }
                );
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyJobPoco[] { new CompanyJobPoco() {
                Id = new Guid(request.Id),
                    
                    Company = new Guid(request.Company),
                    ProfileCreated =  Convert.ToDateTime(request.ProfileCreated),
                    IsInactive = request.IsInactive,
                    IsCompanyHidden = request.IsCompanyHidden,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateCompanyJob(request, context);
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyJobPoco[] { new CompanyJobPoco() {
                Id = new Guid(request.Id),

                    Company = new Guid(request.Company),
                    ProfileCreated =  Convert.ToDateTime(request.ProfileCreated),
                    IsInactive = request.IsInactive,
                    IsCompanyHidden = request.IsCompanyHidden,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.UpdateCompanyJob(request, context);
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyJobPoco[] { new CompanyJobPoco() {
                Id = new Guid(request.Id),

                    Company = new Guid(request.Company),
                    ProfileCreated =  Convert.ToDateTime(request.ProfileCreated),
                    IsInactive = request.IsInactive,
                    IsCompanyHidden = request.IsCompanyHidden,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.DeleteCompanyJob(request, context);
        }

    }
}
