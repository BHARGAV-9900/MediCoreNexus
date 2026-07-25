using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandHandler
    : IRequestHandler<CreateDoctorCommand, int>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IDepartmentRepository departmentRepository)
    {
        _doctorRepository = doctorRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<int> Handle(
        CreateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        // Verify Department Exists
        // Verify Department Exists
        var department = await _departmentRepository.GetByIdAsync(
            request.DepartmentId,
            cancellationToken);

        if (department is null)
        {
            throw new NotFoundException(
                $"Department with Id {request.DepartmentId} was not found.");
        }

        // Verify Email is Unique
        // Verify Email is Unique
        if (await _doctorRepository.ExistsByEmailAsync(
                request.Email,
                cancellationToken))
        {
            throw new ConflictException(
                "A doctor with this email already exists.");
        }

        var doctor = new Doctor(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Specialization,
            request.ExperienceYears,
            request.ConsultationFee,
            request.DepartmentId);

        await _doctorRepository.AddAsync(
            doctor,
            cancellationToken);

        await _doctorRepository.SaveChangesAsync(
            cancellationToken);

        return doctor.Id;
    }
}