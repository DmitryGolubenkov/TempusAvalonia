using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkPeriod_Date",
                table: "WorkPeriod",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkPeriod_Date",
                table: "WorkPeriod");
        }
    }
}
