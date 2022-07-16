using Newtonsoft.Json;

namespace job_offer.Common.Infra.API
{
    public class ApiStringResponse
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public ApiStringResponse(string message)
        {
            Message = message;
        }
    }
}