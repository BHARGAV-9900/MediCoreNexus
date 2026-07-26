using MediatR;

namespace MediCore.Application.Features.Prescriptions.Commands.DeletePrescription;

public record DeletePrescriptionCommand(int Id) : IRequest<bool>;