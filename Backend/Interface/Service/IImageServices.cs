using Backend.Entities;
using Backend.Models;

namespace Backend.Interface.Service
{
    public interface IImageServices
    {
        Task<Guid?> PostAddImage(ImageRequest request);
        Task<BookImage?> GetBookImage(Guid idImage);
        Task<int?> PutImage(Guid id, byte[] image);
        Task<int?> DelImage(Guid id);
        Task<byte[]?> GetImageGeneral();
    }
}
