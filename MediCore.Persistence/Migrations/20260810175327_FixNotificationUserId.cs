using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixNotificationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        DECLARE @constraintName NVARCHAR(128);

        SELECT @constraintName = fk.name
        FROM sys.foreign_keys fk
        INNER JOIN sys.foreign_key_columns fkc
            ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns c
            ON fkc.parent_object_id = c.object_id
            AND fkc.parent_column_id = c.column_id
        WHERE fk.parent_object_id = OBJECT_ID(N'Notifications')
          AND c.name = N'UserId';

        IF @constraintName IS NOT NULL
        BEGIN
            EXEC(
                N'ALTER TABLE [Notifications] DROP CONSTRAINT [' 
                + @constraintName + N']'
            );
        END
        """);

            migrationBuilder.Sql("""
        DECLARE @indexName NVARCHAR(128);

        SELECT TOP 1 @indexName = i.name
        FROM sys.indexes i
        INNER JOIN sys.index_columns ic
            ON i.object_id = ic.object_id
            AND i.index_id = ic.index_id
        INNER JOIN sys.columns c
            ON ic.object_id = c.object_id
            AND ic.column_id = c.column_id
        WHERE i.object_id = OBJECT_ID(N'Notifications')
          AND c.name = N'UserId'
          AND i.is_primary_key = 0
          AND i.is_unique_constraint = 0;

        IF @indexName IS NOT NULL
        BEGIN
            EXEC(
                N'DROP INDEX [' 
                + @indexName 
                + N'] ON [Notifications]'
            );
        END
        """);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
