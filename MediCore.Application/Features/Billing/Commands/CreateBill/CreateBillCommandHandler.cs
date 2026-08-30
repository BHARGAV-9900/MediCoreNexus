using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Application.Interfaces.Services;
using MediCore.Domain.Entities;
using MediCore.Domain.Enums;

namespace MediCore.Application.Features.Billing.Commands.CreateBill;

public class CreateBillCommandHandler
    : IRequestHandler<CreateBillCommand, int>
{
    private readonly IBillRepository _billRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly INotificationService _notificationService;

    public CreateBillCommandHandler(
        IBillRepository billRepository,
        IAppointmentRepository appointmentRepository,
        INotificationService notificationService)
    {
        _billRepository = billRepository;
        _appointmentRepository = appointmentRepository;
        _notificationService = notificationService;
    }

    public async Task<int> Handle(
        CreateBillCommand request,
        CancellationToken cancellationToken)
    {
        // Ensure appointment exists
        var appointment = await _appointmentRepository.GetByIdAsync(
            request.AppointmentId,
            cancellationToken);

        if (appointment is null)
            throw new NotFoundException(
                $"Appointment with Id {request.AppointmentId} was not found.");

        // Ensure only one bill exists
        var exists = await _billRepository.ExistsForAppointmentAsync(
            request.AppointmentId,
            cancellationToken);

        if (exists)
            throw new ConflictException(
                "A bill already exists for this appointment.");

        var bill = new Bill(
            request.AppointmentId,
            request.TotalAmount);

        await _billRepository.AddAsync(
            bill,
            cancellationToken);

        await _billRepository.SaveChangesAsync(
            cancellationToken);

        // Notify Administrator and Receptionist after the bill is successfully created.
        await _notificationService.NotifyRolesAsync(
            new[]
            {
                UserRole.Administrator,
                UserRole.Receptionist
            },
            "New Bill Created",
            $"A new bill has been created for Appointment #{request.AppointmentId} with a total amount of {request.TotalAmount:0.00}.",
            "Billing",
            cancellationToken);

        return bill.Id;
    }
}