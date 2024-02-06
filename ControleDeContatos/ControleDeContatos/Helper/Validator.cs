using ControleDeContatos.Data;

namespace ControleDeContatos.Helper
{
    public class Validator : IValidator
    {
        private readonly BancoContext _bancoContext;

        public Validator(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public bool ValidaContHora(int id_status)
        {
            string contabiliza_horas = Convert.ToString(_bancoContext.status_requisicao.Where(status_req => status_req.id_status == id_status).Select(status_req => status_req.contabiliza_horas).FirstOrDefault());

            // Caso o valor que venha do status passado for sim ele retorna verdadeiro e permite o calculo de horas
            return contabiliza_horas == "S";
        }
    }
}
