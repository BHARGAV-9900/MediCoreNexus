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
        var department = await _departmentRepository.GetByIdAsync(
            request.DepartmentId,
            cancellationToken);

        if (department is null)
        {
            throw new NotFoundException(
                $"Department with Id {request.DepartmentId} was not found.");
        }

        var normalizedEmail = request.Email.Trim().ToLower();
        var normalizedPhoneNumber = request.PhoneNumber.Trim();

        // Check both duplicate conditions before throwing so the user
        // receives the most accurate message when both values already exist.
        var emailExists = await _doctorRepository.ExistsByEmailAsync(
            normalizedEmail,
            cancellationToken);

        var phoneExists = await _doctorRepository.ExistsByPhoneNumberAsync(
            normalizedPhoneNumber,
            cancellationToken);

        // Both email and phone already exist.
        if (emailExists && phoneExists)
        {
            throw new ConflictException(
                "A doctor with this email and phone number already exists.");
        }

        // Only email already exists.
        if (emailExists)
        {
            throw new ConflictException(
                "A doctor with this email already exists.");
        }

        // Only phone number already exists.
        if (phoneExists)
        {
            throw new ConflictException(
                "A doctor with this phone number already exists.");
        }

        var doctor = new Doctor(
            request.FirstName,
            request.LastName,
            normalizedEmail,
            normalizedPhoneNumber,
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