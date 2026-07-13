using ApiCoreID.Middleware;
using ApiCoreID.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreID.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FotoController : Controller
    {

        private readonly PhotosServices _photosService;
        private readonly GuidServices _guidService;
        private readonly ILogger<FotoController> _logger;
        public FotoController(PhotosServices photosServices, GuidServices guidServices, ILogger<FotoController> logger)
        {
            this._photosService = photosServices;
            this._guidService = guidServices;
            this._logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> Create([FromBody] Models.Photo model)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new
                {
                    Error = "campos vacíos o inválidos.",
                    Detalles = errores
                });
            }

            var result = await _photosService.InsertFoto(model);
            return result;
        }
        [HttpGet("GetPhotos")]
        public async Task<ActionResult<List<Models.Photo>>> GetAllPhotos()
        {
            var result = await _photosService.GetPhotos();
            return result;
        }

        [HttpDelete("DeletePhotos")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _photosService.DeletePhoto(id);
            return result;
        }

        [HttpPost("UpdatePhotos")]
        public async Task<ActionResult<bool>> UpdatePhotos([FromBody] Models.Photo model)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new
                {
                    Error = "campos vacíos o inválidos.",
                    Detalles = errores
                });
            }

            var result = await _photosService.UpdatePhotos(model);
            return result;
        }

        //[HttpGet("Guid")]
        //public async Task<ActionResult<string>> Guid()
        //{
        //    var logMessage = $"Controller: {_guidService.resultadoGuid}";
        //    _logger.LogInformation(logMessage);
        //    return Ok(logMessage);
        //}
    }
}
