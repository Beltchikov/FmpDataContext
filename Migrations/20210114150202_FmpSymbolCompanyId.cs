using Microsoft.EntityFrameworkCore.Migrations;

namespace FmpDataContext.Migrations
{
    public partial class FmpSymbolCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FmpSymbolCompany",
                table: "FmpSymbolCompany");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "FmpSymbolCompany",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FmpSymbolCompany",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FmpSymbolCompany",
                table: "FmpSymbolCompany",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FmpSymbolCompany",
                table: "FmpSymbolCompany");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FmpSymbolCompany");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "FmpSymbolCompany",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FmpSymbolCompany",
                table: "FmpSymbolCompany",
                column: "Symbol");
        }
    }
}
