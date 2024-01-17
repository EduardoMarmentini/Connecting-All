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
                        _bancoContext.Usuarios,
                        requisicao => requisicao.requisicao.id_usuario,
                        usuario => usuario.Id,
                        (requisicao, usuario) => new RequisicaoViewModel
                        {
                            requisicao = requisicao.requisicao,
                            statusReq = requisicao.statusReq,
                            usuario = usuario
                        })
                    .Join(
                        _bancoContext.Contatos,
                        requisicao => requisicao.requisicao.id_cliente, // Aqui eu acesso o objeto RequisicaoViewModel e acesso o atributo requisicao que é uma instancia da model RequisicaoModel
                        cliente => cliente.Id,
                        (requisicao, cliente) => new RequisicaoViewModel
                        {
                            requisicao = requisicao.requisicao,
                            statusReq = requisicao.statusReq,
                            usuario = requisicao.usuario,
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
                            usuario = requisicao.usuario,
                            cliente = requisicao.cliente,
                            prioridade = prioridade
                        })
                    .ToList();
        }

        public List<RequisicaoViewModel> BuscarRequisicaoPorFiltros(int id_requisicao, string titulo_requisicao, int id_usuario, int id_cliente, int id_estado_req)
        {
            var query = _bancoContext.requisicoes;

            if (id_requisicao != 0)
            {
                query = query.Where(e => e.id_requisicao == id_requisicao);
            }

            if (id_usuario != 0)
            {
                query = query.Where(e => e.id_usuario == id_usuario);
            }

            if (id_cliente != 0)
            {
                query = query.Where(e => e.id_cliente == id_cliente);
            }

            if (id_estado_req != 0)
            {
                if (id_estado_req == 1)
                {
                    query = query.Where(e => e.status != 12);
                }

                query = query.Where(e => e.status == 12);
            }

            if (!string.IsNullOrEmpty(titulo_requisicao))
            {
                query = query.Where(e => e.titulo_requisicao == titulo_requisicao);
            }

            var result = query
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
                    _bancoContext.Usuarios,
                    requisicao => requisicao.requisicao.id_usuario,
                    usuario => usuario.Id,
                    (requisicao, usuario) => new RequisicaoViewModel
                    {
                        requisicao = requisicao.requisicao,
                        statusReq = requisicao.statusReq,
                        usuario = usuario
                    })
                .Join(
                    _bancoContext.Contatos,
                    requisicao => requisicao.requisicao.id_cliente,
                    cliente => cliente.Id,
                    (requisicao, cliente) => new RequisicaoViewModel
                    {
                        requisicao = requisicao.requisicao,
                        statusReq = requisicao.statusReq,
                        usuario = requisicao.usuario,
                        cliente = cliente
                    })
                .Join(
                    _bancoContext.prioridade,
                    requisicao => requisicao.requisicao.id_prioridade,
                    prioridade => prioridade.id_prioridade,
                    (requisicao, prioridade) => new RequisicaoViewModel
                    {
                        requisicao = requisicao.requisicao,
                        statusReq = requisicao.statusReq,
                        usuario = requisicao.usuario,
                        cliente = requisicao.cliente,
                        prioridade = prioridade
                    })
                .ToList();

            return result;
        }

        public List<UsuarioModel> BuscaResponsavelPorNome(string txtSugestao)
        {
           return _bancoContext.Usuarios.Where(u => u.Nome.Contains(txtSugestao)).ToList();
        }


        // ----------------------------------------------- Metodos de alteração de adição/alteração de dados ---------------------------------------------------------------------------------
        public RequisicaoModel CriarRequisicao(RequisicaoModel requisicao)
        {
            requisicao.data_cadastro = DateTime.Now;

            switch (requisicao.id_prioridade)
            {   
                // Baixa prioridade
                case 1:
                    requisicao.data_entrega = DateTime.Now.AddDays(20);
                    break;
                // Media prioridade
                case 2:
                    requisicao.data_entrega = DateTime.Now.AddDays(10);
                    break;
                // Alta prioridade
                case 3:
                    requisicao.data_entrega = DateTime.Now.AddDays(5);
                    break;
                // Urgente prioridade
                case 4:
                    requisicao.data_entrega = DateTime.Now.AddDays(1);
                    break;
            }

            _bancoContext.requisicoes.Add(requisicao);

            _bancoContext.SaveChanges();

            return requisicao;
        }

    }
}
