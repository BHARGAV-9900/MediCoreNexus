using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryResult;

public class CreateLaboratoryResultCommandHandler
    : IRequestHandler<CreateLaboratoryResultCommand, int>
{
    private readonly ILaboratoryResultRepository _resultRepository;
    private readonly ILaboratoryOrderRepository _orderRepository;
    private readonly INotificationService _notificationService;

    public CreateLaboratoryResultCommandHandler(
        ILaboratoryResultRepository resultRepository,
        ILaboratoryOrderRepository orderRepository,
        INotificationService notificationService)
    {
        _resultRepository = resultRepository;
        _orderRepository = orderRepository;
        _notificationService = notificationService;
    }

    public async Task<int> Handle(
        CreateLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.LaboratoryOrderId,
            cancellationToken);

        if (order is null)
            throw new NotFoundException(
                $"Laboratory order with Id {request.LaboratoryOrderId} was not found.");

        var exists = await _resultRepository.ExistsByLaboratoryOrderAsync(
            request.LaboratoryOrderId,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                "A laboratory result already exists for this laboratory order.");

        var laboratoryResult = new LaboratoryResult(
            request.LaboratoryOrderId,
            request.Result,
            request.Remarks);

        await _resultRepository.AddAsync(
            laboratoryResult,
            cancellationToken);

        await _resultRepository.SaveChangesAsync(
            cancellationToken);

        var patientName = order.Appointment?.Patient is not null
            ? $"{order.Appointment.Patient.FirstName} {order.Appointment.Patient.LastName}"
            : $"Patient #{order.Appointment?.PatientId}";

        var doctorName = order.Appointment?.Doctor is not null
            ? $"Dr. {order.Appointment.Doctor.FirstName} {order.Appointment.Doctor.LastName}"
            : $"Doctor #{order.Appointment?.DoctorId}";

        await _notificationService.NotifyRolesAsync(
            new[]
            {
                UserRole.Administrator,
                UserRole.Receptionist
            },
            "Laboratory Result Available",
            $"The laboratory result for {patientName} with {doctorName} is now available. Laboratory Order #{request.LaboratoryOrderId}.",
            "Laboratory",
            cancellationToken);

        return laboratoryResult.Id;
    }
}