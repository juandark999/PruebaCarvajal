using Api.Bll;
using Api.Entities;
using Api.IBll;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/Producto")]
    public class ProductoController : Controller
    {
        private IProductoBusinessLogic service;
        public ProductoController(IProductoBusinessLogic service)
        {
            this.service = service;
        }

        [HttpPost("RegistrarProducto")]
        public async Task<Producto> RegistrarProducto(Producto producto)
        {

            Producto result = await service.Save(producto);

            return result;
        }

        [HttpPost("EditarProducto")]
        public async Task<Producto> EditarProducto(Producto producto)
        {

            Producto result = await service.Modify(producto);

            return result;
        }

        [HttpPost("EliminarProducto")]
        public async Task<Producto> EliminarProducto(Producto producto)
        {

            Producto result = await service.Deleted(producto);

            return result;
        }

        [HttpGet("GetById")]
        public async Task<Producto> GetById(int Id)
        {

            Producto result = await service.GetById(Id);

            return result;
        }

        [HttpGet("GetAll")]
        public async Task<List<Producto>> GetAll()
        {
            return await service.GetAll();
        }
    }
}
