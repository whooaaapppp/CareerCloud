using System;

namespace CareerCloud.Pocos
{
    public class ApplicantEducationPoco
    {
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        public string Major { get; set; }
        public string  CertificateDiploma { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public Byte CompletionPercent { get; set; }
        public Byte[] TimeStamp { get; set; }
    }
}
