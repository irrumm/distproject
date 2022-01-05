using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class addresschanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Addresses",
                newName: "MachineLocation");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GameInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "ServiceProvider",
                table: "Addresses",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceProvider",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "MachineLocation",
                table: "Addresses",
                newName: "Info");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GameInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "Addresses",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
