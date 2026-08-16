namespace MediCore.Application.Features.Dashboard.Queries.GetDashboard;

public class DashboardDto
{
    public int TotalPatients { get; set; }

    public int TotalDoctors { get; set; }

    public int TotalDepartments { get; set; }

    public int TotalAppointments { get; set; }

    public int TodayAppointments { get; set; }

    public int TotalMedicines { get; set; }

    public int LowStockMedicines { get; set; }

    public int PendingBills { get; set; }

    public int PaidBills { get; set; }

    public decimal TotalRevenue { get; set; }

    public int TotalLaboratoryOrders { get; set; }

    public int TotalPrescriptions { get; set; }
}