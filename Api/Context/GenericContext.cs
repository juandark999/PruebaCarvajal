using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Context
{
    public class GenericContext<T> : IGenericContext<T> where T : BaseEntity
    {
        private MyContext context;

        public GenericContext(MyContext context)
        {
            this.context = context;

        }

        public async Task<T> Save(T Data)
        {
            context.Entry(Data).State = EntityState.Added;
            context.SaveChanges();
             var a = await Task.Run(() => { return Data; });
            return a;
        }

        public async Task<T> Modify(T Data)
        {
            context.Entry(Data).State = EntityState.Modified;
            context.SaveChanges();
            return await Task.Run(() => { return Data; });
        }
        public async Task<T> Deleted(T Data)
        {
            context.Entry(Data).State = EntityState.Deleted;
            context.SaveChanges();
            return await Task.Run(() => { return Data; });
        }
        public async Task<T?> GetById(int Id)
        {
            T? result = context.Set<T>().FirstOrDefault(x => x.Id == Id);
            return await Task.Run(() => { return result; });
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            List<T> result;
            if (predicate != null)
                result = context.Set<T>().Where(predicate).ToList();
            else
                result = context.Set<T>().ToList();
            return await Task.Run(() => { return result; });

        }
    }


}
