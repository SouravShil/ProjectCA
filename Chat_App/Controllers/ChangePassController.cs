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
    public class ChangePassController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public ChangePassController(ChatAppContext context)
        {
            _context = context;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(string id, Users user)
        {
            var user1 = await _context.Users.FindAsync(user.Email);
            if (user1 == null)
            {
                return NotFound();
            }
            else if (user1.Password == user.Password)
            {
                user1.Password = id;
                _context.Entry(user1).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(user.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();
            }
            return BadRequest();
        }
        private bool UsersExists(string id)
        {
            return (_context.Users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
