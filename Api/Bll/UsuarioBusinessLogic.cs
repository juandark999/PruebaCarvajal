using Api.Context;
using Api.Entities;
using Api.IBll;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Bll
{
	public class UsuarioBusinessLogic : IUsuarioBusinessLogic
	{
		private IGenericContext<Usuario> Context;
		public UsuarioBusinessLogic(IGenericContext<Usuario> Context)
		{
			this.Context = Context;
		}

		public async Task<Usuario> Save(Usuario usuario)
		{
			if (usuario != null)
			{
				try
				{
					return await Context.Save(usuario);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else
			{
				Usuario result = new Usuario()
				{
					Errores = "no hay datos para procesar"
				};
				return result;
			}

		}
		public async Task<Usuario> Modify(Usuario usuario)
		{
			if (usuario != null)
			{
				try
				{
					return await Context.Modify(usuario);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else
			{
				Usuario result = new Usuario()
				{
					Errores = "no hay datos para procesar"
				};
				return result;
			}

		}
		public async Task<Usuario> Deleted(Usuario usuario)
		{
			if (usuario != null)
			{
				try
				{
					return await Context.Deleted(usuario);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else
			{
				Usuario result = new Usuario()
				{
					Errores = "no hay datos para procesar"
				};
				return result;
			}

		}
		public async Task<Usuario> GetById(int Id)
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
		public async Task<List<Usuario>> GetAll()
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

		public Usuario Login(Usuario usuario)
		{
			try
			{
				using (SqlConnection cn = new SqlConnection("Data Source=DESKTOP-S5VKFGQ\\SQLEXPRESS;Initial Catalog=Carvajal;Integrated Security=True;User ID=juan;Password=1234;Trust Server Certificate=true"))
				{
					SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
					cmd.Parameters.AddWithValue("Nombre", usuario.UsuNombre);
					cmd.Parameters.AddWithValue("Clave", usuario.UsuPass);
					cmd.CommandType = CommandType.StoredProcedure;

					cn.Open();



					usuario.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
				}
				if (usuario.Id != 0)
				{
					return usuario;
				}
				else
				{
					Usuario result = new Usuario()
					{
						Errores = "no se encontro el usuario"
					};
					return result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}


			


		}
	}
}
