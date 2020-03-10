using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;        

        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);


        }

        //Get on ID
        [HttpGet]
        [Route("resume/{id}")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(ApplicantResumePoco))]
        public ActionResult GetApplicantResume(Guid id)
        {
            ApplicantResumePoco poco = _logic.Get(id);
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
        [Route("resume")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(List<ApplicantResumePoco>))]
        public ActionResult GetAllApplicantResume()
        {
            List<ApplicantResumePoco> pocos = _logic.GetAll();
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
        [Route("resume")]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Add(applicantResumePocos);
            return Ok();
        }

        //Put
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPut]
        [Route("resume")]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Update(applicantResumePocos);
            return Ok();
        }

        //Delete
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpDelete]
        [Route("resume")]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Delete(applicantResumePocos);
            return Ok();
        }




    }
}