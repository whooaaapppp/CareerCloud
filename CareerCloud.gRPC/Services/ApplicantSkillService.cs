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
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillService()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        public override Task<ApplicantSkillPayload> ReadApplicantSkill(IdRequestApplicantSkill request, ServerCallContext context)
        {


            ApplicantSkillPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<ApplicantSkillPayload>(
                () => new ApplicantSkillPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                }
                );
        }

        public override Task<Empty> CreateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new ApplicantSkillPoco[] { new ApplicantSkillPoco() {
                

                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    StartMonth = (byte)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (byte)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateApplicantSkill(request, context);
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new ApplicantSkillPoco[] { new ApplicantSkillPoco() {


                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    StartMonth = (byte)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (byte)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateApplicantSkill(request, context);
        }

        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new ApplicantSkillPoco[] { new ApplicantSkillPoco() {


                    Id = new Guid(request.Id),
                    Applicant = new Guid(request.Applicant),
                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    StartMonth = (byte)request.StartMonth,
                    StartYear = request.StartYear,
                    EndMonth = (byte)request.EndMonth,
                    EndYear = request.EndYear
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteApplicantSkill(request, context);
        }
    }
}
