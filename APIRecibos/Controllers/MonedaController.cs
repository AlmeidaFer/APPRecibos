using APIRecibos.DbAccess;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRecibos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonedaController : ControllerBase
    {
        private readonly RecibosDbContext _context;

        public MonedaController(RecibosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonedas()
        {
            try
            {
                var Monedas = await _context.Monedas.ToListAsync();

                return Ok(Monedas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMoneda(int id)
        {
            try
            {
                var Moneda = await _context.Monedas.FindAsync(id);

                return Ok(Moneda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
             
        }

        [HttpPost]
        public async Task<IActionResult> InsertMoneda([FromBody] IEMoneda Moneda)
        {
            try
            {
                if (Moneda == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.AddAsync(Moneda);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }

        [HttpPut]
        public async Task<IActionResult> UpdateMoneda([FromBody] IEMoneda Moneda)
        {
            try
            {
                if (Moneda == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var eBd = await _context.Monedas.FindAsync(Moneda.id);

                if (eBd != null)
                {
                    eBd.clave = Moneda.clave;
                    eBd.moneda = Moneda.moneda;
   
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
        public async Task<IActionResult> DeleteMoneda(int id)
        {
            try
            {
                var Moneda = await _context.Monedas.FirstOrDefaultAsync(x => x.id == id);
                if (Moneda!=null)
                {
                    _context.Monedas.Remove(Moneda);
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
