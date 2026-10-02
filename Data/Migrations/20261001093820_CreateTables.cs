using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace implementation.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NIF = table.Column<string>(type: "TEXT", nullable: false),
                    Idade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrestacoesAtuais = table.Column<decimal>(type: "TEXT", nullable: false),
                    RendimentoMensal = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValorPretendido = table.Column<decimal>(type: "TEXT", nullable: false),
                    Prazo = table.Column<int>(type: "INTEGER", nullable: false),
                    IncidentesCredito = table.Column<bool>(type: "INTEGER", nullable: false),
                    SituacaoProfissional = table.Column<string>(type: "TEXT", nullable: false),
                    DecisaoFinal = table.Column<string>(type: "TEXT", nullable: false),
                    Motivos = table.Column<string>(type: "TEXT", nullable: false),
                    PrestacaoNova = table.Column<decimal>(type: "TEXT", nullable: false),
                    TaxaEsforco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
