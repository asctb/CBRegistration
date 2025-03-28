using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBRegistration.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedverifiedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasVerfiedPhone",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVerifiedEmail",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVerfiedPhone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasVerifiedEmail",
                table: "Users");
        }
    }
}
