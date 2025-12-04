using Backend.Interface.Service;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageServices imageServices) : ControllerBase
    {
        [Authorize(Roles = "admin, user")]
        [HttpPost("postCreateImage")]
        public async Task<ActionResult> PostCreateImage(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = file
                });
            }
            using var tempMemory = new MemoryStream();
            await file.CopyToAsync(tempMemory);
            var imageByte = tempMemory.ToArray();

            var result = await imageServices.PostAddImage(new ImageRequest() { image = imageByte});
            if(result is null)
            {
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            }
            return Ok(new
            {
                EC = 0,
                EM = result
            });


        }

        [HttpGet("getImage")]
        public async Task<ActionResult> GetImage(string idImage)
        {
            if (!Guid.TryParse(idImage, out var guidId))
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Guid Wrong"
                });
            }


            var result = await imageServices.GetBookImage(guidId);
            if (result is null)
            {
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            }
            return File(result.image, "image/png");


        }

        [Authorize(Roles = "admin")]
        [HttpPut("putImage")]
        public async Task<ActionResult> PutImage( IFormFile file,[FromForm] string idImage)
        {
            if (file is null || file.Length == 0)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = file
                });
            }
            if (!Guid.TryParse(idImage, out var guidId))
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Guid Wrong"
                });
            }

            using var tempMemory = new MemoryStream();
            await file.CopyToAsync(tempMemory);
            var imageByte = tempMemory.ToArray();

            var result = await imageServices.PutImage(guidId, imageByte);
            if (result == 1)
            {
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            }
            if (result == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Image is not exist"
                });
            }
            return Ok(new
            {
                EC = 0,
                EM = "Ok"
            });


        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteImage")]
        public async Task<ActionResult> DelImage(string idImage)
        {

            if (!Guid.TryParse(idImage, out var guidId))
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Guid Wrong"
                });
            }



            var result = await imageServices.DelImage(guidId);
            if (result == 1)
            {
                return Ok(new
                {
                    EC = 1,
                    EM = "Error from BE"
                });
            }
            if (result == 2)
            {
                return Ok(new
                {
                    EC = 2,
                    EM = "Image is not exist"
                });
            }
            return Ok(new
            {
                EC = 0,
                EM = "Ok"
            });


        }
    }
}
