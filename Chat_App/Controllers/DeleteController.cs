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
    public class DeleteController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public DeleteController(ChatAppContext context)
        {
            _context = context;
        }
        // DELETE: api/Delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUsers([FromBody] Users user)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(user.Email);
            if (users == null)
            {
                return NotFound();
            }
            else if(users.Password==user.Password)
            {
                _context.Users.Remove(users);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
            
        }

    }
}
