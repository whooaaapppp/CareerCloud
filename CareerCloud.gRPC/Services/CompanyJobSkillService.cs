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
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {

            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());

        }

        public override Task<CompanyJobSkillPayload> ReadCompanyJobSkill(IdRequestCompanyJobSkill request, ServerCallContext context)
        {


            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyJobSkillPayload>(
                () => new CompanyJobSkillPayload()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    Importance = poco.Importance,
                }
                );
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyJobSkillPoco[] { new CompanyJobSkillPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),
                   
                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    Importance = request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateCompanyJobSkill(request, context);
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyJobSkillPoco[] { new CompanyJobSkillPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),

                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    Importance = request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateCompanyJobSkill(request, context);
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyJobSkillPoco[] { new CompanyJobSkillPoco() {
                Id = new Guid(request.Id),
                    Job = new Guid(request.Job),

                    Skill = request.Skill,
                    SkillLevel = request.SkillLevel,
                    Importance = request.Importance,
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteCompanyJobSkill(request, context);
        }
    }
}
