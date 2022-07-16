using job_offer.Common.DomainModel.Entities;

namespace job_offer.Employers.Command.DomainModel.ValueObjects
{
    public class EmployerId : Identity
    {
        protected EmployerId()
        {
        }

        private EmployerId(string referencedId) : base(referencedId)
        {
        }

        public static EmployerId FromExisting(string referencedId)
        {
            return new EmployerId(referencedId);
        }
    }
}
