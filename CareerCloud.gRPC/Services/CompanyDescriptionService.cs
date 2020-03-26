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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {

            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());

        }

        public override Task<CompanyDescriptionPayload> ReadCompanyDescription(IdRequestCompanyDescription request, ServerCallContext context)
        {


            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyDescriptionPayload>(
                () => new CompanyDescriptionPayload()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    LanguageId = poco.LanguageId,
                    CompanyName = poco.CompanyName,
                    CompanyDescription = poco.CompanyDescription
                    
                }
                );
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                   LanguageId = request.LanguageId,
                    CompanyName = request.CompanyName,
                    CompanyDescription = request.CompanyDescription
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateCompanyDescription(request, context);
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                   LanguageId = request.LanguageId,
                    CompanyName = request.CompanyName,
                    CompanyDescription = request.CompanyDescription
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
            }


                return base.UpdateCompanyDescription(request, context);
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                   LanguageId = request.LanguageId,
                    CompanyName = request.CompanyName,
                    CompanyDescription = request.CompanyDescription
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
            }
                

                return base.DeleteCompanyDescription(request, context);
        }
    }
}
