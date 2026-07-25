using MediatR;
using MediCore.Application.Features.Doctors.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Doctors.Queries.GetAllDoctors;

public class GetAllDoctorsQueryHandler
    : IRequestHandler<GetAllDoctorsQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(
        GetAllDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        var doctors = await _doctorRepository.GetAllAsync(cancellationToken);

        return doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Email = d.Email,
            PhoneNumber = d.PhoneNumber,
            Specialization = d.Specialization,
            ExperienceYears = d.ExperienceYears,
            ConsultationFee = d.ConsultationFee,
            IsAvailable = d.IsAvailable,
            DepartmentId = d.DepartmentId,
            DepartmentName = d.Department?.Name ?? string.Empty
        });

    }
}