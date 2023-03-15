using Api.Entities;

namespace Api.IBll
{
    public interface IProductoBusinessLogic
    {
        Task<Producto> Save(Producto producto);
        Task<Producto> Modify(Producto producto);
        Task<Producto> Deleted(Producto producto);
        Task<Producto> GetById(int Id);
        Task<List<Producto>> GetAll();
    }
}

