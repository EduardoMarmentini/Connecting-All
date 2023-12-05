namespace ControleDeContatos.Repositorio
{
    public interface IPhotoRepositorio
    {
        // Metodo para adicionar a foto de perfil ao cadastrar o contato
        Task UploadPhoto(int id, IFormFile picture_upload);
        // Metodo para alterar a foto de perfil do contato ao editar
        Task AlterarPhoto(int id, IFormFile picture_upload);
        // Metodo para excluir a foto de perfil ao excluir o contato
        Task ExcluirPhoto(int id);
    }
}
