using MediatR;
using MediCore.Application.Exceptions;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Entities;

namespace MediCore.Application.Features.Billing.Commands.CreateBill;

public class CreateBillCommandHandler
    : IRequestHandler<CreateBillCommand, int>
{
    private readonly IBillRepository _billRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public CreateBillCommandHandler(
        IBillRepository billRepository,
        IAppointmentRepository appointmentRepository)
    {
        _billRepository = billRepository;
        _appointmentRepository = appointmentRepository;
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

        return bill.Id;
    }
}