using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StocksWebApp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                using (Stream strm = file?.OpenReadStream())
                {
                    return Ok($"File uploaded had {file?.Length ?? -1} bytes");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}