using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TyMiniProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeeTableAndProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FeeTables");

            migrationBuilder.RenameColumn(
                name: "branch",
                table: "Profiles",
                newName: "Branch");

            migrationBuilder.RenameColumn(
                name: "Feecategory",
                table: "FeeTables",
                newName: "FeeCategory");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeeCategory",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Profiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PRN",
                table: "Profiles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FeeCategory",
                table: "FeeTables",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "FeeTables",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "FeeTables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FeeCategory",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PRN",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "FeeTables");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "FeeTables");

            migrationBuilder.RenameColumn(
                name: "Branch",
                table: "Profiles",
                newName: "branch");

            migrationBuilder.RenameColumn(
                name: "FeeCategory",
                table: "FeeTables",
                newName: "Feecategory");

            migrationBuilder.AlterColumn<string>(
                name: "Feecategory",
                table: "FeeTables",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FeeTables",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
