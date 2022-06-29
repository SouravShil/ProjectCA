using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chap_App.Models;
using System.Web;

namespace Chap_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ChatAppContext _context;

        public MessagesController(ChatAppContext context)
        {
            _context = context;
        }


        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
          if (_context.Messages == null)
          {
              return NotFound();
          }
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var message = await _context.Messages.FindAsync(id);

            if(message==null)
            {
                return NotFound();
            }

            return message;
        }


        // GET: api/Messages/email1/email2
        [HttpGet("{me},{inFrontOfMe}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessage(string me, string inFrontOfMe)
        {
            string SId = me;
            string RId = inFrontOfMe;
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var messages = await _context.Messages.Where(m=>((m.Sender==SId) && (m.Receiver==RId)) ||
                                                            ((m.Sender == RId) && (m.Receiver == SId))
                                                        ).ToListAsync();

            if (messages.Count() == 0)
            {
                return NotFound();
            }

            return messages;
        }



        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
          if (_context.Messages == null)
          {
              return Problem("Entity set 'ChatAppContext.Messages'  is null.");
          }

            message.CreatedDate = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

    }
}
