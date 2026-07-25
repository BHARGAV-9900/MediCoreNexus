namespace MediCore.Application.Features.Departments.Queries.GetAllDepartments;

public class DepartmentDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}