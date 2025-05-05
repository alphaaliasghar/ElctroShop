using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectonShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class changegrouptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ProductGroups",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ProductGroups");
        }
    }
}
