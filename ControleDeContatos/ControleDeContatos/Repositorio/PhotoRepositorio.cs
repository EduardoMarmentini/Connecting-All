
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class PhotoRepositorio : IPhotoRepositorio
    {
        private string caminhoServidor;
        public PhotoRepositorio(IWebHostEnvironment sistema)
        {
            caminhoServidor = sistema.WebRootPath;
        }

        Task IPhotoRepositorio.UploadPhoto(int id, IFormFile picture_upload)
        {
            if (picture_upload != null)
            {
                string caminhoDaimagem = Path.Combine(caminhoServidor, "img");

                // Salva a imagem na pasta 'img' com o nome do arquivo sendo o id_do_contato.extensão_do_arquivo
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), caminhoDaimagem, id + Path.GetExtension(picture_upload.FileName));
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    picture_upload.CopyTo(stream);
                }
            }

            return Task.CompletedTask;
        }

        Task IPhotoRepositorio.AlterarPhoto(int id, IFormFile picture_upload)
        {
            // Caso o usuario tenha feito o upload de uma nova foto ele substitui a antiga por ela
            if(picture_upload != null)
            {
                string caminhoDaimagem = Path.Combine(caminhoServidor, "img");

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), caminhoDaimagem, id + Path.GetExtension(picture_upload.FileName));

                // Verifica se o arquivo já existe
                if (File.Exists(imagePath))
                {
                    // Se o arquivo existir, exclua-o
                    File.Delete(imagePath);
                }
                // Salva a nova imagem de perfil na pasta 'img' com o nome do arquivo sendo o id_do_contato.extensão_do_arquivo
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    picture_upload.CopyTo(stream);
                }
                
            }
            return Task.CompletedTask;
        }

    }
}
