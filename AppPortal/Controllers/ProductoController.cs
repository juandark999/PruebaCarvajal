using AppPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace AppPortal.Controllers
{
	public class ProductoController : Controller
	{
		public IActionResult ProductoConsulta(List<ProductoModel>? productos)
		{
			return View("../Producto/ConsultaPro", productos);
		}
		public ActionResult ProductoNuevo()
		{
			return View();
		}
		public async Task<ActionResult> ProductoElimina(int Id)
		{
			ProductoNuevoModel? producto = new ProductoNuevoModel();

			producto = await GetByIdPro(Id);

			return await DeletedPro(producto);
		}
		public async Task<ActionResult> ProductoModifica(int Id)
		{
			ProductoNuevoModel? producto = new ProductoNuevoModel();

			producto = await GetByIdPro(Id);

			return View("EditarProducto", producto);
		}

		[HttpPost("GetByIdPro")]
		public async Task<ProductoNuevoModel?> GetByIdPro(int Id)
		{

			using (HttpClient cliente = new HttpClient())
			{

				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Producto/GetById?Id=" + Id);

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					ProductoNuevoModel? result = JsonConvert.DeserializeObject<ProductoNuevoModel>(contenido);

					return result;
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}
			ProductoNuevoModel result2 = new ProductoNuevoModel();

			return result2;
		}

		[HttpPost("SaveProductoNuevo")]
		public async Task<IActionResult> SaveProductoNuevo(ProductoNuevoModel producto)
		{

			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Producto/RegistrarProducto", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					return await GetAllPro();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpPost("ModifyProducto")]
		public async Task<dynamic> ModifyProducto(ProductoNuevoModel producto)
		{
			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Producto/EditarProducto", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					return await GetAllPro();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpPost("DeletedPro")]
		public async Task<dynamic> DeletedPro(ProductoNuevoModel? producto)
		{
			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Producto/EliminarProducto", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					return await GetAllPro();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpGet("GetAllPro")]
		public async Task<IActionResult> GetAllPro()
		{
			using (HttpClient cliente = new HttpClient())
			{
				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Producto/GetAll");

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(contenido))
					{
						List<ProductoModel>? productos = JsonConvert.DeserializeObject<List<ProductoModel>>(contenido);

						return ProductoConsulta(productos);

					}
					else
					{
						return View("Consulta");
					}

				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View("UsuarioConsulta");
		}
		[HttpGet("GetAllProPe")]
		public async Task<List<ProductoModel>?> GetAllProPe()
		{
			using (HttpClient cliente = new HttpClient())
			{
				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Producto/GetAll");

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(contenido))
					{
						List<ProductoModel>? productos = JsonConvert.DeserializeObject<List<ProductoModel>>(contenido);

						return productos;

					}
					else
					{
						List<ProductoModel>? productos2 = new List<ProductoModel>();
						return productos2;
					}

				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			List<ProductoModel>? productos3 = new List<ProductoModel>();
			return productos3;
		}

	}
}
