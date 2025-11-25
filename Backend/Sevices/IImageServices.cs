using Backend.Entitise;
using Backend.Models;

namespace Backend.Sevices
{
    public interface IImageServices
    {
        Task<Guid?> PostAddImage(ImageRequest request);
    }
}
