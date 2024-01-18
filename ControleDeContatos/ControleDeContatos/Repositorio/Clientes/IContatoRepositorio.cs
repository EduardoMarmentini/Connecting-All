using ControleDeContatos.Models.Cliente;

namespace ControleDeContatos.Repositorio.Clientes
{
    public interface IContatoRepositorio
    {
        // Metodo de buscar informaçõespor id
        ContatoModel ListarPorId(int id);
        // Metodo de buscar todos os registros
        List<ContatoViewModel> BuscarTodos();
        // Metodo de adicionar registros as tabela.
        ContatoModel Adicionar(ContatoModel contato, IFormFile picture_upload);
        // Metodo para alterar os dados do contato na tabela de contatos
        ContatoModel Alterar(ContatoModel contato, IFormFile picture_upload);
        // Metodo para Apagar o contato na tabela de contatos
        ContatoModel Excluir(ContatoModel contato);
    }
}