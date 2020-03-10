using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;        

        public SecurityLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("login/{id}")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(SecurityLoginPoco))]
        public ActionResult GetSecurityLogin(Guid id)
        {
            SecurityLoginPoco poco = _logic.Get(id);
            if (poco == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(poco);
            }
        }

        //Get All
        [HttpGet]
        [Route("login")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginPoco>))]
        public ActionResult GetAllSecurityLogin()
        {
            List<SecurityLoginPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(pocos);
            }

        }

        //Post
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPost]
        [Route("login")]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Add(securityLoginPocos);
            return Ok();
        }

        //Put
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Update(securityLoginPocos);
            return Ok();
        }

        //Delete
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Delete(securityLoginPocos);
            return Ok();
        }

    }
}