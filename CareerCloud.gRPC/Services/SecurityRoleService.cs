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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;
        

        public SecurityRoleService()
        {

            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());

        }

        public override Task<SecurityRolePayload> ReadSecurityRole(IdRequestSecurityRole request, ServerCallContext context)
        {
            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SecurityRolePayload>(
                () => new SecurityRolePayload()
                {
                    Id = poco.Id.ToString(),
                    Role = poco.Role,
                    IsInactive = poco.IsInactive
                    
        }
                ) ;
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new SecurityRolePoco[] { new SecurityRolePoco() {
                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateSecurityRole(request, context);
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new SecurityRolePoco[] { new SecurityRolePoco() {
                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.UpdateSecurityRole(request, context);
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new SecurityRolePoco[] { new SecurityRolePoco() {
                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }
            return base.DeleteSecurityRole(request, context);
        }
    }
}
