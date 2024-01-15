using ControleDeContatos.Data;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControleDeContatos.Repositorio
{
    public class RequisicaoRepositorio : IRequisicaoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public RequisicaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        List<RequisicaoViewModel> IRequisicaoRepositorio.BuscarRequisicaoPorUsuario(int id_usuario_logado)
        {
        return _bancoContext.requisicoes
                .Where(e => e.id_usuario == id_usuario_logado)
                .Join(
                    _bancoContext.status_requisicao,
                    requisicao => requisicao.status,
                    status => status.id_status,
                    (requisicao, status) => new RequisicaoViewModel
                    {
                        requisicao = requisicao,
                        statusReq = status
                    }) 
                .Join(
                    _bancoContext.Contatos,
                    requisicao => requisicao.requisicao.id_cliente, // Aqui eu acesso o objeto RequisicaoViewModel e acesso o atributo requisicao que é uma instancia da model RequisicaoModel
                    cliente => cliente.Id,
                    (requisicao, cliente) => new RequisicaoViewModel
                    {
                        requisicao = requisicao.requisicao,
                        statusReq = requisicao.statusReq,
                        cliente = cliente
                    })
                .Join(
                    _bancoContext.prioridade,
                    requisicao => requisicao.requisicao.id_prioridade, // Aqui eu acesso o objeto RequisicaoViewModel e acesso o atributo requisicao que é uma instancia da model RequisicaoModel
                    prioridade => prioridade.id_prioridade,
                    (requisicao, prioridade) => new RequisicaoViewModel
                    {
                        requisicao = requisicao.requisicao,
                        statusReq = requisicao.statusReq,
                        cliente = requisicao.cliente,
                        prioridade = prioridade
                    })
                .ToList();
        }

        public List<UsuarioModel> BuscaResponsavelPorNome(string txtSugestao)
        {
           return _bancoContext.Usuarios.Where(u => u.Nome.Contains(txtSugestao)).ToList();
        }
    }
}
