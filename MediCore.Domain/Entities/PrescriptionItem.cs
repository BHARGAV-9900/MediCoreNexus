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

        SetDosage(dosage);
        SetFrequency(frequency);
        SetDuration(durationInDays);
        SetQuantity(quantity);
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
    private void SetDosage(string dosage)
    {
        if (string.IsNullOrWhiteSpace(dosage))
            throw new ArgumentException("Dosage is required.");

        Dosage = dosage.Trim();
    }
    private void SetFrequency(string frequency)
    {
        if (string.IsNullOrWhiteSpace(frequency))
            throw new ArgumentException("Frequency is required.");

        Frequency = frequency.Trim();
    }
    private void SetDuration(int durationInDays)
    {
        if (durationInDays <= 0)
            throw new ArgumentException("Duration must be greater than zero.");

        DurationInDays = durationInDays;
    }
    private void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Quantity = quantity;
    }
    public void Update(
        string dosage,
        string frequency,
        int durationInDays,
        int quantity)
    {
        SetDosage(dosage);
        SetFrequency(frequency);
        SetDuration(durationInDays);
        SetQuantity(quantity);

        UpdatedAt = DateTime.UtcNow;
    }
    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}