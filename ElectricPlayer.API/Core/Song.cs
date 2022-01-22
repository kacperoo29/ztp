using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ElectricPlayer.API.Core
{
    public class Song
    {
        public string? Path { get; set; }
        
        [JsonIgnore]
        [XmlIgnore]
        public Metadata? Metadata { get; set; }
    }
}