using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPetShop.Migrations
{
    /// <inheritdoc />
    public partial class alterandotypepPettype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "VetServices",
                newName: "PetType");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PetServices",
                newName: "PetType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PetType",
                table: "VetServices",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "PetType",
                table: "PetServices",
                newName: "Type");
        }
    }
}
