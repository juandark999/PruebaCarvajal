using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppPortal.Models
{
	public class ProductoNuevoModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Nombre")]
		public string? ProDesc { get; set; }
		[Required]
		[Display(Name = "Valor")]
		public decimal ProValor { get; set; }
	}
}
