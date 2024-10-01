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
        private readonly IAlunoAuthService _iAlunoAuthService;
        private readonly IProfessorAuthService _iProfessorAuthService;

        public AuthController(IAlunoAuthService alunoAuthService, IProfessorAuthService iProfessorAuthService)
        {
            _iAlunoAuthService = alunoAuthService;
            _iProfessorAuthService = iProfessorAuthService;
        }

        [HttpPost("Registrar/Aluno")]
        public async Task<IActionResult> Register(AlunoDto alunoDto)
        {
            var resposta = await _iAlunoAuthService.Registrar(alunoDto);
            return Ok(resposta);
        }

        [HttpPost("Login/Aluno")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var resposta = await _iAlunoAuthService.Login(loginDto);
            return Ok(resposta);
        }

        [HttpPost("Registrar/Professor")]
        public async Task<IActionResult> RegistrarProfessor(ProfessorDto professorDto)
        {
            var resposta = await _iProfessorAuthService.Registrar(professorDto);
            return Ok(resposta);
        }

        [HttpPost("Login/Professor")]
        public async Task<IActionResult> LoginProfessor(LoginDto ProfessorLoginDto)
        {
            var resposta = await _iProfessorAuthService.Login(ProfessorLoginDto);
            return Ok(resposta);
        }
    }
}