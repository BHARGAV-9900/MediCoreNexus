using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore.Persistence.Migrations;

public partial class AddAuditLogs : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AuditLogs",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: true),
                UserEmail = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                EntityName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                EntityPublicId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                RequestPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                RequestId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditLogs", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_Action",
            table: "AuditLogs",
            column: "Action");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_EntityName",
            table: "AuditLogs",
            column: "EntityName");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_OccurredAtUtc",
            table: "AuditLogs",
            column: "OccurredAtUtc");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_UserId",
            table: "AuditLogs",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogs_PublicId",
            table: "AuditLogs",
            column: "PublicId",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditLogs");
    }
}
