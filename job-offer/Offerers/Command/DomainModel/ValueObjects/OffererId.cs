using job_offer.Common.DomainModel.Entities;

namespace job_offer.Offerers.Command.DomainModel.ValueObjects
{
    public class OffererId : Identity
    {
        protected OffererId()
        {
        }

        private OffererId(string referencedId) : base(referencedId)
        {
        }

        public static OffererId FromExisting(string referencedId)
        {
            return new OffererId(referencedId);
        }
    }
}
