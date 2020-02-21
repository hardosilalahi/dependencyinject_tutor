using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using webapi_DependenInject.Models;

namespace webapi_DependenInject.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {


        private readonly IDatabase _database;

        public PostsController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _database.readPost();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _database.readByID(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Posts post)
        {
            return Ok(_database.createPost(post));
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]JsonPatchDocument<Posts> data){
            _database.updatePost(id, data);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id){
            _database.deletePost(id);
        }
    }
}
