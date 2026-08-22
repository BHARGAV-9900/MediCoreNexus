using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IDepartmentRepository departmentRepository)
    {
        _doctorRepository = doctorRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(
        UpdateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(
                $"Doctor with Id {request.Id} was not found.");
        }

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

        if (await _doctorRepository.ExistsByEmailExceptIdAsync(
                normalizedEmail,
                request.Id,
                cancellationToken))
        {
            throw new ConflictException(
                "A doctor with this email already exists.");
        }

        if (await _doctorRepository.ExistsByPhoneNumberExceptIdAsync(
                normalizedPhoneNumber,
                request.Id,
                cancellationToken))
        {
            throw new ConflictException(
                "A doctor with this phone number already exists.");
        }

        doctor.Update(
            request.FirstName,
            request.LastName,
            normalizedEmail,
            normalizedPhoneNumber,
            request.Specialization,
            request.ExperienceYears,
            request.ConsultationFee,
            request.DepartmentId);

        await _doctorRepository.SaveChangesAsync(
            cancellationToken);
    }
}
