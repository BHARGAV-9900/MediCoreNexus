using MediCore.Domain.Entities;
using System.Numerics;

namespace MediCore.UnitTests.Domain;

public class DoctorTests
{
    [Fact]
    public void Constructor_ShouldCreateDoctor()
    {
        var doctor = new Doctor(
            " John ",
            " Smith ",
            "john.smith@example.com",
            "9876543210",
            "Cardiologist",
            10,
            1500m,
            1);

        Assert.Equal("John", doctor.FirstName);
        Assert.Equal("Smith", doctor.LastName);
        Assert.Equal(
            "john.smith@example.com",
            doctor.Email);

        Assert.Equal("9876543210", doctor.PhoneNumber);
        Assert.Equal("Cardiologist", doctor.Specialization);
        Assert.Equal(10, doctor.ExperienceYears);
        Assert.Equal(1500m, doctor.ConsultationFee);
        Assert.Equal(1, doctor.DepartmentId);
        Assert.True(doctor.IsAvailable);
        Assert.False(doctor.IsDeleted);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyFirstName()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Doctor(
                "",
                "Smith",
                "john@example.com",
                "9876543210",
                "Cardiologist",
                10,
                1500m,
                1));

        Assert.Equal(
            "First name is required.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyLastName()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Doctor(
                "John",
                "",
                "john@example.com",
                "9876543210",
                "Cardiologist",
                10,
                1500m,
                1));

        Assert.Equal(
            "Last name is required.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyEmail()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Doctor(
                "John",
                "Smith",
                "",
                "9876543210",
                "Cardiologist",
                10,
                1500m,
                1));

        Assert.Equal(
            "Email is required.",
            exception.Message);
    }

    [Fact]
    public void SetAvailability_ShouldChangeAvailability()
    {
        var doctor = new Doctor(
            "John",
            "Smith",
            "john@example.com",
            "9876543210",
            "Cardiologist",
            10,
            1500m,
            1);

        doctor.SetAvailability(false);

        Assert.False(doctor.IsAvailable);

        doctor.SetAvailability(true);

        Assert.True(doctor.IsAvailable);
    }

    [Fact]
    public void Delete_ShouldSoftDeleteDoctor()
    {
        var doctor = new Doctor(
            "John",
            "Smith",
            "john@example.com",
            "9876543210",
            "Cardiologist",
            10,
            1500m,
            1);

        doctor.Delete();

        Assert.True(doctor.IsDeleted);
        Assert.NotNull(doctor.DeletedAt);
    }
}