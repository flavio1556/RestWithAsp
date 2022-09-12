using RestWithASPNET.HyperMedia;
using RestWithASPNET.HyperMedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithASPNET.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("code")]
        public long Id { get; set; }
        [JsonPropertyName("Name")]
        public string FirstName { get; set; }

        [JsonPropertyName("Last_Name")]
        public string LastName { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }
        [JsonPropertyName("Sex")]
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
