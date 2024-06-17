using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoaFacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_table_solicitantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "solicitantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(250)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(13)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitantes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "solicitantes");
        }
    }
}
