using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersAlbumsService.Models;

namespace UsersAlbumsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataServiceHttpClient _dataService;
        public ValuesController(DataServiceHttpClient dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            var data = await _dataService.SendRequestAsync<IEnumerable<User>>(HttpMethod.Get, "users");
            return Ok(data);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            var data = await _dataService.SendRequestAsync<User>(HttpMethod.Get, $"users/{id}");
            return Ok(data);
        }

        [HttpGet("albums")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsAsync([FromQuery]int? userId)
        {
            var route = "albums";
            if (userId != null)
                route = $"albums?userId={userId}";
            var data = await _dataService.SendRequestAsync<IEnumerable<Album>>(HttpMethod.Get, route);
            return Ok(data);
        }

        [HttpGet("albums/{id}")]
        public async Task<ActionResult<Album>> GetAlbumByIdAsync(int id)
        {
            var data = await _dataService.SendRequestAsync<Album>(HttpMethod.Get, $"albums/{id}");
            return Ok(data);
        }
    }
}
