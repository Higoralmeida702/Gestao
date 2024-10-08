using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gestao.Data.Dtos;
using Gestao.Service;
using Gestao.Enum;

namespace Gestao.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(Roles = nameof(CargoEnum.Professor))]
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

        [HttpGet("obterRA/{usuario}")]
        public async Task<IActionResult> ObterRaPorUsuario(string usuario)
        {
            var ra = await _notaService.ObterRaPorUsuario(usuario);

            if (ra.HasValue)
            {
                return Ok(new { RA = ra.Value });
            }

            return NotFound("Aluno não encontrado.");
        }

        [HttpPut("editarNota/{notaId}")]
        public async Task<IActionResult> EditarNota(int notaId, [FromBody] double novaNota)
        {
            var resultado = await _notaService.EditarNota(notaId, novaNota);
            if (resultado.StartsWith("Erro"))
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpDelete("excluirNota/{notaId}")]
        public async Task<IActionResult> ExcluirNota(int notaId)
        {
            var resultado = await _notaService.ExcluirNota(notaId);
            if (resultado.StartsWith("Erro"))
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
