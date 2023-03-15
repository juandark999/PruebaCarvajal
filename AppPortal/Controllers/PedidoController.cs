using AppPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace AppPortal.Controllers
{
	public class PedidoController : Controller
	{
		public IActionResult PedidoConsulta(List<PedidoModel>? pedido)
		{
			return View("../Pedido/ConsultaPe", pedido);
		}
		public async Task<ActionResult> PedidoNuevo()
		{
			ProductoController service = new ProductoController();
			List<ProductoModel>? productos = await service.GetAllProPe();
			return View(productos);
		}
		public async Task<ActionResult> PedidoElimina(int Id)
		{
			PedidoNuevoModel? pedido = new PedidoNuevoModel();

			pedido = await GetByIdPe(Id);

			return await DeletedPe(pedido);
		}
		public async Task<ActionResult> PedidoModifica(int Id)
		{
			PedidoNuevoModel? pedido = new PedidoNuevoModel();

			pedido = await GetByIdPe(Id);
			if (pedido != null && pedido.Id != 0)
			{
				HttpContext.Session.SetInt32("IdProducto", pedido.Id);
			}
			ProductoController service = new ProductoController();
			List<ProductoModel>? productos = await service.GetAllProPe();

			return View("EditarPedido", productos);
		}

		[HttpPost("GetByIdPe")]
		public async Task<PedidoNuevoModel?> GetByIdPe(int Id)
		{

			using (HttpClient cliente = new HttpClient())
			{

				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Pedido/GetById?Id=" + Id);

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					PedidoNuevoModel? result = JsonConvert.DeserializeObject<PedidoNuevoModel>(contenido);

					return result;
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}
			PedidoNuevoModel result2 = new PedidoNuevoModel();

			return result2;
		}

		[HttpPost("SavePedidoNuevo")]
		public async Task<IActionResult> SavePedidoNuevo(PedidoNuevoModel pedido)
		{
			ProductoController service = new ProductoController();
			ProductoNuevoModel? producto =  await service.GetByIdPro(pedido.PedPro);
			if(producto != null && producto.ProValor != 0)
			{
				pedido.PedVrUnit = producto.ProValor;
				pedido.PedSubtot = producto.ProValor * (decimal)pedido.PedCant;
				pedido.PedIVA = (double)pedido.PedSubtot * 0.19;
				pedido.PedTotal = pedido.PedSubtot + (decimal)pedido.PedIVA;
			}
				pedido.PedUsu = HttpContext.Session.GetInt32("Id").Value;
			
			
			//id pedido, id usuario, id producto, valor del producto, cantidad producto, sub total


			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Pedido/RegistrarPedido", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					return await GetAllPe();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpPost("ModifyPedido")]
		public async Task<dynamic> ModifyPedido(PedidoNuevoModel pedido)
		{

			ProductoController service = new ProductoController();
			ProductoNuevoModel? producto = await service.GetByIdPro(pedido.PedPro);
			if (producto != null && producto.ProValor != 0)
			{
				pedido.PedVrUnit = producto.ProValor;
				pedido.PedSubtot = producto.ProValor * (decimal)pedido.PedCant;
				pedido.PedIVA = (double)pedido.PedSubtot * 0.19;
				pedido.PedTotal = pedido.PedSubtot + (decimal)pedido.PedIVA;
			}
			pedido.PedUsu = HttpContext.Session.GetInt32("Id").Value;
			pedido.Id = HttpContext.Session.GetInt32("IdProducto").Value;

			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Pedido/EditarPedido", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					HttpContext.Session.Remove("IdProducto");
					return await GetAllPe();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpPost("DeletedPe")]
		public async Task<dynamic> DeletedPe(PedidoNuevoModel? pedido)
		{
			using (HttpClient cliente = new HttpClient())
			{

				HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

				HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Pedido/EliminarPedido", contenidoHttp);

				if (respuesta.IsSuccessStatusCode)
				{
					return await GetAllPe();
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}

			return View();
		}

		[HttpGet("GetAllPe")]
		public async Task<IActionResult> GetAllPe()
		{
			using (HttpClient cliente = new HttpClient())
			{
				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Pedido/GetAll");

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(contenido))
					{
						List<PedidoModel>? pedido = JsonConvert.DeserializeObject<List<PedidoModel>>(contenido);

						return PedidoConsulta(pedido);

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

	}
}
