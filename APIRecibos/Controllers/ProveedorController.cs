using APIRecibos.DbAccess;
using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRecibos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly RecibosDbContext _context;

        public ProveedorController(RecibosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProveedors()
        {
            try
            {
                var Proveedors = await _context.Proveedores.ToListAsync();

                return Ok(Proveedors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            try
            {
                var Proveedor = await _context.Proveedores.FindAsync(id);

                return Ok(Proveedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }          
             
        }

        [HttpPost]
        public async Task<IActionResult> InsertProveedor([FromBody] IEProveedor Proveedor)
        {
            try
            {
                if (Proveedor == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.AddAsync(Proveedor);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProveedor([FromBody] IEProveedor Proveedor)
        {
            try
            {
                if (Proveedor == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var eBd = await _context.Proveedores.FindAsync(Proveedor.id);

                if (eBd != null)
                {
                    eBd.proveedor = Proveedor.proveedor;

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
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            try
            {
                var Proveedor = await _context.Proveedores.FirstOrDefaultAsync(x => x.id == id);
                if (Proveedor!=null)
                {
                    _context.Proveedores.Remove(Proveedor);
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
