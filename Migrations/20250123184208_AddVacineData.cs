using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPetShop.Migrations
{
    /// <inheritdoc />
    public partial class AddVacineData : Migration
    {
        private readonly string[] Vacines = { "Felv", "Giárdia", "Gripe Canina", "Gripe Oral", "Leptospirose",
                                                "Múltipla Cães (V10)", "Múltipla Cães (V7)", "Múltipla Cães (V3)", "Múltipla Cães (V4)",
                                                "Múltipla Cães (V5)", "Raiva"};

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var vacine in Vacines)
            {
                migrationBuilder.Sql($@"INSERT INTO Vacines (Id, Name) VALUES (NEWID(), '{vacine}')");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var vacine in Vacines)
            {
                migrationBuilder.Sql($@"DELETE FROM Vacines WHERE Name = '{vacine}'");
            }
        }
    }
}
