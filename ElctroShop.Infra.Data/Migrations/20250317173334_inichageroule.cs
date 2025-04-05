using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElctroShop.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class inichageroule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rouls",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Rouls",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
