﻿using System;
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
    public class EquipmentsController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public EquipmentsController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Equipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
          if (_context.Equipment == null)
          {
              return NotFound();
          }
            return await _context.Equipment.ToListAsync();
        }

        // GET: api/Equipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
          if (_context.Equipment == null)
          {
              return NotFound();
          }
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        // PUT: api/Equipments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
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

        // POST: api/Equipments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
        {
          if (_context.Equipment == null)
          {
              return Problem("Entity set 'InventoryDBContext.Equipment'  is null.");
          }
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipment", new { id = equipment.EquipmentId }, equipment);
        }

        // DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(ec => ec.EquipmentId == id);

            if (equipment == null)
            {
                return NotFound();
            }

            var movementsToDelete = await _context.EquipmentMovements
                    .Where(m => m.EquipmentId == id)
                    .ToListAsync();

            var applicationToDelete = await _context.Applications
            .Where(t => t.IdEquipment == id)
            .ToListAsync();

            _context.EquipmentMovements.RemoveRange(movementsToDelete);
            _context.Applications.RemoveRange(applicationToDelete);

            await _context.SaveChangesAsync();

            _context.Equipment.Remove(equipment);

            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool EquipmentExists(int id)
        {
            return (_context.Equipment?.Any(e => e.EquipmentId == id)).GetValueOrDefault();
        }
    }
}
