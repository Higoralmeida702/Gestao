using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Data.Dtos;
using Gestao.Service.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Gestao.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _iAuthService;

        public AuthController(IAuthService authService)
        {
            _iAuthService = authService;
        }

        [HttpPost("Registrar/Aluno")]
        public async Task<IActionResult> Register(AlunoDtos alunoDto)
        {
            var resposta = await _iAuthService.Registrar(alunoDto);
            return Ok(resposta);
        }
    }
}