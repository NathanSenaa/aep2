using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoaFacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_columns_quantidade_total : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTotal",
                table: "pedidos_doacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeTotal",
                table: "pedidos_doacao");
        }
    }
}
