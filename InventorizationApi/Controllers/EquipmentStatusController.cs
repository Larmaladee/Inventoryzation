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
    public class EquipmentStatusController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public EquipmentStatusController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/EquipmentStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentStatus>>> GetEquipmentStatuses()
        {
          if (_context.EquipmentStatuses == null)
          {
              return NotFound();
          }
            return await _context.EquipmentStatuses.ToListAsync();
        }

        // GET: api/EquipmentStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentStatus>> GetEquipmentStatus(int id)
        {
          if (_context.EquipmentStatuses == null)
          {
              return NotFound();
          }
            var equipmentStatus = await _context.EquipmentStatuses.FindAsync(id);

            if (equipmentStatus == null)
            {
                return NotFound();
            }

            return equipmentStatus;
        }

        // PUT: api/EquipmentStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentStatus(int id, EquipmentStatus equipmentStatus)
        {
            if (id != equipmentStatus.EquipmentStatusId)
            {
                return BadRequest();
            }

            _context.Entry(equipmentStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentStatusExists(id))
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

        // POST: api/EquipmentStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipmentStatus>> PostEquipmentStatus(EquipmentStatus equipmentStatus)
        {
          if (_context.EquipmentStatuses == null)
          {
              return Problem("Entity set 'InventoryDBContext.EquipmentStatuses'  is null.");
          }
            _context.EquipmentStatuses.Add(equipmentStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentStatus", new { id = equipmentStatus.EquipmentStatusId }, equipmentStatus);
        }

        // DELETE: api/EquipmentStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentStatus(int id)
        {
            if (_context.EquipmentStatuses == null)
            {
                return NotFound();
            }
            var equipmentStatus = await _context.EquipmentStatuses.FindAsync(id);
            if (equipmentStatus == null)
            {
                return NotFound();
            }

            _context.EquipmentStatuses.Remove(equipmentStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentStatusExists(int id)
        {
            return (_context.EquipmentStatuses?.Any(e => e.EquipmentStatusId == id)).GetValueOrDefault();
        }
    }
}
