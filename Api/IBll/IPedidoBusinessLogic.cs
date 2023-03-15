using Api.Entities;

namespace Api.IBll
{
    public interface IPedidoBusinessLogic
    {
        Task<Pedido> Save(Pedido producto);
        Task<Pedido> Modify(Pedido producto);
        Task<Pedido> Deleted(Pedido producto);
        Task<Pedido> GetById(int Id);
        Task<List<Pedido>> GetAll();
    }
}
