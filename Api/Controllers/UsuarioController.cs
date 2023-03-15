using Api.Entities;
using Api.IBll;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("Api/Usuario")]
    public class UsuarioController : Controller
    {

        private IUsuarioBusinessLogic service;
        public UsuarioController(IUsuarioBusinessLogic service)
        {
            this.service = service;
        }

        [HttpPost("Save")]
        public async Task<Usuario> Save(Usuario usuario)
        {
            return await service.Save(usuario);    
        }

        [HttpPost("Modify")]
        public async Task<Usuario> Modify(Usuario usuario)
        {

            Usuario result = await service.Modify(usuario);

            return result;
        }

        [HttpPost("Deleted")]
        public async Task<Usuario> Deleted(Usuario usuario)
        {

            Usuario result = await service.Deleted(usuario);

            return result;
        }

        [HttpGet("GetById")]
        public async Task<Usuario> GetById(int Id)
        {

            Usuario result = await service.GetById(Id);

            return result;
        }

        [HttpGet("GetAll")]
        public async Task<List<Usuario>> GetAll()
        {
            return await service.GetAll();
        }
        
        [HttpPost("Login")]
        public Usuario Login(Usuario usuario)
        {
            return service.Login(usuario);
        }

       }


}
