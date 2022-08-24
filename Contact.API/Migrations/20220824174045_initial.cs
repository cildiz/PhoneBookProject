using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    InformationType = table.Column<int>(type: "integer", nullable: false),
                    InformationContent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_ContactInformations_Persons_PersonUUID",
                        column: x => x.PersonUUID,
                        principalTable: "Persons",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_PersonUUID",
                table: "ContactInformations",
                column: "PersonUUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
