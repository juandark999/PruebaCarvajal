using Api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Api.Entities
{
    public class Usuario: BaseEntity
    {
        public string? UsuNombre { get; set; }
        public string? UsuPass { get; set; }

    }
}
