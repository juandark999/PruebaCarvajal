using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppPortal.Models
{
	public class UsuarioNuevoModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Nombre")]
		public string? UsuNombre { get; set; }
		[Required]
		[Display(Name = "Clave")]
		public string? UsuPass { get; set; }
	}
}
