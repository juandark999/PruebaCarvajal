using AppPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;


namespace AppPortal.Controllers
{
    public class UsuarioController: Controller
    {

        public IActionResult UsuarioConsulta(List<UsuarioModel>? usuarios)
        {
            return View("../Usuario/Consulta", usuarios);
        }
        public ActionResult UsuarioNuevo()
        {
            return View();
        }
		public async Task<ActionResult> UsuarioElimina(int Id)
		{
			UsuarioNuevoModel? usuario = new UsuarioNuevoModel();

			usuario = await GetById(Id);

			return await Deleted(usuario);
		}
		public async Task<ActionResult> UsuarioModifica(int Id)
		{
            UsuarioNuevoModel? usuario = new UsuarioNuevoModel();

            usuario = await GetById(Id);

			return View("Editar",usuario);
		}

		[HttpPost("GetById")]
		public async Task<UsuarioNuevoModel?> GetById(int Id)
		{

			using (HttpClient cliente = new HttpClient())
			{
	
				HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Usuario/GetById?Id=" + Id);

				if (respuesta.IsSuccessStatusCode)
				{
					string contenido = await respuesta.Content.ReadAsStringAsync();

					UsuarioNuevoModel? result = JsonConvert.DeserializeObject<UsuarioNuevoModel>(contenido);

					return result;
				}
				else
				{
					ViewBag.Error = "No se pudo obtener la información de la Web API.";
				}
			}
            UsuarioNuevoModel result2 = new UsuarioNuevoModel();

			return result2;
		}

		[HttpPost("SaveUsuarioNuevo")]
        public async Task<IActionResult> SaveUsuarioNuevo(UsuarioNuevoModel usuario)
        {
            //if (ModelState.IsValid)
            //{

            //}

            using (HttpClient cliente = new HttpClient())
            {

                HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Usuario/Save", contenidoHttp);

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();

                    var result = contenido;

                    return await GetAll();
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la información de la Web API.";
                }
            }

            return View();
        }

        [HttpPost("ModifyUsuario")]
        public async Task<dynamic> ModifyUsuario(UsuarioNuevoModel usuario)
        {
            using (HttpClient cliente = new HttpClient())
            {

                HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Usuario/Modify", contenidoHttp);

                if (respuesta.IsSuccessStatusCode)
                {
                    return await GetAll();
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la información de la Web API.";
                }
            }

            return View();
        }

        [HttpPost("Deleted")]
        public async Task<dynamic> Deleted(UsuarioNuevoModel? usuario)
        {
            using (HttpClient cliente = new HttpClient())
            {

                HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Usuario/Deleted", contenidoHttp);

                if (respuesta.IsSuccessStatusCode)
                {
                    return await GetAll();
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la información de la Web API.";
                }
            }

            return View();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            using (HttpClient cliente = new HttpClient())
            {
                HttpResponseMessage respuesta = await cliente.GetAsync("https://localhost:7224/Api/Usuario/GetAll");

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();

					if (!string.IsNullOrEmpty(contenido))
					{
						List<UsuarioModel>? usuarios =  JsonConvert.DeserializeObject<List<UsuarioModel>>(contenido);

                        return UsuarioConsulta(usuarios);

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
