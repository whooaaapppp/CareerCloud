using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;
        

        public CompanyProfileService()
        {

            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());

        }

        public override Task<CompanyProfilePayload> ReadCompanyProfile(IdRequestCompanyProfile request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<CompanyProfilePayload>(
                () => new CompanyProfilePayload()
                {
                    Id = poco.Id.ToString(),
                    RegistrationDate = Timestamp.FromDateTime(poco.RegistrationDate),
                    CompanyWebsite = poco.CompanyWebsite,
                    ContactPhone = poco.ContactPhone,
                    ContactName = poco.ContactName,
                    CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)
                }
                ) ;
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new CompanyProfilePoco[] { new CompanyProfilePoco() {
                Id = new Guid(request.Id),
                    RegistrationDate = Convert.ToDateTime(request.RegistrationDate),
                    CompanyWebsite = request.CompanyWebsite,
                    ContactPhone = request.ContactPhone,
                    ContactName = request.ContactName,
                    CompanyLogo = request.CompanyLogo.ToByteArray()
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateCompanyProfile(request, context);
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new CompanyProfilePoco[] { new CompanyProfilePoco() {
                Id = new Guid(request.Id),
                    RegistrationDate = Convert.ToDateTime(request.RegistrationDate),
                    CompanyWebsite = request.CompanyWebsite,
                    ContactPhone = request.ContactPhone,
                    ContactName = request.ContactName,
                    CompanyLogo = request.CompanyLogo.ToByteArray()
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteCompanyProfile(request, context);
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new CompanyProfilePoco[] { new CompanyProfilePoco() {
                Id = new Guid(request.Id),
                    RegistrationDate = Convert.ToDateTime(request.RegistrationDate),
                    CompanyWebsite = request.CompanyWebsite,
                    ContactPhone = request.ContactPhone,
                    ContactName = request.ContactName,
                    CompanyLogo = request.CompanyLogo.ToByteArray()
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }


            return base.UpdateCompanyProfile(request, context);
        }

    }
}
