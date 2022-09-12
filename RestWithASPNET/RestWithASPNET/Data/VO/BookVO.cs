using RestWithASPNET.HyperMedia;
using RestWithASPNET.HyperMedia.Abstract;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithASPNET.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        [JsonPropertyName("Code")]
        public long Id { get; set; }
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("Author")]

        public string Author { get; set; }
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        [JsonPropertyName("LaunchDate")]
        public DateTime LaunchDate { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink> { };
    }
}
