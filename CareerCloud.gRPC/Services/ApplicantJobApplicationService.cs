using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        public override Task<ApplicantJobApplicationPayload> ReadApplicantJobApplication(IdRequestApplicantJobApplication request, ServerCallContext context)
        {

            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");
            }

            return new Task<ApplicantJobApplicationPayload>(
                () => new ApplicantJobApplicationPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Job = poco.Job.ToString(),
                    ApplicationDate = Timestamp.FromDateTime((DateTime)poco.ApplicationDate)
                }

                );
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Job = new Guid(request.Job),
                    ApplicationDate = Convert.ToDateTime(request.ApplicationDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateApplicantJobApplication(request, context);
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Job = new Guid(request.Job),
                    ApplicationDate = Convert.ToDateTime(request.ApplicationDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateApplicantJobApplication(request, context);
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new ApplicantJobApplicationPoco[] { new ApplicantJobApplicationPoco() {
                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Job = new Guid(request.Job),
                    ApplicationDate = Convert.ToDateTime(request.ApplicationDate)

            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteApplicantJobApplication(request, context);
        }
    }
}
