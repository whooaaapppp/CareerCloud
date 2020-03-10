using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;        

        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);


        }

        //Get on ID
        [HttpGet]
        [Route("jobeducation/{id}")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(CompanyJobEducationPoco))]
        public ActionResult GetCompanyJobEducation(Guid id)
        {
            CompanyJobEducationPoco poco = _logic.Get(id);
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
        [Route("jobeducation")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(List<CompanyJobEducationPoco>))]
        public ActionResult GetAllCompanyJobEducation()
        {
            List<CompanyJobEducationPoco> pocos = _logic.GetAll();
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
        [Route("jobeducation")]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Add(companyJobEducationPocos);
            return Ok();
        }

        //Put
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPut]
        [Route("jobeducation")]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Update(companyJobEducationPocos);
            return Ok();
        }

        //Delete
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpDelete]
        [Route("jobeducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Delete(companyJobEducationPocos);
            return Ok();
        }

    }
}