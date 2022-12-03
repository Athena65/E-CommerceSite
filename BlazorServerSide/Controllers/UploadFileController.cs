using BlazorServerSide.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BlazorServerSide.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IProductService _productService;

        public UploadFileController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave=Path.Combine(Directory.GetCurrentDirectory(),folderName);    
                if(file.Length> 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath=Path.Combine(pathToSave,fileName);
                    var dbPath=Path.Combine(folderName,fileName);   

                    using(var stream= new FileStream(fullPath,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;  
                return BadRequest(response);    
            }
        }
    }
}
