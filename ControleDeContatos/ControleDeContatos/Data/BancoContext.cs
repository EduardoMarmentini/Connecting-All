using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        // Informar a Model que representa a tabela no banco de dados
        public DbSet<ContatoModel> Contatos { get; set; } 
    }
}
