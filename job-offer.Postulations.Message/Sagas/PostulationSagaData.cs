using NServiceBus;

namespace job_offer.Postulations.Messages.Sagas
{
    public class PostulationSagaData : ContainSagaData
    {
        public virtual string PostulationId { get;  set; }
        public virtual string JobOfferId { get;  set; }
        public virtual string OffererId { get;  set; }
    }
}