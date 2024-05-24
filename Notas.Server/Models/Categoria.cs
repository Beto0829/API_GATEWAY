using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Notas.Server.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string Nombre { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public string Email { get; set; }

        [JsonIgnore]
        public List<Nota>? Notas { get; set; }
    }
}