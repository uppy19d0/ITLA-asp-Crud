using Microsoft.EntityFrameworkCore.Migrations;

namespace ITLASchool.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignaturas",
                columns: table => new
                {
                    AsignaturasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    TipoDeAsignatura = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaturas", x => x.AsignaturasID);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudiantesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Apellido = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudiantesID);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    ProfesoresID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Apellido = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.ProfesoresID);
                });

            migrationBuilder.CreateTable(
                name: "AsignaturasEstudiantes",
                columns: table => new
                {
                    AsignaturasEstudiantesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudiantesID = table.Column<int>(nullable: false),
                    AsignaturasID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignaturasEstudiantes", x => x.AsignaturasEstudiantesID);
                    table.ForeignKey(
                        name: "FK_AsignaturasEstudiantes_Asignaturas_AsignaturasID",
                        column: x => x.AsignaturasID,
                        principalTable: "Asignaturas",
                        principalColumn: "AsignaturasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignaturasEstudiantes_Estudiantes_EstudiantesID",
                        column: x => x.EstudiantesID,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudiantesID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsignaturasMaestros",
                columns: table => new
                {
                    AsignaturasMaestrosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsignaturasID = table.Column<int>(nullable: false),
                    ProfesoresID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignaturasMaestros", x => x.AsignaturasMaestrosID);
                    table.ForeignKey(
                        name: "FK_AsignaturasMaestros_Asignaturas_AsignaturasID",
                        column: x => x.AsignaturasID,
                        principalTable: "Asignaturas",
                        principalColumn: "AsignaturasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignaturasMaestros_Profesores_ProfesoresID",
                        column: x => x.ProfesoresID,
                        principalTable: "Profesores",
                        principalColumn: "ProfesoresID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturasEstudiantes_AsignaturasID",
                table: "AsignaturasEstudiantes",
                column: "AsignaturasID");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturasEstudiantes_EstudiantesID",
                table: "AsignaturasEstudiantes",
                column: "EstudiantesID");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturasMaestros_AsignaturasID",
                table: "AsignaturasMaestros",
                column: "AsignaturasID");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturasMaestros_ProfesoresID",
                table: "AsignaturasMaestros",
                column: "ProfesoresID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignaturasEstudiantes");

            migrationBuilder.DropTable(
                name: "AsignaturasMaestros");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Asignaturas");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
