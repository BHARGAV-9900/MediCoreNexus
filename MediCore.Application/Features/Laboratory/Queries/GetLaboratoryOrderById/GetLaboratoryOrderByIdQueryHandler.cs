using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetLaboratoryOrderById;

public class GetLaboratoryOrderByIdQueryHandler
    : IRequestHandler<
        GetLaboratoryOrderByIdQuery,
        LaboratoryOrderDto>
{
    private readonly ILaboratoryOrderRepository _repository;

    public GetLaboratoryOrderByIdQueryHandler(
        ILaboratoryOrderRepository repository)
    {
        _repository = repository;
    }


    public async Task<LaboratoryOrderDto> Handle(
        GetLaboratoryOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var order =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);


        if (order is null)
        {
            throw new KeyNotFoundException(
                "Laboratory order not found.");
        }


        return new LaboratoryOrderDto
        {
            Id = order.Id,

            PublicId = order.PublicId,


            // Appointment

            AppointmentId =
                order.AppointmentId,

            AppointmentPublicId =
                order.Appointment!.PublicId,

            AppointmentDate =
                order.Appointment.AppointmentDate,


            // Patient

            PatientId =
                order.Appointment.PatientId,

            PatientName =
                $"{order.Appointment.Patient!.FirstName} " +
                $"{order.Appointment.Patient.LastName}",


            // Doctor

            DoctorId =
                order.Appointment.DoctorId,

            DoctorName =
                $"{order.Appointment.Doctor!.FirstName} " +
                $"{order.Appointment.Doctor.LastName}",


            // Laboratory Test

            LaboratoryTestId =
                order.LaboratoryTestId,

            LaboratoryTestPublicId =
                order.LaboratoryTest!.PublicId,

            LaboratoryTestName =
                order.LaboratoryTest.Name,

            LaboratoryTestPrice =
                order.LaboratoryTest.Price
        };
    }
}