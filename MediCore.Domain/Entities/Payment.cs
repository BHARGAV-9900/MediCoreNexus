using MediCore.Domain.Common;
using MediCore.Domain.Enums;

namespace MediCore.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    private Payment()
    {
    }


    public Payment(
        int billId,
        decimal amount,
        PaymentMethod paymentMethod)
    {
        SetBill(billId);
        SetAmount(amount);

        PaymentMethod = paymentMethod;

        PaidOn = DateTime.UtcNow;
    }


    // =========================================================
    // Properties
    // =========================================================

    public int BillId { get; private set; }

    public decimal Amount { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public DateTime PaidOn { get; private set; }


    // =========================================================
    // Navigation Property
    // =========================================================

    public Bill? Bill { get; private set; }


    // =========================================================
    // Validation
    // =========================================================

    private void SetBill(
        int billId)
    {
        if (billId <= 0)
            throw new ArgumentException(
                "Invalid bill.");

        BillId = billId;
    }


    private void SetAmount(
        decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException(
                "Payment amount must be greater than zero.");

        Amount = amount;
    }


    // =========================================================
    // Update Payment
    // =========================================================

    public void Update(
        decimal amount,
        PaymentMethod paymentMethod)
    {
        SetAmount(amount);

        PaymentMethod = paymentMethod;

        UpdatedAt = DateTime.UtcNow;
    }


    // =========================================================
    // Soft Delete
    // =========================================================

    public void Delete()
    {
        IsDeleted = true;

        DeletedAt = DateTime.UtcNow;

        UpdatedAt = DateTime.UtcNow;
    }
}