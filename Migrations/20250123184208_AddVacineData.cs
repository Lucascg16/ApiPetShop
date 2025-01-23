using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPetShop.Migrations
{
    /// <inheritdoc />
    public partial class AddVacineData : Migration
    {
        private readonly string[] Vacines = { "Vacina Felv", "Vacina Giárdia", "Vacina Gripe Canina", "Vacina Gripe Oral", "Vacina Leptospirose",
                                                "Vacina Múltipla Cães (V10)", "Vacina Múltipla Cães (V7)", "Vacina Múltipla Cães (V3)", "Vacina Múltipla Cães (V4)",
                                                "Vacina Múltipla Cães (V5)", "Vacina Raiva"};

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
