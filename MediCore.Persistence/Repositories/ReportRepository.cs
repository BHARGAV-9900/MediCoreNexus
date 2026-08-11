using MediCore.Application.Features.Reports;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Domain.Enums;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class ReportRepository
    : IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardReportDto> GetDashboardReportAsync(
        CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;

        var expiryLimit = today.AddDays(30);

        var totalPatients =
            await _context.Patients
                .AsNoTracking()
                .CountAsync(
                    x => !x.IsDeleted,
                    cancellationToken);

        var totalDoctors =
            await _context.Doctors
                .AsNoTracking()
                .CountAsync(
                    x => !x.IsDeleted,
                    cancellationToken);

        var totalAppointments =
            await _context.Appointments
                .AsNoTracking()
                .CountAsync(
                    x => !x.IsDeleted,
                    cancellationToken);

        var scheduledAppointments =
            await _context.Appointments
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.Status ==
                        AppointmentStatus.Scheduled,
                    cancellationToken);

        var completedAppointments =
            await _context.Appointments
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.Status ==
                        AppointmentStatus.Completed,
                    cancellationToken);

        var cancelledAppointments =
            await _context.Appointments
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.Status ==
                        AppointmentStatus.Cancelled,
                    cancellationToken);

        var noShowAppointments =
            await _context.Appointments
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.Status ==
                        AppointmentStatus.NoShow,
                    cancellationToken);

        var totalBills =
            await _context.Bills
                .AsNoTracking()
                .CountAsync(
                    x => !x.IsDeleted,
                    cancellationToken);

        var totalRevenue =
            await _context.Payments
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .SumAsync(
                    x => (decimal?)x.Amount,
                    cancellationToken)
                ?? 0m;

        var totalPayments =
            await _context.Payments
                .AsNoTracking()
                .CountAsync(
                    x => !x.IsDeleted,
                    cancellationToken);

        var totalInventoryItems =
            await _context.Inventories
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.IsActive,
                    cancellationToken);

        var lowStockItems =
            await _context.Inventories
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.IsActive &&
                        x.QuantityInStock <=
                        x.MinimumStockLevel,
                    cancellationToken);

        var expiringInventoryItems =
            await _context.Inventories
                .AsNoTracking()
                .CountAsync(
                    x =>
                        !x.IsDeleted &&
                        x.IsActive &&
                        x.ExpiryDate.Date >= today &&
                        x.ExpiryDate.Date <= expiryLimit,
                    cancellationToken);

        return new DashboardReportDto
        {
            TotalPatients =
                totalPatients,

            TotalDoctors =
                totalDoctors,

            TotalAppointments =
                totalAppointments,

            ScheduledAppointments =
                scheduledAppointments,

            CompletedAppointments =
                completedAppointments,

            CancelledAppointments =
                cancelledAppointments,

            NoShowAppointments =
                noShowAppointments,

            TotalBills =
                totalBills,

            TotalRevenue =
                totalRevenue,

            TotalPayments =
                totalPayments,

            TotalInventoryItems =
                totalInventoryItems,

            LowStockItems =
                lowStockItems,

            ExpiringInventoryItems =
                expiringInventoryItems
        };
    }
}