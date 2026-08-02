using MediCore.Application.Features.Dashboard.Queries.GetDashboard;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Persistence.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync(
        CancellationToken cancellationToken)
    {
        var today = DateTime.Today;

        return new DashboardDto
        {
            TotalPatients = await _context.Patients
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            TotalDoctors = await _context.Doctors
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            TotalDepartments = await _context.Departments
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            TotalAppointments = await _context.Appointments
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            TodayAppointments = await _context.Appointments
                .CountAsync(
                    x => !x.IsDeleted &&
                    x.AppointmentDate.Date == today,
                    cancellationToken),

            TotalMedicines = await _context.Medicines
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            PendingBills = await _context.Bills
                .CountAsync(
                    x => !x.IsDeleted &&
                    !x.IsPaid,
                    cancellationToken),

            PaidBills = await _context.Bills
                .CountAsync(
                    x => !x.IsDeleted &&
                    x.IsPaid,
                    cancellationToken),

            TotalRevenue = await _context.Bills
                .Where(x => !x.IsDeleted && x.IsPaid)
                .SumAsync(
                    x => (decimal?)x.TotalAmount,
                    cancellationToken) ?? 0,

            TotalLaboratoryOrders = await _context.LaboratoryOrders
                .CountAsync(x => !x.IsDeleted, cancellationToken),

            TotalPrescriptions = await _context.Prescriptions
                .CountAsync(x => !x.IsDeleted, cancellationToken)
        };
    }
}