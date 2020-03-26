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
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationService()
        {

            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());

        }

        public override Task<ApplicantEducationPayload> ReadApplicantEducation(IdRequestApplicantEducation request, ServerCallContext context)
        {


            ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<ApplicantEducationPayload>(
                () => new ApplicantEducationPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CertificateDiploma = poco.CertificateDiploma,
                    CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                    CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent,
                    Major = poco.Major,
                    StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate)
                }
                );
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new ApplicantEducationPoco[] { new ApplicantEducationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    CertificateDiploma = request.CertificateDiploma,
                    CompletionDate = request.CompletionDate is null ? DateTime.MinValue : Convert.ToDateTime(request.CompletionDate),
                    CompletionPercent = (byte?)request.CompletionPercent,
                    Major = request.Major,
                    StartDate = request.StartDate is null ? DateTime.MinValue : Convert.ToDateTime(request.StartDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateApplicantEducation(request, context);
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new ApplicantEducationPoco[] { new ApplicantEducationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    CertificateDiploma = request.CertificateDiploma,
                    CompletionDate = request.CompletionDate is null ? DateTime.MinValue : Convert.ToDateTime(request.CompletionDate),
                    CompletionPercent = (byte?)request.CompletionPercent,
                    Major = request.Major,
                    StartDate = request.StartDate is null ? DateTime.MinValue : Convert.ToDateTime(request.StartDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateApplicantEducation(request, context);
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new ApplicantEducationPoco[] { new ApplicantEducationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    CertificateDiploma = request.CertificateDiploma,
                    CompletionDate = request.CompletionDate is null ? DateTime.MinValue : Convert.ToDateTime(request.CompletionDate),
                    CompletionPercent = (byte?)request.CompletionPercent,
                    Major = request.Major,
                    StartDate = request.StartDate is null ? DateTime.MinValue : Convert.ToDateTime(request.StartDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteApplicantEducation(request, context);
        }
    }
}
