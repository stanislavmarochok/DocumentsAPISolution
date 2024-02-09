using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DocumentsAPI.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        [XmlIgnore]
        [JsonIgnore]
        public object ActualData { get; set; }

        public string Data
        {
            get => JsonConvert.SerializeObject(ActualData);
            set => ActualData = JsonConvert.DeserializeObject(value);
        }
    }
}
