using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryOrder;

public class CreateLaboratoryOrderCommandHandler
    : IRequestHandler<CreateLaboratoryOrderCommand, int>
{
    private readonly ILaboratoryOrderRepository _laboratoryOrderRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILaboratoryTestRepository _laboratoryTestRepository;

    public CreateLaboratoryOrderCommandHandler(
        ILaboratoryOrderRepository laboratoryOrderRepository,
        IAppointmentRepository appointmentRepository,
        ILaboratoryTestRepository laboratoryTestRepository)
    {
        _laboratoryOrderRepository = laboratoryOrderRepository;
        _appointmentRepository = appointmentRepository;
        _laboratoryTestRepository = laboratoryTestRepository;
    }

    public async Task<int> Handle(
        CreateLaboratoryOrderCommand request,
        CancellationToken cancellationToken)
    {
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

        if (exists)
            throw new ConflictException(
                "This laboratory test has already been ordered for this appointment.");

        var laboratoryOrder = new LaboratoryOrder(
            request.AppointmentId,
            request.LaboratoryTestId);

        await _laboratoryOrderRepository.AddAsync(
            laboratoryOrder,
            cancellationToken);

        await _laboratoryOrderRepository.SaveChangesAsync(
            cancellationToken);

        return laboratoryOrder.Id;
    }
}