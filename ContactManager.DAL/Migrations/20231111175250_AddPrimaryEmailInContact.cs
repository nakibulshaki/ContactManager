using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryEmailInContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryEmail",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                column: "PrimaryEmail",
                value: "test1@test.com");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                column: "PrimaryEmail",
                value: "test3@test.com");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                column: "PrimaryEmail",
                value: "test2@test.com");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PrimaryEmail",
                table: "Contacts",
                column: "PrimaryEmail",
                unique: true,
                filter: "[PrimaryEmail] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_PrimaryEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PrimaryEmail",
                table: "Contacts");
        }
    }
}
