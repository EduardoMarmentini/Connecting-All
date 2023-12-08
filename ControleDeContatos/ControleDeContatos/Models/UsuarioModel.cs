using ControleDeContatos.Enums;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilEnum Tipo_Usuario { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteração { get; set; }
        // Busca o caminho da imagem salva no projeto
        public string GetImagePath()
        {
            string foto = $"{Id}.jpeg";
            string caminho = $"/img/ContatosPhotos/{foto}";
            return caminho;
        }
        /*
            OBS: O metodo de buscar a foto de perfil do usuario está no arquivo da model pois todo arquivo que incorporar a model tera acesso a ela 
        */
    }
}
