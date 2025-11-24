
public record PersonData
{
    public string? ExternalRequestId { get; set; }
    public string? Oip { get; set; }
    public List<Policy> Policies { get; set; } = new();
    public List<Document> Documents { get; set; } = new();
    public List<Attachment> Attachments { get; set; } = new();
    public List<SnilsInfo> SnilsList { get; set; } = new();
}
