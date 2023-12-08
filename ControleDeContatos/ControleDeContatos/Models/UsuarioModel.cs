using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Informa o tipo do usuário")]
        public required PerfilEnum Tipo_Usuario { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuário")]
        public required string Password { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteração { get; set; }
        // Busca o caminho da imagem salva no projeto
        public string GetImagePath()
        {
            string foto = $"{Id}.jpeg";
            string caminho = $"/img/UsuariosPhotos/{foto}";
            return caminho;
        }
        /*
            OBS: O metodo de buscar a foto de perfil do usuario está no arquivo da model pois todo arquivo que incorporar a model tera acesso a ela 
        */
    }
}
