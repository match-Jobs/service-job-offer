using System;
using job_offer.Employers.Command.DomainModel.ValueObjects;
using job_offer.Offerers.Command.DomainModel.ValueObjects;

namespace job_offer.Employers.Command.DomainModel.Entities
{
    public class Employer
    {
        public virtual EmployerId EmployerId { get; protected set; }
        public virtual string CodEmployer { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime UpdatedAt { get; protected set; }

        protected Employer()
        {
        }

        protected Employer(
            EmployerId employerId,
            string codEmployer,
            bool active,
            DateTime createdAt,
            DateTime updatedAt)
        {
            EmployerId = employerId;
            CodEmployer = codEmployer;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Employer From(
            EmployerId employerId,
            string codEmployer,
            bool active,
            DateTime createdAt,
            DateTime updatedAt)
        {
            return new Employer(
                employerId,
                codEmployer,
                active,
                createdAt, 
                updatedAt);
        }

        public static Employer NonExisting()
        {
            EmployerId employerId = EmployerId.FromExisting(null);
            DateTime Now = DateTime.Now;
            return new Employer(
                employerId, 
                null,
                true, 
                Now, 
                Now);
        }

        public virtual bool DoesNotExist()
        {
            return EmployerId == null || !EmployerId.Ok();
        }

        public virtual bool Exist()
        {
            return EmployerId != null && EmployerId.Ok();
        }
    }
}