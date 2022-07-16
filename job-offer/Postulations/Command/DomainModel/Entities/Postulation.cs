using job_offer.Postulations.Command.DomainModel.ValueObjects;
using job_offer.Postulations.Command.DomainModel.Enums;
using job_offer.Common.DomainModel.ValueObjects;
using System;
using job_offer.JobOffers.Command.DomainModel.ValueObjects;
using job_offer.Offerers.Command.DomainModel.ValueObjects;

namespace job_offer.Postulations.Command.DomainModel.Entities
{
    public class Postulation
    {
        public virtual PostulationId PostulationId { get; protected set; }
        public virtual JobOfferId JobOfferId { get; protected set; }
        public virtual OffererId OffererId { get; protected set; }
        public virtual PostulationStateId PostulationStateId { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime UpdatedAt { get; protected set; }


        protected Postulation()
        {
        }

        public Postulation(
            PostulationId postulationId,
            JobOfferId jobOfferId,
            OffererId offererId,
            PostulationStateId postulationStateId,
            DateTime createdAt,
            DateTime updatedAt
            )
        {
            PostulationId = postulationId;
            JobOfferId = jobOfferId;
            OffererId = offererId;
            PostulationStateId = postulationStateId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Postulation NonExisting()
        {
            DateTime Now = DateTime.Now;
            PostulationId postulationId = PostulationId.FromExisting(null);
            JobOfferId jobOfferId = JobOfferId.FromExisting(null);
            OffererId offererId = OffererId.FromExisting(null);
            return new Postulation(
                postulationId,
                jobOfferId,
                offererId,
                PostulationStateId.NULL,
                Now,
                Now);
        }

        public virtual bool DoesNotExist()
        {
            return PostulationId == null || !PostulationId.Ok();
        }

        public virtual bool Exist()
        {
            return PostulationId != null && PostulationId.Ok();
        }

        public virtual void Send()
        {
            PostulationStateId = PostulationStateId.PENDING;
        }
        public virtual void Rejec()
        {
            PostulationStateId = PostulationStateId.REJECTED;
        }
        public virtual void ChangeUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}