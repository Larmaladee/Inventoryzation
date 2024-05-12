using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventorizationApi.Models;

namespace InventorizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentMovementsController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public EquipmentMovementsController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/EquipmentMovements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentMovement>>> GetEquipmentMovements()
        {
          if (_context.EquipmentMovements == null)
          {
              return NotFound();
          }
            return await _context.EquipmentMovements.ToListAsync();
        }

        // GET: api/EquipmentMovements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentMovement>> GetEquipmentMovement(int id)
        {
          if (_context.EquipmentMovements == null)
          {
              return NotFound();
          }
            var equipmentMovement = await _context.EquipmentMovements.FindAsync(id);

            if (equipmentMovement == null)
            {
                return NotFound();
            }

            return equipmentMovement;
        }

        // PUT: api/EquipmentMovements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentMovement(int id, EquipmentMovement equipmentMovement)
        {
            if (id != equipmentMovement.MovementId)
            {
                return BadRequest();
            }

            _context.Entry(equipmentMovement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentMovementExists(id))
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

        // POST: api/EquipmentMovements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipmentMovement>> PostEquipmentMovement(EquipmentMovement equipmentMovement)
        {
          if (_context.EquipmentMovements == null)
          {
              return Problem("Entity set 'InventoryDBContext.EquipmentMovements'  is null.");
          }
            _context.EquipmentMovements.Add(equipmentMovement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentMovement", new { id = equipmentMovement.MovementId }, equipmentMovement);
        }

        // DELETE: api/EquipmentMovements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentMovement(int id)
        {
            if (_context.EquipmentMovements == null)
            {
                return NotFound();
            }
            var equipmentMovement = await _context.EquipmentMovements.FindAsync(id);
            if (equipmentMovement == null)
            {
                return NotFound();
            }

            _context.EquipmentMovements.Remove(equipmentMovement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentMovementExists(int id)
        {
            return (_context.EquipmentMovements?.Any(e => e.MovementId == id)).GetValueOrDefault();
        }
    }
}
