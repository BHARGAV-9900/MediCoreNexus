using MediatR;

namespace MediCore.Application.Features.Doctors.Commands.CreateDoctor;

public sealed record CreateDoctorCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Specialization,
    int ExperienceYears,
    decimal ConsultationFee,
    int DepartmentId
) : IRequest<int>;