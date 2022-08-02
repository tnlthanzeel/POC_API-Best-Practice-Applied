using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VPMS.Persistence.Migrations
{
    public partial class indexbyisdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todo_IsDeleted",
                table: "Todo",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Todo_IsDeleted",
                table: "Todo");
        }
    }
}
