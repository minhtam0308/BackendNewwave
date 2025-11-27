using Azure.Core;
using Backend.Data;
using Backend.Entitise;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class ImageServices(ImageDBContext context) : IImageServices
    {
        public async Task<BookImage?> GetBookImage(Guid idImage)
        {
            try
            {
                var image = await context.BookImages
                          .FirstOrDefaultAsync(x => x.Id == idImage);
                return image;
            }
            catch (Exception ex)
            {
                Log.Information("Error GetBookImage {t}", ex);
                return null;
            }
        }

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

        public async Task<int?> PutImage(Guid id, byte[] image)
        {
            try
            {

                var editImage = await context.BookImages.FirstOrDefaultAsync(b => b.Id == id);
                if (editImage == null)
                {
                    return 2;
                }
                editImage.image = image;
                await context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error PutImage {t}", ex);
                return 1;
            }

        }

        public async Task<int?> DelImage(Guid idImage)
        {
            try
            {
                var image = await context.BookImages
                          .FirstOrDefaultAsync(x => x.Id == idImage);
                if (image == null)
                {
                    return 2;
                }
                context.BookImages.Remove(image);
                context.SaveChanges(); 
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information("Error GetBookImage {t}", ex);
                return 1;
            }
        }

    }
}
