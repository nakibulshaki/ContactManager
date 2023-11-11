using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryEmailAddressInContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_PrimaryEmail",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PrimaryEmail",
                table: "Contacts");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId1",
                table: "EmailAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "EmailAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryEmailAddressId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                column: "PrimaryEmailAddressId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                column: "PrimaryEmailAddressId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                column: "PrimaryEmailAddressId",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("3a406f64-ad7b-4098-ab01-7e93aae2b851"),
                columns: new[] { "ContactId1", "IsPrimary" },
                values: new object[] { null, true });

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("3ddeb084-5e5d-4eca-b275-e4f6886e04dc"),
                columns: new[] { "ContactId1", "IsPrimary" },
                values: new object[] { null, true });

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("5111f412-a7f4-4169-bb27-632687569ccd"),
                columns: new[] { "ContactId1", "IsPrimary" },
                values: new object[] { null, true });

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("d1a50413-20c0-4972-a351-8be24e1fc939"),
                columns: new[] { "ContactId1", "IsPrimary" },
                values: new object[] { null, false });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_ContactId1",
                table: "EmailAddresses",
                column: "ContactId1",
                unique: true,
                filter: "[ContactId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PrimaryEmailAddressId",
                table: "Contacts",
                column: "PrimaryEmailAddressId",
                unique: true,
                filter: "[PrimaryEmailAddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddresses_Contacts_ContactId1",
                table: "EmailAddresses",
                column: "ContactId1",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddresses_Contacts_ContactId1",
                table: "EmailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddresses_ContactId1",
                table: "EmailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PrimaryEmailAddressId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "EmailAddresses");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "EmailAddresses");

            migrationBuilder.DropColumn(
                name: "PrimaryEmailAddressId",
                table: "Contacts");

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
    }
}
