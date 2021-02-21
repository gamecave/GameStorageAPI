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
    public class GameController : Controller
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
        public IActionResult DevForm()
        {
            return View("DevForm");
        }

        [HttpGet("Names")]
        public async Task<IEnumerable<string>> GetGameNames()
        {
            throw new NotImplementedException();
            return await infoService.GetGameNames();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetGameByName([FromRoute] string name)
        {
            throw new NotImplementedException();
            var path = await infoService.GetGamePath(name);
            var memory = await fileService.GetFile(path);
            return File(memory, "application/octet-stream");
        }

        [HttpGet("path/{path}")]
        public async Task<IActionResult> GetGameByPath([FromRoute] string path)
        {
            var memory = await fileService.GetFile(path);
            return File(memory, "application/octet-stream");
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadGame(IFormFile file, string name, string author)
        {
            // TODO: fix the database stuff then uncomment the next line
            // await infoService.AddGameInfo(name, author, file.FileName);
            // TODO: should we should probably add the game name and author, then get the unique key from the db to use as the path
            _logger.LogInformation(file.FileName);
            await fileService.SaveFile(file);
            return View("DevForm");
        }
    }
}
