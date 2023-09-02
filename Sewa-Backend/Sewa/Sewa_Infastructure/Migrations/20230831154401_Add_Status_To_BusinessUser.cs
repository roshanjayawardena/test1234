using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sewa_Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Status_To_BusinessUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BusinessUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BusinessUser");
        }
    }
}
