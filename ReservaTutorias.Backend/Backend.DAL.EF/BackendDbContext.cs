using Backend.DO.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.DAL.EF
{
    public partial class BackendDbContext: DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }

        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Tema> Tema { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tema>(entity =>
            {
                entity.HasOne(d => d.Materia)
                    .WithMany(p => p.Tema)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__Tema__IdMateria__1EA48E88");
            });
        }
    }
}
