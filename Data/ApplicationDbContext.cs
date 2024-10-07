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
        public DbSet<NotaAluno> NotaAlunos {get; set;}
        public DbSet<Materia> Materias {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materia>().HasData(
                new Materia {Id = 1, Nome = "Português"},
                new Materia {Id = 2, Nome = "Matemática"},
                new Materia {Id = 3, Nome = "Química"},
                new Materia {Id = 4, Nome = "Biologia"},
                new Materia {Id = 5, Nome = "Educação Física"},
                new Materia {Id = 6, Nome = "Artes"}
            );
        }
    }
}