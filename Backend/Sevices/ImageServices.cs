using Backend.Data;
using Backend.Entitise;
using Backend.Models;
using Serilog;

namespace Backend.Sevices
{
    public class ImageServices(ImageDBContext context) : IImageServices
    {
        public async Task<Guid?> PostAddImage(ImageRequest request)
        {
            try
            {
                BookImage newImage = new BookImage() { image = request.image };
                await context.BookImages.AddAsync(newImage);
                await context.SaveChangesAsync();
                return newImage.Id;
            }
            catch (Exception ex)
            {
                Log.Information("Error PostAddImage {t}", ex);
                return null;
            }


        }
    }
}
