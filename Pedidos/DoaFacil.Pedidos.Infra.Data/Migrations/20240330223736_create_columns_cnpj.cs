using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoaFacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_columns_cnpj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "solicitantes",
                type: "varchar(14)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "solicitantes");
        }
    }
}
