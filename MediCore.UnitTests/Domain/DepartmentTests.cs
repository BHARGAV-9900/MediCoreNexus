using MediCore.Domain.Entities;

namespace MediCore.UnitTests.Domain;

public class DepartmentTests
{
    [Fact]
    public void Constructor_ShouldCreateActiveDepartment()
    {
        var department = new Department(
            " Cardiology ",
            "Heart related treatments");

        Assert.Equal("Cardiology", department.Name);
        Assert.Equal("Heart related treatments", department.Description);
        Assert.True(department.IsActive);
        Assert.False(department.IsDeleted);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyName()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Department(""));

        Assert.Equal(
            "Department name cannot be empty.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldRejectNameLongerThan100Characters()
    {
        var name = new string('A', 101);

        var exception = Assert.Throws<ArgumentException>(() =>
            new Department(name));

        Assert.Equal(
            "Department name cannot exceed 100 characters.",
            exception.Message);
    }

    [Fact]
    public void Rename_ShouldUpdateDepartmentName()
    {
        var department = new Department("Cardiology");

        department.Rename("Neurology");

        Assert.Equal("Neurology", department.Name);
    }

    [Fact]
    public void Delete_ShouldSoftDeleteDepartment()
    {
        var department = new Department("Cardiology");

        department.Delete();

        Assert.True(department.IsDeleted);
        Assert.NotNull(department.DeletedAt);
    }

    [Fact]
    public void Deactivate_ShouldMakeDepartmentInactive()
    {
        var department = new Department("Cardiology");

        department.Deactivate();

        Assert.False(department.IsActive);
    }

    [Fact]
    public void Activate_ShouldMakeDepartmentActive()
    {
        var department = new Department("Cardiology");

        department.Deactivate();
        department.Activate();

        Assert.True(department.IsActive);
    }
}