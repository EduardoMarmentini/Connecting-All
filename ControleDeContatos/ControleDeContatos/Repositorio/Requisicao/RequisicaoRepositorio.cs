using ControleDeContatos.Data;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;


namespace ControleDeContatos.Repositorio.Requisicao
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
                            requisicao = requisicao.requisicao, // Aqui eu acesso o objeto RequisicaoViewModel e acesso o atributo requisicao que é uma instancia da model RequisicaoModel
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
        }

        public List<RequisicaoViewModel> BuscarRequisicaoPorFiltros(int id_requisicao, string titulo_requisicao, int id_usuario, int id_cliente, int id_estado_req)
        {
            // Criar uma lista para salvar a query da consulta
            List<RequisicaoModel> query = _bancoContext.requisicoes.ToList();

            // Caso informado busca pelo numero da requisicao
            if (id_requisicao != 0)
            {
                query = query.Where(e => e.id_requisicao == id_requisicao).ToList();
            }
            // Caso informado busca pela fila do usuario 
            if (id_usuario != 0)
            {
                query = query.Where(e => e.id_usuario == id_usuario).ToList();
            }

            // Caso informado busca pelo cliente que solicitou a requisicao
            if (id_cliente != 0)
            {
                query = query.Where(e => e.id_cliente == id_cliente).ToList();
            }

            // Caso for selecionado o estado da requisição
            if (id_estado_req != 0)
            {   
                // Busca todas as requisicoes que estao em aberto 
                if (id_estado_req == 1)
                {
                    query = query.Where(e => e.status != 12).ToList();
                }
                // Busca todas as requisicoes fechadas / concluidas
                else
                {
                    query = query.Where(e => e.status == 12).ToList();
                }
            }

            // Caso informado busca pelo titulo da requisicao
            if (!string.IsNullOrEmpty(titulo_requisicao))
            {
                query = query.Where(e => e.titulo_requisicao.Contains(titulo_requisicao)).ToList();
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
        public RequisicaoModel CriarRequisicao(CriarReqModel requisicao)
        {

            requisicao.data_cadastro = DateTime.Now;

            // De acordo com o grau de prioridade posto pelo usuario ele seta a data de entrega da demanda
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

            // Criar a varivale que guarda os valores da requisicao
            RequisicaoModel new_requisicao = new RequisicaoModel
            {
                id_usuario = requisicao.id_usuario,
                id_responsavel = requisicao.id_responsavel,
                id_cliente = requisicao.id_cliente,
                id_prioridade = requisicao.id_prioridade,
                titulo_requisicao = requisicao.titulo_requisicao,
                status = requisicao.status,
                data_cadastro = requisicao.data_cadastro, 
                data_entrega = requisicao.data_entrega,
                data_conclusao = requisicao.data_entrega,
                horas_trabalhadas = requisicao.horas_trabalhadas
            };

            // Salva a requisição no banco de dados
            _bancoContext.requisicoes.Add(new_requisicao);

            _bancoContext.SaveChanges();

            // Busca a ultima requisicao criada que seria a que foi gerada acima e salva se  id 
            requisicao.id_requisicao = _bancoContext.requisicoes.OrderByDescending(r => r.id_requisicao).Select(r => r.id_requisicao).FirstOrDefault();

            // Cria a variavel que guarda os valores da ocorrencia 
            RequisicaoOcorrenciaModel new_ocorrencia = new RequisicaoOcorrenciaModel
            {
                id_requisicao = requisicao.id_requisicao,
                detalhe_ocorrencia = requisicao.detalhe_ocorrencia,
                id_usuario = requisicao.id_usuario,
                id_status = requisicao.status,
                data_ocorrencia = requisicao.data_cadastro,
                log_ocorrencia = $"Criada requisição : ( {new_requisicao.id_requisicao} ) | Fila Usuario: ( {requisicao.id_usuario} ) | Status ( {requisicao.status} )"
            };

            // Salva ele no banco de dados
            _bancoContext.requisicao_ocorrencia.Add(new_ocorrencia);

            _bancoContext.SaveChanges();

            return new_requisicao;
        }

        public RequisicaoOcorrenciaModel EncaminharRequisicao(RequisicaoOcorrenciaModel registro)
        {
            // Dentro desse metodo criamos um registro de encaminhamento da demanda onde contem uma ocorrencia sobre o motivo do encaminhamento da demanda;

            // Primeiro mandamos a demanda para a fila que desejamos com o status que ela se encontra.
            RequisicaoModel requisicaoDB = _bancoContext.requisicoes.FirstOrDefault(req => req.id_requisicao == registro.id_requisicao);

            if (requisicaoDB == null) throw new Exception("Houve um erro no encaminhamento da demanda");

            // Atualiza o usuario e o status da requisicao
            requisicaoDB.id_usuario = registro.id_usuario;
            requisicaoDB.status = registro.id_status;

            // Atualiza os dados
            _bancoContext.requisicoes.Update(requisicaoDB);
            
            string log = $"Requisicao : ( {registro.id_requisicao} ) | Fila Usuario: ( {registro.id_usuario} ) | Status ( {registro.id_status} )";

            registro.data_ocorrencia = DateTime.Now;
            registro.log_ocorrencia = log;

            _bancoContext.requisicao_ocorrencia.Add(registro);

            _bancoContext.SaveChanges();

            return registro;
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
