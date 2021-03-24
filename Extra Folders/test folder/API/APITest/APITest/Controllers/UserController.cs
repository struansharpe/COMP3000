using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly string[] Usernames = new[]
       {
            "johnnybiglad", "bessieboo", "chandelier", "Alicewonder", "Marthamary", "Steveo", "Stephanie"
        };

        private static readonly string[] First_names = new[]
    {
            "John", "Bessie", "Chandler", "Alice", "Martha", "Steve", "Stephanie"
        };

        private static readonly string[] Last_names = new[]
    {
            "flotsom", "berti", "ross", "roth", "bomble", "clark", "jett"
        };

        private static readonly string[] Password = new[]
    {
            "Never", "fortitude", "memes", "Korbanth", "Intermediate", "Encapsulate", "Jetengine"
        };
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
public IEnumerable<User> Get()
{
          
            return Enumerable.Range(1, 5).Select(index => new User
            {

                Username = Usernames[index],
                First_name = First_names[index],
                Last_name = Last_names[index],
                Password = Password[index]
                
            })
    .ToArray();
}
}
}