using Backend.DO.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DAL.EF
{
    public class BackendDbContext: DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }
        public DbSet<Entrada> Entrada { get; set; }
        public DbSet<Expediente> Expediente { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Tema> Tema { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
