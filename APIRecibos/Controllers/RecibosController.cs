using APIRecibos.DbAccess;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRecibos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecibosController : ControllerBase
    {
        private readonly RecibosDbContext _context;

        public RecibosController(RecibosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReciboss()
        {
            try
            {
                var Reciboss = await _context.Recibos.ToListAsync();

                return Ok(Reciboss);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRecibos(int id)
        {
            try
            {
                var Recibos = await _context.Recibos.FindAsync(id);

                return Ok(Recibos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
             
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecibos(ERecibos Recibos)
        {
            try
            {
                if (Recibos == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (Recibos.id==0 || Recibos.id == null)
                {
                    var cons = await _context.Recibos.OrderByDescending(x=>x.consecutivo).FirstOrDefaultAsync();

                    if (cons!=null)
                    {
                        Recibos.consecutivo = cons.consecutivo + 1;
                    }
                   
                }
               

                await _context.AddAsync(Recibos);
                await _context.SaveChangesAsync();

                return Ok(Recibos.consecutivo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecibos(ERecibos Recibos)
        {
            try
            {
                if (Recibos == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var eBd = await _context.Recibos.FindAsync(Recibos.id);

                if (eBd != null)
                {
                    eBd.proveedorId = Recibos.proveedorId;
                    eBd.consecutivo = Recibos.consecutivo;
                    eBd.monto  = Recibos.monto;
                    eBd.fecha = Recibos.fecha;
                    eBd.comentario = Recibos.comentario;
                    eBd.monedaId = Recibos.monedaId;

   
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecibos(int id)
        {
            try
            {
                var Recibos = await _context.Recibos.FindAsync(id);
                if (Recibos!=null)
                {
                    _context.Recibos.Remove(Recibos);
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
