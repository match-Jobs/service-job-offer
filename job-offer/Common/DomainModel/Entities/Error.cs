namespace job_offer.Common.DomainModel.Entities
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