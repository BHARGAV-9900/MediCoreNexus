using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.CreateLaboratoryOrder;

public class CreateLaboratoryOrderCommand : IRequest<int>
{
    public int AppointmentId { get; set; }

    public int LaboratoryTestId { get; set; }
}