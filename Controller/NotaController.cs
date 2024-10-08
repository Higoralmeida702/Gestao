using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gestao.Data.Dtos;
using Gestao.Service;

namespace Gestao.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly NotaService _notaService;

        public NotaController(NotaService notaService)
        {
            _notaService = notaService;
        }

        [HttpPost("AtribuirNota")]
        public async Task<IActionResult> AtribuirNota([FromBody] NotaAlunoDto notaDto)
        {
            var resultado = await _notaService.AtribuirNota(notaDto);

            return resultado;
        }

        [HttpGet("BuscarNotasPorRa/{ra}")]
        public async Task<IActionResult> BuscarNotasPorRA(int ra)
        {
            var notas = await _notaService.BuscarNotasPorRA(ra); 

            if (notas == null || !notas.Any()) 
            {
                return NotFound("Nenhuma nota encontrada para o RA informado.");
            }

            return Ok(notas);
        }

    }
}
