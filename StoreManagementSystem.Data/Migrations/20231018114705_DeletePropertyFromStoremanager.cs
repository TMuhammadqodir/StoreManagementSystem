using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletePropertyFromStoremanager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "StoreManagers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "StoreManagers");

            migrationBuilder.DropColumn(
                name: "TelNumber",
                table: "StoreManagers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "StoreManagers",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "StoreManagers",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "TelNumber",
                table: "StoreManagers",
                type: "longtext",
                nullable: false);
        }
    }
}
