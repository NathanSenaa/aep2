namespace DoaFacil.Pedidos.Application.Commands.DTO
{
    public class ItemPedidoDTO
    {
        public Guid IdPedido { get; set; }
        public Guid IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
