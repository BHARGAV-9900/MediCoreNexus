namespace MediCore.Domain.Enums;

public enum PaymentMethod : byte
{
    Cash = 1,
    CreditCard = 2,
    DebitCard = 3,
    UPI = 4,
    NetBanking = 5,
    Insurance = 6
}