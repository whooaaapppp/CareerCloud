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
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryService()
        {

            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());

        }

        public override Task<ApplicantWorkHistoryPayload> ReadApplicantWorkHistory(IdRequestApplicantWorkHistory request, ServerCallContext context)
        {


            ApplicantWorkHistoryPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<ApplicantWorkHistoryPayload>(
                () => new ApplicantWorkHistoryPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CompanyName = poco.CompanyName,
                    CountryCode = poco.CountryCode,
                    Location = poco.Location,
                    JobTitle = poco.JobTitle,
                    JobDescription = poco.JobDescription,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                }
                );
        }

        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new ApplicantWorkHistoryPoco[] { new ApplicantWorkHistoryPoco() {
                Id = new Guid(request.Id),
                    
                    Applicant = new Guid(request.Applicant),
                    CompanyName = request.CompanyName,
                    CountryCode = request.CountryCode,
                    Location = request.Location,
                    JobTitle = request.JobTitle,
                    JobDescription = request.JobDescription,
                    StartMonth = (short)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (short)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateApplicantWorkHistory(request, context);
        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {

            try
            {
                _logic.Update(new ApplicantWorkHistoryPoco[] { new ApplicantWorkHistoryPoco() {
                Id = new Guid(request.Id),

                    Applicant = new Guid(request.Applicant),
                    CompanyName = request.CompanyName,
                    CountryCode = request.CountryCode,
                    Location = request.Location,
                    JobTitle = request.JobTitle,
                    JobDescription = request.JobDescription,
                    StartMonth = (short)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (short)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.UpdateApplicantWorkHistory(request, context);
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new ApplicantWorkHistoryPoco[] { new ApplicantWorkHistoryPoco() {
                Id = new Guid(request.Id),

                    Applicant = new Guid(request.Applicant),
                    CompanyName = request.CompanyName,
                    CountryCode = request.CountryCode,
                    Location = request.Location,
                    JobTitle = request.JobTitle,
                    JobDescription = request.JobDescription,
                    StartMonth = (short)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (short)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteApplicantWorkHistory(request, context);
        }
    }
}
