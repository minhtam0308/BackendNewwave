using Azure.Core;
using Backend.Data;
using Backend.Entities;
using Backend.Interface.Service;
using Backend.Models.Image;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

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
                BookImage newImage = new BookImage() { Image = request.Image };
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
                editImage.Image = image;
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

        public async Task<byte[]?> GetImageGeneral()
        {
            try
            {
                var image = await context.BookImages
                          .FirstOrDefaultAsync(x => x.Id == Guid.Parse("258d5e1a-ff57-4092-2a5d-08de2e43c05d"));
                if (image == null)
                {
                    return null;
                }
                return image.Image;
            }
            catch (Exception ex)
            {
                Log.Information("Error GetBookImage {t}", ex);
                return null;
            }
        }
    }
}
