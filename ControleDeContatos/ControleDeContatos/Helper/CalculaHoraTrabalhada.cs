using ControleDeContatos.Data;
using ControleDeContatos.Models.Requisicao;

namespace ControleDeContatos.Helper
{
    public class CalculaHoraTrabalhada : ICalculaHoraTrabalhada
    {

        private readonly BancoContext _bancoContext;
        private readonly IValidator _validator;

        public CalculaHoraTrabalhada(BancoContext bancoContext, IValidator validator) {
            _bancoContext = bancoContext;
            _validator = validator;
        }

        TimeSpan ICalculaHoraTrabalhada.CalculaHoraTrabalhada(int id_requisicao)
        {

            RequisicaoOcorrenciaModel data = _bancoContext.requisicao_ocorrencia
            .Where(r => r.id_requisicao == id_requisicao)
            .OrderByDescending(r => r.data_ocorrencia)
            .FirstOrDefault();

            // Caso o status presente no ultimo encaminhamento permitir a contagem de horas ele realiza a conta e retorna a diferença para ser somada
            if (_validator.ValidaContHora(data.id_status))
            {
                DateTime hora_atual = DateTime.Now;

                TimeSpan diferencaDeTempo = hora_atual - data.data_ocorrencia;

                return diferencaDeTempo;
            }
            
            return TimeSpan.Zero;
        }
    }
}
