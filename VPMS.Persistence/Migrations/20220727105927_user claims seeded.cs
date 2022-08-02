using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPMS.Persistence.Migrations
{
    public partial class userclaimsseeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "permission", "all", new Guid("b74ddd14-6340-4840-95c2-db12554843e5") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 1, "permission", "all", new Guid("e24f4cd1-0759-440e-9a2b-6072880392b6") });
        }
    }
}
