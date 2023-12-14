using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
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
        // Para melhor tratamento do sistema, informar uma senha especifica do cliente se torna opcional, caso não informado seta um modelo de senha padrão
        public string? Password { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteração { get; set; }


        public bool ValidaSenha(string password)
        {
            return Password == password.GerarHash();
        }
        public void SetSenhaHash() 
        {
            // Caso o usuario não informe a senha na hora do cadastro a senha por padrão a ser enviada será o modelo Con@Ano_do_Cadastro
            if (Password == null)
            {
                Password = $"Con@{DateTime.Now.Year}";
            }

            Password = Password.GerarHash();
        }
        // Gera a nova senha para ser acesssada ao esquecer a senha
        public string GerarNovaSenha() 
        {
            string novasenha = Guid.NewGuid().ToString().Substring(0, 8);

            Password = novasenha.GerarHash();

            return novasenha;
        }
        
        // Pega somente o primeiro nome do usuario para ser exibido na mensagem de login
        public string GetNome() 
        {
            string inputString = Nome;

            // Encontrar o índice do primeiro espaço em branco
            int indiceEspaco = inputString.IndexOf(' ');

            // Extrair o primeiro nome usando Substring
            string primeiroNome = indiceEspaco != -1 ? inputString.Substring(0, indiceEspaco) : inputString;

            return primeiroNome;
        }

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
