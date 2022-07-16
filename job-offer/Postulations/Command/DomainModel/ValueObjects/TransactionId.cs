using job_offer.Common.DomainModel.Entities;

namespace job_offer.Postulations.Command.DomainModel.ValueObjects
{
    public class PostulationId : Identity
    {
        protected PostulationId()
        {
        }

        private PostulationId(string referencedId) : base(referencedId)
        {
        }

        public static PostulationId FromExisting(string referencedId)
        {
            return new PostulationId(referencedId);
        }
    }
}