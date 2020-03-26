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
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {

            _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());

        }

        public override Task<CompanyJobEducationPayload> ReadCompanyJobEducation(IdRequestCompanyJobEducation request, ServerCallContext context)
        {


            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyJobEducationPayload>(
                () => new CompanyJobEducationPayload()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Major = poco.Major,
                    Importance = poco.Importance,
                    
                    
                }
                );
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                    Major = request.Major,
                    Importance = (short)request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.CreateCompanyJobEducation(request, context);
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                    Major = request.Major,
                    Importance = (short)request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateCompanyJobEducation(request, context);
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                    Major = request.Major,
                    Importance = (short)request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteCompanyJobEducation(request, context);
        }
    }
}
