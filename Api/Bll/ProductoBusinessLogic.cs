using Api.Context;
using Api.Entities;
using Api.IBll;

namespace Api.Bll
{
    public class ProductoBusinessLogic: IProductoBusinessLogic
    {
        private IGenericContext<Producto> Context;
        public ProductoBusinessLogic(IGenericContext<Producto> Context)
        {
            this.Context = Context;
        }

        public async Task<Producto> Save(Producto producto)
        {
            if (producto != null)
            {
                try
                {
                    return await Context.Save(producto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Producto result = new Producto()
                {
                    Errores = "no hay datos para procesar"
                };
            return result;
            }
            
        }
        public async Task<Producto> Modify(Producto producto)
        {
            if(producto != null)
            {
                try
                {
                    return await Context.Modify(producto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Producto result = new Producto()
                {
                    Errores = "no hay datos para procesar"
                };
                return result;
            }
           
        }
        public async Task<Producto> Deleted(Producto producto)
        {
            if(producto != null)
            {
                try
                {
                    return await Context.Deleted(producto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Producto result = new Producto()
                {
                    Errores = "no hay datos para procesar"
                };
                return result;
            }
            
        }
        public async Task<Producto> GetById(int Id)
        {
           
                try
                {
                    return await Context.GetById(Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
           
            
        }
        public async Task<List<Producto>> GetAll()
        {
            try
            {
                return await Context.GetAll(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
