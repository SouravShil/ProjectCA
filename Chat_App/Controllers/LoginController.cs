using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chap_App.Models;

namespace Chap_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public LoginController(ChatAppContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers([FromBody] Users user)
        {

            var user1 = await _context.Users.FindAsync(user.Email);
            if (user1 == null)
            {
                return NotFound();
            }
            else if (user1.Password == user.Password)
            {
                return user1;
            }
            else return BadRequest();
        }
    }
}
