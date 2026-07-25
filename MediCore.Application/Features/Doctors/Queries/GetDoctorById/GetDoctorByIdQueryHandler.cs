using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Features.Doctors.DTOs;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryHandler
    : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorDto> Handle(
        GetDoctorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(
                $"Doctor with Id {request.Id} was not found.");
        }

        return new DoctorDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            Specialization = doctor.Specialization,
            ExperienceYears = doctor.ExperienceYears,
            ConsultationFee = doctor.ConsultationFee,
            IsAvailable = doctor.IsAvailable,
            DepartmentId = doctor.DepartmentId,
            DepartmentName = doctor.Department?.Name ?? string.Empty
        };
    }
}