using Newtonsoft.Json;

namespace ArtistManagement.WebApi.Infrastructure
{
    public class ValidationErrorStateModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public object Message { get; }

        public ValidationErrorStateModel(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
