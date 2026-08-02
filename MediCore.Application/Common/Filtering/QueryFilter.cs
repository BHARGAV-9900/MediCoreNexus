namespace MediCore.Application.Common.Filtering;

public class QueryFilter
{
    public string? Search { get; set; }

    public string? SortBy { get; set; }

    public string? SortDirection { get; set; } = "asc";
}