namespace ControleDeContatos.Repositorio
{
    public interface IPhotoRepositorio
    {
        Task UploadPhoto(int id, IFormFile picture_upload);

        Task AlterarPhoto(int id, IFormFile picture_upload, IFormFile foto_perfil);
    }
}
