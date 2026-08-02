namespace MediCore.Application.Common.Filtering;

public class PatientFilter : QueryFilter
{
    public string? Gender { get; set; }

    public string? BloodGroup { get; set; }

    public bool? IsActive { get; set; }
}