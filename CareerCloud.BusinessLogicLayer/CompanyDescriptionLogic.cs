using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {

        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            //implement the exception rules 106,107
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
                if (poco.CompanyName.Length < 3)
                {
                    exceptions.Add(new ValidationException(106, "Company Name must be more than 2 characters!"));
                }
                if (poco.CompanyDescription.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, "Company Description must be more than 2 characters!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            //make sure poco is clean, doesn't have exceptions
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            //make sure poco is clean, doesn't have exceptions
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
