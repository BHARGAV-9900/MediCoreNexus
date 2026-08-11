namespace MediCore.Application.Features.Reports;

public class DashboardReportDto
{
    public int TotalPatients { get; set; }

    public int TotalDoctors { get; set; }

    public int TotalAppointments { get; set; }

    public int ScheduledAppointments { get; set; }

    public int CompletedAppointments { get; set; }

    public int CancelledAppointments { get; set; }

    public int NoShowAppointments { get; set; }

    public int TotalBills { get; set; }

    public decimal TotalRevenue { get; set; }

    public int TotalPayments { get; set; }

    public int TotalInventoryItems { get; set; }

    public int LowStockItems { get; set; }

    public int ExpiringInventoryItems { get; set; }
}