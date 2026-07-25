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
        LaboratoryOrderId = laboratoryOrderId;
        Result = result;
        Remarks = remarks;
    }

    public int LaboratoryOrderId { get; private set; }

    public string Result { get; private set; } = string.Empty;

    public string? Remarks { get; private set; }

    public LaboratoryOrder? LaboratoryOrder { get; private set; }
}