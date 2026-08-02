namespace MediCore.Application.Common.Pagination;

public class PatientFilterRequest
{
    public string? Search { get; set; }

    public string? Gender { get; set; }

    public string? BloodGroup { get; set; }

    public bool? IsActive { get; set; }

    public string? SortBy { get; set; } = "CreatedAt";

    public string? SortDirection { get; set; } = "desc";
}