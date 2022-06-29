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
    public class ResetPassController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public ResetPassController(ChatAppContext context)
        {
            _context = context;
        }

       
        [HttpPut]
        public async Task<IActionResult> PutUsers(Users user)
        {
            var user1 = await _context.Users.FindAsync(user.Email);
            user1.Password =user.Password;
            if (user1 == null)
            {
                return NotFound();
            }
            //_context.Entry(user.Password).State = EntityState.Modified;
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

        private bool UsersExists(string id)
        {
            return (_context.Users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
