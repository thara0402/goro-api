using Newtonsoft.Json;

namespace Goro.Api.Infrastructure.Models
{
    public abstract class DocumentBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string Etag { get; set; }
    }

}