using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorDictionary.Infrastructure.Persistence.Migrations
{
    public partial class Address_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAdress",
                schema: "dbo",
                table: "user",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "OldEmailAdress",
                schema: "dbo",
                table: "emailconfirmation",
                newName: "OldEmailAddress");

            migrationBuilder.RenameColumn(
                name: "NewEmailAdress",
                schema: "dbo",
                table: "emailconfirmation",
                newName: "NewEmailAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                schema: "dbo",
                table: "user",
                newName: "EmailAdress");

            migrationBuilder.RenameColumn(
                name: "OldEmailAddress",
                schema: "dbo",
                table: "emailconfirmation",
                newName: "OldEmailAdress");

            migrationBuilder.RenameColumn(
                name: "NewEmailAddress",
                schema: "dbo",
                table: "emailconfirmation",
                newName: "NewEmailAdress");
        }
    }
}
