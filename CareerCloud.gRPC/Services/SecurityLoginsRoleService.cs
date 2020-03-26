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
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;
        

        public SecurityLoginsRoleService()
        {

            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());

        }

        public override Task<SecurityLoginsRolePayload> ReadSecurityLoginsRole(IdRequestSecurityLoginsRole request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("Poco is null");

            }
            return new Task<SecurityLoginsRolePayload>(
                () => new SecurityLoginsRolePayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    Role = poco.Role.ToString()
                    
        }
                ) ;
        }

        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Add(new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.CreateSecurityLoginsRole(request, context);
        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Update(new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.UpdateSecurityLoginsRole(request, context);
        }

        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            try
            {
                _logic.Delete(new SecurityLoginsRolePoco[] { new SecurityLoginsRolePoco() {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)
            } });
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();

            }

            return base.DeleteSecurityLoginsRole(request, context);
        }
    }
}
