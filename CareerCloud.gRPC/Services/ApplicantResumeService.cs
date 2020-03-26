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
using static CareerCloud.gRPC.Protos.ApplicantResume;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantResumeService : ApplicantResumeBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeService()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        public override Task<ApplicantResumePayload> ReadApplicantResume(IdRequestApplicantResume request, ServerCallContext context)
        {
            ApplicantResumePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<ApplicantResumePayload>(
                () => new ApplicantResumePayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Resume = poco.Resume,
                    LastUpdated = poco.LastUpdated is null ? null : Timestamp.FromDateTime((DateTime)poco.LastUpdated)
                    
                }
                );
        }

        public override Task<Empty> CreateApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new ApplicantResumePoco[] { new ApplicantResumePoco() {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                    Resume = request.Resume,
                    LastUpdated = request.LastUpdated is null ? DateTime.MinValue : Convert.ToDateTime(request.LastUpdated)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateApplicantResume(request, context);
        }

        public override Task<Empty> UpdateApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new ApplicantResumePoco[] { new ApplicantResumePoco() {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                    Resume = request.Resume,
                    LastUpdated = request.LastUpdated is null ? DateTime.MinValue : Convert.ToDateTime(request.LastUpdated)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateApplicantResume(request, context);
        }

        public override Task<Empty> DeleteApplicantResume(ApplicantResumePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new ApplicantResumePoco[] { new ApplicantResumePoco() {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                    Resume = request.Resume,
                    LastUpdated = request.LastUpdated is null ? DateTime.MinValue : Convert.ToDateTime(request.LastUpdated)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteApplicantResume(request, context);
        }
    }
}
