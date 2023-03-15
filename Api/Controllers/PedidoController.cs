using Api.IBll;
using Microsoft.AspNetCore.Mvc;
using Api.Bll;
using Api.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/Pedido")]
    public class PedidoController : Controller
    {
        private IPedidoBusinessLogic service;
        public PedidoController(IPedidoBusinessLogic service)
        {
            this.service = service;
        }

        [HttpPost("RegistrarPedido")]
        public async Task<Pedido> RegistrarPedido(Pedido pedido)
        {

            Pedido result = await service.Save(pedido);

            return result;
        }

        [HttpPost("EditarPedido")]
        public async Task<Pedido> EditarPedido(Pedido pedido)
        {

            Pedido result = await service.Modify(pedido);

            return result;
        }

        [HttpPost("EliminarPedido")]
        public async Task<Pedido> EliminarPedido(Pedido pedido)
        {

            Pedido result = await service.Deleted(pedido);

            return result;
        }

        [HttpGet("GetById")]
        public async Task<Pedido> GetById(int Id)
        {

            Pedido result = await service.GetById(Id);

            return result;
        }

        [HttpGet("GetAll")]
        public async Task<List<Pedido>> GetAll()
        {
            return await service.GetAll();
        }


    }


}
