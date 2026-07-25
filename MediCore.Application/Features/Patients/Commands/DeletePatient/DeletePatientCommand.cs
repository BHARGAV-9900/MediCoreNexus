using MediatR;

namespace MediCore.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommand : IRequest
{
    public int Id { get; set; }
}