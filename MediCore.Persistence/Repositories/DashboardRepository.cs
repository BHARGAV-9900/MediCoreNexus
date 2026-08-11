using Microsoft.EntityFrameworkCore;
using MediCore.Application.Features.Dashboard.Queries.GetDashboard;
using MediCore.Application.Interfaces.Repositories;
using MediCore.Persistence.Context;

namespace MediCore.Persistence.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync(
        CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.Date;

        // -----------------------------
        // PATIENTS
        // -----------------------------

        var totalPatients = await _context.Patients
            .CountAsync(cancellationToken);


        // -----------------------------
        // DOCTORS
        // -----------------------------

        var totalDoctors = await _context.Doctors
            .CountAsync(cancellationToken);


        // -----------------------------
        // DEPARTMENTS
        // -----------------------------

        var totalDepartments = await _context.Departments
            .CountAsync(cancellationToken);


        // -----------------------------
        // APPOINTMENTS
        // -----------------------------

        var totalAppointments = await _context.Appointments
            .CountAsync(cancellationToken);


        // -----------------------------
        // TODAY'S APPOINTMENTS
        // -----------------------------

        var todayAppointments = await _context.Appointments
            .CountAsync(
                a => a.AppointmentDate.Date == today,
                cancellationToken);


        // -----------------------------
        // MEDICINES
        // -----------------------------

        var totalMedicines = await _context.Medicines
            .CountAsync(cancellationToken);


        // -----------------------------
        // BILLS
        // -----------------------------

        var totalBills = await _context.Bills
            .CountAsync(cancellationToken);


        // -----------------------------
        // PAID BILLS
        // -----------------------------

        var paidBills = await _context.Payments
            .Select(p => p.BillId)
            .Distinct()
            .CountAsync(cancellationToken);


        // -----------------------------
        // PENDING BILLS
        // -----------------------------

        var pendingBills = totalBills - paidBills;


        // -----------------------------
        // TOTAL REVENUE
        // -----------------------------

        var totalRevenue = await _context.Payments
            .SumAsync(
                p => (decimal?)p.Amount,
                cancellationToken)
            ?? 0;


        // -----------------------------
        // LABORATORY ORDERS
        // -----------------------------

        var totalLaboratoryOrders = await _context.LaboratoryOrders
            .CountAsync(cancellationToken);


        // -----------------------------
        // PRESCRIPTIONS
        // -----------------------------

        var totalPrescriptions = await _context.Prescriptions
            .CountAsync(cancellationToken);


        // -----------------------------
        // RESULT
        // -----------------------------

        return new DashboardDto
        {
            TotalPatients = totalPatients,

            TotalDoctors = totalDoctors,

            TotalDepartments = totalDepartments,

            TotalAppointments = totalAppointments,

            TodayAppointments = todayAppointments,

            TotalMedicines = totalMedicines,

            PendingBills = pendingBills,

            PaidBills = paidBills,

            TotalRevenue = totalRevenue,

            TotalLaboratoryOrders = totalLaboratoryOrders,

            TotalPrescriptions = totalPrescriptions
        };
    }
}