using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePatientPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber");
        }
    }
}
