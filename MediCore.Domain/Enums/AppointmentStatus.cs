namespace MediCore.Domain.Enums;

public enum AppointmentStatus : byte
{
    Scheduled = 1,
    CheckedIn = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    NoShow = 6
}