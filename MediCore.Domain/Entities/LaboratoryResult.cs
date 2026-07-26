using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class LaboratoryResult : BaseAuditableEntity
{
    private LaboratoryResult()
    {
    }

    public LaboratoryResult(
        int laboratoryOrderId,
        string result,
        string? remarks = null)
    {
        SetLaboratoryOrder(laboratoryOrderId);
        SetResult(result);
        SetRemarks(remarks);
    }

    public int LaboratoryOrderId { get; private set; }

    public string Result { get; private set; } = string.Empty;

    public string? Remarks { get; private set; }

    public LaboratoryOrder? LaboratoryOrder { get; private set; }
    private void SetLaboratoryOrder(int laboratoryOrderId)
    {
        if (laboratoryOrderId <= 0)
            throw new ArgumentException("Invalid laboratory order.");

        LaboratoryOrderId = laboratoryOrderId;
    }
    private void SetResult(string result)
    {
        if (string.IsNullOrWhiteSpace(result))
            throw new ArgumentException("Laboratory result is required.");

        Result = result.Trim();
    }
    private void SetRemarks(string? remarks)
    {
        Remarks = string.IsNullOrWhiteSpace(remarks)
            ? null
            : remarks.Trim();
    }
    public void Update(
        string result,
        string? remarks)
    {
        SetResult(result);
        SetRemarks(remarks);

        UpdatedAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}