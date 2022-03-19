using APIRecibos.DbAccess;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "GetUsers")]
        public void GetUser()
        {
            var list = new List<EUser>();
          
                list = (from u in _context.Users
                        select new EUser
                        {
                            id = u.id,
                            email = u.email,
                            pass = u.pass,
                            name = u.name
                        }).ToList();

                
            

            var fff = list;
        }
    }
}
