using MediatR;

namespace MediCore.Application.Features.Settings.Queries.GetSettings;

public record GetSettingsQuery
    : IRequest<SettingsDto>;