using job_offer.JobOffers.Command.DomainModel.ValueObjects;
using job_offer.JobOffers.Command.DomainModel.Enums;
using job_offer.Offerers.Command.DomainModel.ValueObjects;
using job_offer.Common.DomainModel.ValueObjects;
using System;
using job_offer.JobOffers.DomainModel.ValueObjects;
using job_offer.Employers.Command.DomainModel.ValueObjects;

namespace job_offer.JobOffers.Command.DomainModel.Entities
{
    public class JobOffer
    {
        public virtual JobOfferId JobOfferId { get; protected set; }
        public virtual string CodOffer { get; protected set; }

        public virtual EmployerId EmployerId { get; protected set; }

        public virtual Detail OfferDetail { get; protected set; }
        public virtual Vacancie Vacancie { get; protected set; }

        public virtual Money Remuneration { get; protected set; }
        public virtual JobOfferStateId JobOfferStateId { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime UpdatedAt { get; protected set; }


        protected JobOffer()
        {
        }

        public JobOffer(
            JobOfferId jobOfferId,
            string codOffer,
            EmployerId employerId,
            Detail offerDetail,
            Vacancie vacancie,
            Money remuneration,
            JobOfferStateId jobOfferStateId,
            DateTime createdAt,
            DateTime updatedAt
            )
        {
            JobOfferId = jobOfferId;
            CodOffer = codOffer;
            EmployerId = employerId;
            OfferDetail = offerDetail;
            Vacancie = vacancie;
            Remuneration = remuneration;
            JobOfferStateId = jobOfferStateId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static JobOffer NonExisting()
        {
            DateTime Now = DateTime.Now;
            JobOfferId jobOfferId = JobOfferId.FromExisting(null);
            EmployerId employerId = EmployerId.FromExisting(null);
            return new JobOffer(
                jobOfferId,
                null,
                employerId,
                null,
                null,
                null,
                JobOfferStateId.NULL,
                Now,
                Now);
        }

        public virtual bool DoesNotExist()
        {
            return JobOfferId == null || !JobOfferId.Ok();
        }

        public virtual bool Exist()
        {
            return JobOfferId != null && JobOfferId.Ok();
        }

        public virtual void Close()
        {
            JobOfferStateId = JobOfferStateId.CLOSED;
        }

        public virtual bool Available()
        {
           return JobOfferStateId == JobOfferStateId.PUBLICATED && !Vacancie.CompleteVacancies();
        }



        public virtual void ChangeUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}