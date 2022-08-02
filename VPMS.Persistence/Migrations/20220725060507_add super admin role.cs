using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPMS.Persistence.Migrations
{
    public partial class addsuperadminrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e24f4cd1-0759-440e-9a2b-6072880392b6"), "d6b0ba4f-67d0-4a6d-b37d-60f891d0875d", "superadmin", "SUPERADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e24f4cd1-0759-440e-9a2b-6072880392b6"));
        }
    }
}
