using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required] string nome, int anoLancamento, [Required] int artistaId, ICollection<GeneroRequest> generos = null);
}
