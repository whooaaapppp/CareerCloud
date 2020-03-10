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
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;        

        public CompanyJobsDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("jobsdescription/{id}")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(CompanyJobDescriptionPoco))]
        public ActionResult GetCompanyJobsDescription(Guid id)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(id);
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
        [Route("jobsdescription")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(List<CompanyJobDescriptionPoco>))]
        public ActionResult GetAllCompanyJobsDescription()
        {
            List<CompanyJobDescriptionPoco> pocos = _logic.GetAll();
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
        [Route("jobsdescription")]
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Add(companyJobDescriptionPocos);
            return Ok();
        }

        //Put
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPut]
        [Route("jobsdescription")]
        public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Update(companyJobDescriptionPocos);
            return Ok();
        }

        //Delete
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpDelete]
        [Route("jobsdescription")]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Delete(companyJobDescriptionPocos);
            return Ok();
        }

    }
}