﻿using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.Repositorio.Requisicao
{
    public interface IRequisicaoRepositorio
    {
        public List<RequisicaoViewModel> BuscarRequisicaoPorUsuario(int id_usuario_logado);

        public List<RequisicaoViewModel> BuscarRequisicaoPorFiltros(int id_requisicao, string titulo_requisicao, int id_usuario, int id_cliente, int id_estado_req);

        public List<RequisicaoViewModel> BuscarOcorrenciasPorReq(int id_requisicao);

        public List<UsuarioModel> BuscaResponsavelPorNome(string txtSugestao);

        //--------------------------------------------------------------- Metodos de alteração de adição/alteração de dados --------------------------------------------------------------------------
        public RequisicaoModel CriarRequisicao(CriarReqModel requisicao);

        public RequisicaoOcorrenciaModel EncaminharRequisicao(RequisicaoOcorrenciaModel registro);

        // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
