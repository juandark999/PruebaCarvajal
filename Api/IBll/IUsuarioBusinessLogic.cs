using Api.Entities;

namespace Api.IBll
{
    public interface IUsuarioBusinessLogic
    {
        Task<Usuario> Save(Usuario usuario);
        Task<Usuario> Modify(Usuario usuario);
        Task<Usuario> Deleted(Usuario usuario);
        Task<Usuario> GetById(int Id);
        Task<List<Usuario>> GetAll();
        Usuario Login(Usuario usuario);
    }
}
