using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoaFacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_tables_pedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "produtos");

            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia",
                startValue: 1000L);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "solicitantes",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produtos",
                table: "produtos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "pedidos_doacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdSolicitante = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusPedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos_doacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pedidos_doacao_solicitantes_IdSolicitante",
                        column: x => x.IdSolicitante,
                        principalTable: "solicitantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "items_pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPedidoDoacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoNome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items_pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_pedido_pedidos_doacao_IdPedidoDoacao",
                        column: x => x.IdPedidoDoacao,
                        principalTable: "pedidos_doacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_items_pedido_produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "produtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_pedido_IdPedidoDoacao",
                table: "items_pedido",
                column: "IdPedidoDoacao");

            migrationBuilder.CreateIndex(
                name: "IX_items_pedido_IdProduto",
                table: "items_pedido",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_doacao_IdSolicitante",
                table: "pedidos_doacao",
                column: "IdSolicitante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items_pedido");

            migrationBuilder.DropTable(
                name: "pedidos_doacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produtos",
                table: "produtos");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");

            migrationBuilder.RenameTable(
                name: "produtos",
                newName: "Produtos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "solicitantes",
                type: "varchar()",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos",
                column: "Id");
        }
    }
}
