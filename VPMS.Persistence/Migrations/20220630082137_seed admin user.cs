using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPMS.Persistence.Migrations
{
    public partial class seedadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b74ddd14-6340-4840-95c2-db12554843e5"), 0, "70428f75-0a6f-4d92-a2cd-ae4e0cdbd10f", null, new DateTimeOffset(new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), "admin@vpms.com", true, false, null, "ADMIN@VPMS.COM", "ADMIN", "AQAAAAEAACcQAAAAEJZHh/S5hmTm+8BR8ssy2GyMm04koddmCJLLGetMIWDEwKTXVwjow5mnIKwK5ExMNA==", "1234567890", false, "70428f75-0a6f-4d92-a2cd-ae4e0cdbd10f", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"));
        }
    }
}
