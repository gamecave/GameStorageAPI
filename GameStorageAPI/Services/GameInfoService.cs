using Google.Cloud.Spanner.Data;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// TODO: Make this class work

namespace GameStorageAPI.Services
{
    public class GameInfoService
    {

        private StorageClient storage;
        private readonly string CONNECTION_STRING = $"Data Source=projects/gamecave/instances/gamecave-game-info/databases/game_info";

        public GameInfoService()
        {   
        }

        public async Task<IEnumerable<string>> GetGameNames()
        {
            using (var connection = new SpannerConnection(CONNECTION_STRING))
            {
                var cmd = connection.CreateSelectCommand(
                    @"SELECT path FROM game");
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    //return await reader.ReadAsync();
                    // todo: fix
                    return null;
                }
            }
        }

        public async Task<string> GetGamePath(string name)
        {
            using (var connection = new SpannerConnection(CONNECTION_STRING))
            {
                var cmd = connection.CreateSelectCommand(
                    @"SELECT path FROM game WHERE name = " + name);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    //return await reader.ReadAsync();
                    // todo: fix
                    return "";
                }
            }
        }

        public async Task<bool> AddGameInfo(string name, string author, string path)
        {
            using (var connection = new SpannerConnection(CONNECTION_STRING))
            {
                var cmd = connection.CreateDmlCommand(
                    $@"INSERT INTO game (name, author, path) VALUES ('{name}', '{author}', '{path}')");
                int rowCount = await cmd.ExecuteNonQueryAsync();
                return rowCount == 1;
            }
        }
    }
}
