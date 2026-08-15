using MediatR;
using MediCore.Application.Interfaces.Repositories;

namespace MediCore.Application.Features.Laboratory.Queries.GetAllLaboratoryOrders;

public class GetAllLaboratoryOrdersQueryHandler
    : IRequestHandler<
        GetAllLaboratoryOrdersQuery,
        IEnumerable<LaboratoryOrderDto>>
{
    private readonly ILaboratoryOrderRepository _repository;

    public GetAllLaboratoryOrdersQueryHandler(
        ILaboratoryOrderRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<LaboratoryOrderDto>> Handle(
        GetAllLaboratoryOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders =
            await _repository.GetAllAsync(
                cancellationToken);


        return orders.Select(o => new LaboratoryOrderDto
        {
            Id = o.Id,

            PublicId = o.PublicId,


            // Appointment

            AppointmentId =
                o.AppointmentId,

            AppointmentPublicId =
                o.Appointment!.PublicId,

            AppointmentDate =
                o.Appointment.AppointmentDate,


            // Patient

            PatientId =
                o.Appointment.PatientId,

            PatientName =
                $"{o.Appointment.Patient!.FirstName} " +
                $"{o.Appointment.Patient.LastName}",


            // Doctor

            DoctorId =
                o.Appointment.DoctorId,

            DoctorName =
                $"{o.Appointment.Doctor!.FirstName} " +
                $"{o.Appointment.Doctor.LastName}",


            // Laboratory Test

            LaboratoryTestId =
                o.LaboratoryTestId,

            LaboratoryTestPublicId =
                o.LaboratoryTest!.PublicId,

            LaboratoryTestName =
                o.LaboratoryTest.Name,

            LaboratoryTestPrice =
                o.LaboratoryTest.Price
        });
    }
}