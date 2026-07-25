using MediatR;

namespace MediCore.Application.Features.Doctors.Commands.UpdateDoctor;

public sealed record UpdateDoctorCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    int ExperienceYears,
    decimal ConsultationFee,
    int DepartmentId
) : IRequest;