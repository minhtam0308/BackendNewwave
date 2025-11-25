using Backend.Models;
using Backend.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageServices imageServices) : ControllerBase
    {
        [Authorize(Roles = "admin")]
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
    }
}
