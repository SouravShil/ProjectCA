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
    public class UpdateController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public UpdateController(ChatAppContext context)
        {
            _context = context;
        }

        // PUT: api/Update/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUsers(Users user)
        {
            var user1 = await _context.Users.FindAsync(user.Email);
            if (user1 == null)
            {
                return NotFound();
            }
            else if (user1.Password == user.Password)
            {
                user1.Name = user.Name;
                user1.DOB=user.DOB;
                _context.Entry(user1).State = EntityState.Modified;
                //return user1;
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
            }
            else return BadRequest();

            return Ok();
        }

        private bool UsersExists(string id)
        {
            return (_context.Users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
