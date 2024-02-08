namespace DocumentsAPI.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public object Data { get; set; }
    }
}
