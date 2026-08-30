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
        // Dashboard counts should match the active records shown
        // in the Patients module. Soft-deleted records are excluded.

        var totalPatients = await _context.Patients
            .CountAsync(
                p => !p.IsDeleted,
                cancellationToken);

        // -----------------------------
        // DOCTORS
        // -----------------------------

        var totalDoctors = await _context.Doctors
            .CountAsync(
                d => !d.IsDeleted,
                cancellationToken);

        // -----------------------------
        // DEPARTMENTS
        // -----------------------------

        var totalDepartments = await _context.Departments
            .CountAsync(
                d => !d.IsDeleted,
                cancellationToken);

        // -----------------------------
        // APPOINTMENTS
        // -----------------------------

        var totalAppointments = await _context.Appointments
            .CountAsync(
                a => !a.IsDeleted,
                cancellationToken);

        // -----------------------------
        // TODAY'S APPOINTMENTS
        // -----------------------------

        var todayAppointments = await _context.Appointments
            .CountAsync(
                a =>
                    !a.IsDeleted &&
                    a.AppointmentDate.Date == today,
                cancellationToken);

        // -----------------------------
        // MEDICINES
        // -----------------------------

        var totalMedicines = await _context.Medicines
            .CountAsync(
                m => !m.IsDeleted,
                cancellationToken);

        // -----------------------------
        // LOW STOCK MEDICINES
        // -----------------------------

        var lowStockMedicines = await _context.Inventories
            .CountAsync(
                i =>
                    !i.IsDeleted &&
                    i.IsActive &&
                    i.QuantityInStock <= i.MinimumStockLevel,
                cancellationToken);

        // -----------------------------
        // BILLS
        // -----------------------------
        // A bill remains active until it is paid or otherwise voided/deleted.
        // A partially paid bill is still pending and must remain in the
        // pending count.

        var activeBills = _context.Bills
            .Where(b => !b.IsDeleted);

        var totalBills = await activeBills
            .CountAsync(cancellationToken);

        // -----------------------------
        // PAID BILLS
        // -----------------------------
        // A bill is paid only when its payment total reaches the bill's
        // total amount. The Bill domain entity maintains IsPaid based on
        // the accumulated payments.

        var paidBills = await activeBills
            .CountAsync(
                b => b.IsPaid,
                cancellationToken);

        // -----------------------------
        // PENDING BILLS
        // -----------------------------
        // Includes both unpaid and partially paid active bills.

        var pendingBills = await activeBills
            .CountAsync(
                b => !b.IsPaid,
                cancellationToken);

        // -----------------------------
        // TOTAL REVENUE
        // -----------------------------
        // Revenue is calculated only from active payments belonging
        // to active bills. Partial payments are included in revenue.

        var totalRevenue = await _context.Payments
            .Where(p =>
                !p.IsDeleted &&
                p.Bill != null &&
                !p.Bill.IsDeleted)
            .SumAsync(
                p => (decimal?)p.Amount,
                cancellationToken)
            ?? 0;

        // -----------------------------
        // LABORATORY ORDERS
        // -----------------------------

        var totalLaboratoryOrders = await _context.LaboratoryOrders
            .CountAsync(
                o => !o.IsDeleted,
                cancellationToken);

        // -----------------------------
        // PRESCRIPTIONS
        // -----------------------------

        var totalPrescriptions = await _context.Prescriptions
            .CountAsync(
                p => !p.IsDeleted,
                cancellationToken);

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

            LowStockMedicines = lowStockMedicines,

            PendingBills = pendingBills,

            PaidBills = paidBills,

            TotalRevenue = totalRevenue,

            TotalLaboratoryOrders = totalLaboratoryOrders,

            TotalPrescriptions = totalPrescriptions
        };
    }
}