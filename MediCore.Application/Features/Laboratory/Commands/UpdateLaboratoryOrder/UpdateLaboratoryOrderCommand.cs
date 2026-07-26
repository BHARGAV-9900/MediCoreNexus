using MediatR;

namespace MediCore.Application.Features.Laboratory.Commands.UpdateLaboratoryOrder;

public class UpdateLaboratoryOrderCommand : IRequest<bool>
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int LaboratoryTestId { get; set; }
}