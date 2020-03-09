using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _logic;        

        public ApplicantProfileController()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repo);


        }

        //Get on ID
        [HttpGet]
        [Route("profile/{id}")]
        public ActionResult GetApplicantProfile(Guid id)
        {
            ApplicantProfilePoco poco = _logic.Get(id);
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
        [Route("profile")]
        public ActionResult GetAllApplicantProfile()
        {
            List<ApplicantProfilePoco> pocos = _logic.GetAll();
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
        [Route("profile")]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePocos)
        {
            _logic.Add(applicantProfilePocos);
            return Ok();
        }

        //Put
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpPut]
        [Route("profile")]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePocos)
        {
            _logic.Update(applicantProfilePocos);
            return Ok();
        }

        //Delete
        //To force Web API to read a simple type from the request body, add the[FromBody] attribute to the parameter
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api
        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePocos)
        {
            _logic.Delete(applicantProfilePocos);
            return Ok();
        }




    }
}