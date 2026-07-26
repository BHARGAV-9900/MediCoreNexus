using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandHandler
    : IRequestHandler<CreatePrescriptionCommand, int>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public CreatePrescriptionCommandHandler(
        IPrescriptionRepository prescriptionRepository,
        IAppointmentRepository appointmentRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<int> Handle(
        CreatePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        // Ensure appointment exists
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.AppointmentId,
            cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                $"Appointment with Id {request.AppointmentId} was not found.");

        // Ensure only one prescription per appointment
        var exists =
            await _prescriptionRepository.ExistsForAppointmentAsync(
                request.AppointmentId,
                cancellationToken);

        if (exists)
            throw new ConflictException(
                "A prescription already exists for this appointment.");

        var prescription = new Prescription(
            request.AppointmentId,
            request.Instructions,
            request.Notes);

        await _prescriptionRepository.AddAsync(
            prescription,
            cancellationToken);

        await _prescriptionRepository.SaveChangesAsync(
            cancellationToken);

        return prescription.Id;
    }
}