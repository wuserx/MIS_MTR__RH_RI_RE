public record Document
{
    public string? Series { get; set; }
    public string? Number { get; set; }
    public DateOnly? IssueDate { get; set; }
    public string? Type { get; set; }
    public string? Issuer { get; set; }
    public string? BirthPlace { get; set; }
    public DateOnly? BirthDay { get; set; }
    public string? Citizenship { get; set; }
    public string? Status { get; set; }
    public string? Gender { get; set; }
}
