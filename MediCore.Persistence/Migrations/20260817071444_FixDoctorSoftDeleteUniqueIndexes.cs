using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctorSoftDeleteUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Email",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PhoneNumber",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PhoneNumber",
                table: "Doctors",
                column: "PhoneNumber",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Email",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_PhoneNumber",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PhoneNumber",
                table: "Doctors",
                column: "PhoneNumber",
                unique: true);
        }
    }
}
