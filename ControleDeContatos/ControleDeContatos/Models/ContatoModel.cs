using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        // Torna o campo como obrigatorio
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }
        // Torna o campo como obrigatorio
        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "E-mail invalido para registro")]
        public string Email { get; set; }
        // Torna o campo como obrigatorio
        [Required(ErrorMessage = "Digite o celular do contato.")]
        [Phone(ErrorMessage = "Celular invalido para registro")]
        public string Celular { get; set; }

        // Busca o caminho da imagem salva no projeto
        public string GetImagePath()
        {
            string foto = $"{Id}.jpeg";
            string caminho = $"/img/ContatosPhotos/{foto}";
            return caminho;
        }
        /*
            OBS: O metodo de buscar a foto de perfil do contato está no arquivo da model pois todo arquivo que incorporar a model tera acesso a ela 
        */

    }
}