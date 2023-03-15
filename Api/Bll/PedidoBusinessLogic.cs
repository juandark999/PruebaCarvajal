using Api.Context;
using Api.Entities;
using Api.IBll;

namespace Api.Bll
{
    public class PedidoBusinessLogic: IPedidoBusinessLogic
    {
        private IGenericContext<Pedido> Context;
        public PedidoBusinessLogic(IGenericContext<Pedido> Context)
        {
            this.Context = Context;
        }

        public async Task<Pedido> Save(Pedido pedido)
        {
            if(pedido != null)
            {
                try
                {
                    return await Context.Save(pedido);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Pedido result = new Pedido()
                {
                    Errores = "no hay datos para procesar"
                };
                return result;
            }
            
        }
        public async Task<Pedido> Modify(Pedido pedido)
        {
            if( pedido != null)
            {
                try
                {
                    return await Context.Modify(pedido);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Pedido result = new Pedido()
                {
                    Errores = "no hay datos para procesar"
                };
                return result;
            }
            
        }
        public async Task<Pedido> Deleted(Pedido pedido)
        {
            if(pedido != null)
            {
                try
                {
                    return await Context.Deleted(pedido);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Pedido result = new Pedido()
                {
                    Errores = "no hay datos para procesar"
                };
                return result;
            }
            
        }
        public async Task<Pedido> GetById(int Id)
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
        public async Task<List<Pedido>> GetAll()
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
