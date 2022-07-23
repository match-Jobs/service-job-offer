namespace job_offer.JobOffers.Common.DomainModel.Entities
{
    public class Error
    {
        public string Message { get; private set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}