using MediatR;
using MediCore.Application.Features.Doctors.DTOs;

namespace MediCore.Application.Features.Doctors.Queries.GetAllDoctors;

public record GetAllDoctorsQuery : IRequest<IEnumerable<DoctorDto>>;