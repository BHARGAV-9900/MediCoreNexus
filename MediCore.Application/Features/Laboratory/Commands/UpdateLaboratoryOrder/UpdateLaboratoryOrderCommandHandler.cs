using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryOrder;

public class UpdateLaboratoryOrderCommandHandler
    : IRequestHandler<UpdateLaboratoryOrderCommand, bool>
{
    private readonly ILaboratoryOrderRepository _laboratoryOrderRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILaboratoryTestRepository _laboratoryTestRepository;

    public UpdateLaboratoryOrderCommandHandler(
        ILaboratoryOrderRepository laboratoryOrderRepository,
        IAppointmentRepository appointmentRepository,
        ILaboratoryTestRepository laboratoryTestRepository)
    {
        _laboratoryOrderRepository = laboratoryOrderRepository;
        _appointmentRepository = appointmentRepository;
        _laboratoryTestRepository = laboratoryTestRepository;
    }

    public async Task<bool> Handle(
        UpdateLaboratoryOrderCommand request,
        CancellationToken cancellationToken)
    {
        var laboratoryOrder = await _laboratoryOrderRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (laboratoryOrder is null)
            throw new NotFoundException(
                $"Laboratory order with Id {request.Id} was not found.");

        var appointment = await _appointmentRepository.GetByIdAsync(
            request.AppointmentId,
            cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                $"Appointment with Id {request.AppointmentId} was not found.");

        var laboratoryTest = await _laboratoryTestRepository.GetByIdAsync(
            request.LaboratoryTestId,
            cancellationToken);

        if (laboratoryTest is null)
            throw new NotFoundException(
                $"Laboratory test with Id {request.LaboratoryTestId} was not found.");

        var exists = await _laboratoryOrderRepository.ExistsAsync(
            request.AppointmentId,
            request.LaboratoryTestId,
            cancellationToken);

        if (exists &&
            (laboratoryOrder.AppointmentId != request.AppointmentId ||
             laboratoryOrder.LaboratoryTestId != request.LaboratoryTestId))
        {
            throw new ConflictException(
                "This laboratory test has already been ordered for this appointment.");
        }

        laboratoryOrder.Update(
            request.AppointmentId,
            request.LaboratoryTestId);

        await _laboratoryOrderRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}