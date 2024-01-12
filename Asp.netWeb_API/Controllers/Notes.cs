using Asp.netWeb_API.Data;
using Asp.netWeb_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Routing;

namespace Asp.netWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Notes : ControllerBase
    {
        private readonly AppDbContext _context;
        public Notes(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<List<Note>> GetAll()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var Token = handler.ReadJwtToken(authHeader);
            string email = Token.Claims.First(c => c.Type == "email").Value;

            var user = await _context.Users.Include(u => u.Notes).FirstOrDefaultAsync(e => e.Email == email);
            return user.Notes;
        }
        [Authorize]
        [HttpPost]
        public async Task<IResult> Create(Note note)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var Token = handler.ReadJwtToken(authHeader);
            string email = Token.Claims.First(c => c.Type == "email").Value;

            var user = await _context.Users.Include(u=>u.Notes).FirstOrDefaultAsync(e => e.Email == email);
            note.DateStamp = DateTime.UtcNow;
            user.Notes.Add(note);
            if(_context.SaveChanges() > 0)
            {
                return Results.Ok(new { message = "Note created successfully" });
            }
            else
            {
                return Results.StatusCode(500);
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<IResult> Edit(Note note)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var Token = handler.ReadJwtToken(authHeader);
            string email = Token.Claims.First(c => c.Type == "email").Value;


            var user = await _context.Users.Include(u => u.Notes).FirstOrDefaultAsync(e => e.Email == email);
            var oldNote = user.Notes.FirstOrDefault(d => d.Id == note.Id);
            if(oldNote == null)
            {
                return Results.NotFound();
            }

            oldNote.Title = note.Title;
            _context.Entry(oldNote).State = EntityState.Modified;
            if (_context.SaveChanges() > 0)
            {
                return Results.Ok(new { message = "Note changed successfully" });
            }
            else
            {
                return Results.StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IResult> Delete(int id = -1)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var Token = handler.ReadJwtToken(authHeader);
            string email = Token.Claims.First(c => c.Type == "email").Value;
            
            var user = await _context.Users.Include(u => u.Notes).FirstOrDefaultAsync(e => e.Email == email);
            var delNote = user.Notes.FirstOrDefault(d => d.Id == id);
            if (delNote == null)
            {
                return Results.NotFound();
            }
            _context.Remove(delNote);
            if (_context.SaveChanges() > 0)
            {
                return Results.Ok(new { message = "Note deleted successfully" });
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }
}
