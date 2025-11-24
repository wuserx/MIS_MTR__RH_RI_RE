using System.Xml.Serialization;

public class Pagination
{
    public int? PageNumber { get; set; }

    public int? ItemPerPage { get; set; }

    public int? Count { get; set; }
}