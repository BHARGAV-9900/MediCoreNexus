using MediatR;

namespace MediCore.Application.Features.PrescriptionItems.Commands.DeletePrescriptionItem;

public record DeletePrescriptionItemCommand(int Id) : IRequest<bool>;