using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestao.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Aluno> Alunos {get; set;}
        public DbSet<Professores> Professores {get; set;}
    }
}