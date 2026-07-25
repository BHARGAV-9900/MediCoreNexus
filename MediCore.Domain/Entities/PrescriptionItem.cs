using MediCore.Domain.Common;

namespace MediCore.Domain.Entities;

public class PrescriptionItem : BaseAuditableEntity
{
    private PrescriptionItem()
    {
    }

    public PrescriptionItem(
        int prescriptionId,
        int medicineId,
        string dosage,
        string frequency,
        int durationInDays,
        int quantity)
    {
        SetPrescription(prescriptionId);
        SetMedicine(medicineId);

        Dosage = dosage;
        Frequency = frequency;
        DurationInDays = durationInDays;
        Quantity = quantity;
    }

    public int PrescriptionId { get; private set; }

    public int MedicineId { get; private set; }

    public string Dosage { get; private set; } = string.Empty;

    public string Frequency { get; private set; } = string.Empty;

    public int DurationInDays { get; private set; }

    public int Quantity { get; private set; }

    public Prescription? Prescription { get; private set; }

    public Medicine? Medicine { get; private set; }
    

    private void SetPrescription(int prescriptionId)
    {
        if (prescriptionId <= 0)
            throw new ArgumentException("Invalid prescription.");

        PrescriptionId = prescriptionId;
    }

    private void SetMedicine(int medicineId)
    {
        if (medicineId <= 0)
            throw new ArgumentException("Invalid medicine.");

        MedicineId = medicineId;
    }
}