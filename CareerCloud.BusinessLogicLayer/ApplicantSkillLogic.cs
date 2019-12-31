using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {

        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            //implement the exception rules 101,102,103,104
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                if(poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, "Oops there is no 13th month!"));
                }
                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, "Oops there is no 13th month!"));
                }
                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, "Start Year cannot be less than 1900"));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, "End Year cannot be less than Start Year"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            //make sure poco is clean, doesn't have exceptions
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            //make sure poco is clean, doesn't have exceptions
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
