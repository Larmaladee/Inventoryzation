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
    public class EquipmentCategoriesController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public EquipmentCategoriesController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/EquipmentCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentCategory>>> GetEquipmentCategories()
        {
          if (_context.EquipmentCategories == null)
          {
              return NotFound();
          }
            return await _context.EquipmentCategories.ToListAsync();
        }

        // GET: api/EquipmentCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentCategory>> GetEquipmentCategory(int id)
        {
          if (_context.EquipmentCategories == null)
          {
              return NotFound();
          }
            var equipmentCategory = await _context.EquipmentCategories.FindAsync(id);

            if (equipmentCategory == null)
            {
                return NotFound();
            }

            return equipmentCategory;
        }

        // PUT: api/EquipmentCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentCategory(int id, EquipmentCategory equipmentCategory)
        {
            if (id != equipmentCategory.EquipmentCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(equipmentCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentCategoryExists(id))
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

        // POST: api/EquipmentCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipmentCategory>> PostEquipmentCategory(EquipmentCategory equipmentCategory)
        {
          if (_context.EquipmentCategories == null)
          {
              return Problem("Entity set 'InventoryDBContext.EquipmentCategories'  is null.");
          }
            _context.EquipmentCategories.Add(equipmentCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipmentCategory", new { id = equipmentCategory.EquipmentCategoryId }, equipmentCategory);
        }

        // DELETE: api/EquipmentCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentCategory(int id)
        {
            var equipmentCategory = await _context.EquipmentCategories
                .FirstOrDefaultAsync(ec => ec.EquipmentCategoryId == id);

            if (equipmentCategory == null)
            {
                return NotFound();
            }

            var equipmentsToDelete = await _context.Equipment
                .Where(e => e.IdEquipmentCategory == id)
                .ToListAsync();



            foreach (var equipment in equipmentsToDelete)
            {
                var movementsToDelete = await _context.EquipmentMovements
                    .Where(m => m.EquipmentId == equipment.EquipmentId)
                    .ToListAsync();

                var applicationToDelete = await _context.Applications
                .Where(t => t.IdEquipment == equipment.EquipmentId)
                .ToListAsync();

                _context.EquipmentMovements.RemoveRange(movementsToDelete);
                _context.Applications.RemoveRange(applicationToDelete);
            }

            await _context.SaveChangesAsync(); // Сохраняем изменения в таблице EquipmentMovement

            _context.Equipment.RemoveRange(equipmentsToDelete); // Теперь можно удалить оборудование

            _context.EquipmentCategories.Remove(equipmentCategory);

            await _context.SaveChangesAsync(); // Сохраняем остальные изменения

            return NoContent();
        }


        private bool EquipmentCategoryExists(int id)
        {
            return (_context.EquipmentCategories?.Any(e => e.EquipmentCategoryId == id)).GetValueOrDefault();
        }
    }
}
