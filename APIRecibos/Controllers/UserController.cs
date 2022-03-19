using APIRecibos.DbAccess;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRecibos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RecibosDbContext _context;

        public UserController(RecibosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
             
        }


        [HttpGet("Logear/{email}/{pass}")]
        public async Task<IActionResult> GetUser(string email,string pass)
        {
            try
            {
                var user = await _context.Users.Where(x=>x.email == email && x.pass==pass).FirstOrDefaultAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] EUser user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] EUser user)
        {
            try
            {
                if (user == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var eBd = await _context.Users.FindAsync(user.id);

                if (eBd != null)
                {
                    eBd.pass = user.pass;
                    eBd.email = user.email;
                    eBd.name = user.name;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }


              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
                if (user!=null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


    }
}
