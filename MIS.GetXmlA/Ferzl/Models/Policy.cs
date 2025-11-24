public record Policy
{
    public string? Enp { get; set; }
    public DateOnly pcyDateB { get; set; }
    public DateOnly? pcyDateT { get; set; }          
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? InsurerName { get; set; }
    public string? InsurerOgrn { get; set; }
    public string? Category { get; set; }
    public string? Surname { get; set; }
    public string? FirstName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? BirthDay { get; set; }
    public string? Gender { get; set; }
}
