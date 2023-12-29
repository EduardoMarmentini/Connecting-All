using ControleDeContatos.Models.Cliente;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }
        // Informar a Model que representa a tabela de contatos no banco de dados
        public DbSet<ContatoModel> Contatos { get; set; } 
        // Informa a Model que representa a tabela de usuários no banco de dados
        public DbSet<UsuarioModel> Usuarios { get; set; }
        // Informa a Model que representa a tabela de requisições no banco de dados
        public DbSet<RequisicaoModel> requisicoes { get; set; }

        public DbSet<StatusReqModel> status_requisicao { get; set; }
    }
}
