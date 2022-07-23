using job_offer.JobOffers.Common.DomainModel.Entities;

namespace job_offer.JobOffers.Command.DomainModel.ValueObjects
{
    public class JobOfferId : Identity
    {
        protected JobOfferId()
        {
        }

        private JobOfferId(string referencedId) : base(referencedId)
        {
        }

        public static JobOfferId FromExisting(string referencedId)
        {
            return new JobOfferId(referencedId);
        }
    }
}