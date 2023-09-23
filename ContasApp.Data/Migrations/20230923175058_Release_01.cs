using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContasApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Release_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusConta",
                table: "CONTA",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusConta",
                table: "CONTA");
        }
    }
}
