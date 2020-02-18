using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLASchool.Models;
using System.ComponentModel.DataAnnotations;

namespace ITLASchool.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Estudiantes> Estudiantes { get; set; }
        public DbSet<Profesores> Profesores { get; set; }
        public DbSet<Asignaturas> Asignaturas { get; set; }
        public DbSet<AsignaturasMaestros> AsignaturasMaestros { get; set; }
        public DbSet<AsignaturasEstudiantes> AsignaturasEstudiantes { get; set; }
    }

    public class Estudiantes
    {

        public int EstudiantesID { get; set; }
        [Required(ErrorMessage = "Matricula Requerida")]
        public string Matricula { get; set; }
        [Required(ErrorMessage = "Nombre Requerido ")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido Requerido ")]
        public string Apellido { get; set; }

    }
    public class Profesores
    {
        public int ProfesoresID { get; set; }

        [Required(ErrorMessage = "Nombre Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido Requerido ")]
        public string Apellido { get; set; }
        public virtual ICollection<AsignaturasMaestros> AsignaturasMaestros { get; set; }
    }
    public class Asignaturas
    {
        public int AsignaturasID { get; set; }
        [Required(ErrorMessage = "Nombre Requerido ")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Tipo Requerido ")]
        public string TipoDeAsignatura { get; set; }
        public virtual ICollection<AsignaturasMaestros> AsignaturasMaestros { get; set; }
        public virtual ICollection<AsignaturasEstudiantes> AsignaturasEstudiantes { get; set; }

    }
    public class AsignaturasMaestros
    {
        public int AsignaturasMaestrosID { get; set; }
        [Required(ErrorMessage = "Asignaturas Requerido ")]
        public int AsignaturasID { get; set; }
        public virtual Asignaturas Asignaturas { get; set; }
        [Required(ErrorMessage = "Maestro  Requerido ")]
        public int ProfesoresID { get; set; }
        public virtual Profesores Profesores { get; set; }
    }
    public class AsignaturasEstudiantes
    {
        public int AsignaturasEstudiantesID { get; set; }
        [Required(ErrorMessage = "Estudiante Requerido ")]
        public int EstudiantesID { get; set; }
        [Required(ErrorMessage = "Asignatura Requerido ")]
        public int AsignaturasID { get; set; }
        public virtual Asignaturas Asignaturas { get; set; }
        public virtual Estudiantes Estudiantes { get; set; }
    }
}
