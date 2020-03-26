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
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService()
        {

            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());

        }

        public override Task<CompanyLocationPayload> ReadCompanyLocation(IdRequestCompanyLocation request, ServerCallContext context)
        {


            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyLocationPayload>(
                () => new CompanyLocationPayload()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    CountryCode = poco.CountryCode,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                }
                );
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco() {
                Id = new Guid(request.Id),
                    
                    Company = new Guid(request.Company),
                    CountryCode = request.CountryCode,
                    Province = request.Province,
                    Street = request.Street,
                    City = request.City,
                    PostalCode =request.PostalCode
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.CreateCompanyLocation(request, context);
        }

        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyLocationPoco[] { new CompanyLocationPoco() {
                Id = new Guid(request.Id),

                    Company = new Guid(request.Company),
                    CountryCode = request.CountryCode,
                    Province = request.Province,
                    Street = request.Street,
                    City = request.City,
                    PostalCode =request.PostalCode
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteCompanyLocation(request, context);
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() {
                Id = new Guid(request.Id),

                    Company = new Guid(request.Company),
                    CountryCode = request.CountryCode,
                    Province = request.Province,
                    Street = request.Street,
                    City = request.City,
                    PostalCode =request.PostalCode
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateCompanyLocation(request, context);
        }
    }
}
