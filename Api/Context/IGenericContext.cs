using Api.Entities;
using System.Linq.Expressions;

namespace Api.Context
{
    public interface IGenericContext<T> where T : BaseEntity
    {
        Task<T> Save(T Data);
        Task<T> Modify(T Data);
        Task<T> Deleted(T Data);
        Task<T?> GetById(int Id);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? predicate = null);

    }
}
