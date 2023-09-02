using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sewa_Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_BusinessUser_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BusinessUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessUser_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BusinessUserId",
                table: "AspNetUsers",
                column: "BusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUser_OfficeId",
                table: "BusinessUser",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BusinessUser_BusinessUserId",
                table: "AspNetUsers",
                column: "BusinessUserId",
                principalTable: "BusinessUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BusinessUser_BusinessUserId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BusinessUser");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BusinessUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessUserId",
                table: "AspNetUsers");
        }
    }
}
