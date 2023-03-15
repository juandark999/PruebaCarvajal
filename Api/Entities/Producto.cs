using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class Producto: BaseEntity
    {
     
        public string? ProDesc { get; set; }
        public decimal ProValor { get; set; }
       
       
    }
}
