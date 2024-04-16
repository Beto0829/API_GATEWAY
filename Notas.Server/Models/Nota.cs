using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Notas.Server.Models
{
    public class Nota
    {
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        public required string Titulo { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength]
        public required string Descripcion { get; set; }


        public int IdCategoria { get; set; }

        public DateTime Fecha { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }
}