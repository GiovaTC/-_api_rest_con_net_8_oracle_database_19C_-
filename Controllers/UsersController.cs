using Microsoft.AspNetCore.Mvc;
using oracle_api_demo.Data;
using oracle_api_demo.Models;   

namespace oracle_api_demo.Controllers
{
    [ApiController]
    [Route("api/[users]")]
    public class UsersController : ControllerBase
    {
        private readonly OracleRepository _repository;

        public UsersController(OracleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            _repository.InsertUser(user);
            return Ok(new { message = "usuario registrado correctamente!" });
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _repository.GetUsers();
            return Ok(users);
        }
    }
}   
