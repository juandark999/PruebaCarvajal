using AppPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using AppPortal.Controllers;





namespace AppPortal.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View("Login");
        }
        public IActionResult Registrarse()
        {
            return View("Registrarse");
        }

        [HttpPost("Save")]
        public async Task<ActionResult> Save(UsuarioModel usuario)
        {
            using (HttpClient cliente = new HttpClient())
            {

                HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Usuario/Save", contenidoHttp);

                if (respuesta.IsSuccessStatusCode)
                {
                    return View("Login");
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la información de la Web API.";
                }
            }

            return View("Registrarse");
        }


        [HttpPost("Login")]
        public async Task<dynamic> Login(UsuarioModel usuario)
        {
            using (HttpClient cliente = new HttpClient())
            {

                HttpContent contenidoHttp = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                HttpResponseMessage respuesta = await cliente.PostAsync("https://localhost:7224/Api/Usuario/Login", contenidoHttp);

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();
                    UsuarioModel? result = JsonConvert.DeserializeObject<UsuarioModel>(contenido);
                    if(result != null && result.Id != 0)
                    {
                        HttpContext.Session.SetInt32("Id", result.Id);	              
					}                  
                    UsuarioController Service = new UsuarioController();
                    return await Service.GetAll();
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la información de la Web API.";
                }
            }

            return View();
        }





    }
}
