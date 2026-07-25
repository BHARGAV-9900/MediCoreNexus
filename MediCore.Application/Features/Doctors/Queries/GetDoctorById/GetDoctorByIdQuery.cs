using MediatR;
using MediCore.Application.Features.Doctors.DTOs;

namespace MediCore.Application.Features.Doctors.Queries.GetDoctorById;

public record GetDoctorByIdQuery(int Id) : IRequest<DoctorDto>;