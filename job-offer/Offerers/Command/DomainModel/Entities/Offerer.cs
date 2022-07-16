using System;
using job_offer.Offerers.Command.DomainModel.ValueObjects;

namespace job_offer.Offerers.Command.DomainModel.Entities
{
    public class Offerer
    {
        public virtual OffererId OffererId { get; protected set; }
        public virtual string CodOfferer { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime UpdatedAt { get; protected set; }

        protected Offerer()
        {
        }

        protected Offerer(
            OffererId offererId,
            string codOfferer,
            bool active,
            DateTime createdAt,
            DateTime updatedAt)
        {
            OffererId = offererId;
            CodOfferer = codOfferer;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Offerer From(
            OffererId offererId,
            string codOfferer,
            bool active,
            DateTime createdAt,
            DateTime updatedAt)
        {
            return new Offerer(
                offererId,
                codOfferer,
                active,
                createdAt, 
                updatedAt);
        }

        public static Offerer NonExisting()
        {
            OffererId customerId = OffererId.FromExisting(null);
            DateTime Now = DateTime.Now;
            return new Offerer(
                customerId, 
                null,
                true, 
                Now, 
                Now);
        }

        public virtual bool DoesNotExist()
        {
            return OffererId == null || !OffererId.Ok();
        }

        public virtual bool Exist()
        {
            return OffererId != null && OffererId.Ok();
        }
    }
}