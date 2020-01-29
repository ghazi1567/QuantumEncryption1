using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quantum.API.Contexts;
using Quantum.API.Models;

namespace Quantum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserBasesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserBases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBase>>> GetUserBases()
        {
            return await _context.UserBases.ToListAsync();
        }
        // GET: api/UserBases
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<UserBase>>> GetUserBasesByUserId(int id)
        {
            return await _context.UserBases.Where(x=>x.UserId == id).ToListAsync();
        }
        // GET: api/UserBases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBase>> GetUserBase(int id)
        {
            var userBase = await _context.UserBases.FindAsync(id);

            if (userBase == null)
            {
                return NotFound();
            }

            return userBase;
        }

        // PUT: api/UserBases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBase(int id, UserBase userBase)
        {
            if (id != userBase.UserBaseId)
            {
                return BadRequest();
            }

            _context.Entry(userBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserBases
        [HttpPost]
        public async Task<ActionResult<UserBase>> PostUserBase(UserBase userBase)
        {
            _context.UserBases.Add(userBase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserBase", new { id = userBase.UserBaseId }, userBase);
        }

        // DELETE: api/UserBases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserBase>> DeleteUserBase(int id)
        {
            var userBase = await _context.UserBases.FindAsync(id);
            if (userBase == null)
            {
                return NotFound();
            }

            _context.UserBases.Remove(userBase);
            await _context.SaveChangesAsync();

            return userBase;
        }

        private bool UserBaseExists(int id)
        {
            return _context.UserBases.Any(e => e.UserBaseId == id);
        }
    }
}
