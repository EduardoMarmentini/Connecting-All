using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeContatos.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTableRequisicoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "requisicoes",
                columns: table => new
                {
                    idrequisicao = table.Column<int>(name: "id_requisicao", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(name: "id_usuario", type: "int", nullable: false),
                    responsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titulorequisicao = table.Column<string>(name: "titulo_requisicao", type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    datacadastro = table.Column<DateTime>(name: "data_cadastro", type: "datetime2", nullable: false),
                    dataentrega = table.Column<DateTime>(name: "data_entrega", type: "datetime2", nullable: false),
                    dataconclusao = table.Column<DateTime>(name: "data_conclusao", type: "datetime2", nullable: false),
                    horastrabalhadas = table.Column<DateTime>(name: "horas_trabalhadas", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requisicoes", x => x.idrequisicao);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requisicoes");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
