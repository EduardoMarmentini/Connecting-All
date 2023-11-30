namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        // Busca o caminho da imagem salva no projeto
        public string GetImagePath()
        {
            string foto = $"{Id}.jpeg";
            string caminho = $"/img/{foto}";
            return caminho;
        }
        /*
            OBS: O metodo de buscar a foto de perfil do contato está no arquivo da model pois todo arquivo que incorporar a model tera acesso a ela 
        */
    }
}
