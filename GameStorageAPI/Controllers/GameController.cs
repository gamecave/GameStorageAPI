using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using GameStorageAPI.Services;

namespace GameStorageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private GameFileService fileService;
        private GameInfoService infoService;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
            fileService = new GameFileService();
            infoService = new GameInfoService();
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetGameNames()
        {
            return await infoService.GetGameNames();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetGame([FromRoute] string name)
        {
            var path = await infoService.GetGamePath(name);
            var stream = await fileService.GetFile(path);
            return File(stream, "application/octet-stream");
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task UploadGame(IFormFile file, string name, string author)
        {
            // TODO: should we should probably add the game name and author, then get the unique key from the db to use as the path
            await infoService.AddGameInfo(name, author, file.FileName);
            await fileService.SaveFile(file);
        }
    }
}
