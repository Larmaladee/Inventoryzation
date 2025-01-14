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
    public class ApplicationStatusController : ControllerBase
    {
        private readonly InventoryDBContext _context;

        public ApplicationStatusController(InventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationStatus>>> GetApplicationStatuses()
        {
          if (_context.ApplicationStatuses == null)
          {
              return NotFound();
          }
            return await _context.ApplicationStatuses.ToListAsync();
        }

        // GET: api/ApplicationStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationStatus>> GetApplicationStatus(int id)
        {
          if (_context.ApplicationStatuses == null)
          {
              return NotFound();
          }
            var applicationStatus = await _context.ApplicationStatuses.FindAsync(id);

            if (applicationStatus == null)
            {
                return NotFound();
            }

            return applicationStatus;
        }

        // PUT: api/ApplicationStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationStatus(int id, ApplicationStatus applicationStatus)
        {
            if (id != applicationStatus.ApplicationStatusId)
            {
                return BadRequest();
            }

            _context.Entry(applicationStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationStatusExists(id))
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

        // POST: api/ApplicationStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationStatus>> PostApplicationStatus(ApplicationStatus applicationStatus)
        {
          if (_context.ApplicationStatuses == null)
          {
              return Problem("Entity set 'InventoryDBContext.ApplicationStatuses'  is null.");
          }
            _context.ApplicationStatuses.Add(applicationStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationStatus", new { id = applicationStatus.ApplicationStatusId }, applicationStatus);
        }

        // DELETE: api/ApplicationStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationStatus(int id)
        {
            if (_context.ApplicationStatuses == null)
            {
                return NotFound();
            }
            var applicationStatus = await _context.ApplicationStatuses.FindAsync(id);
            if (applicationStatus == null)
            {
                return NotFound();
            }

            _context.ApplicationStatuses.Remove(applicationStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationStatusExists(int id)
        {
            return (_context.ApplicationStatuses?.Any(e => e.ApplicationStatusId == id)).GetValueOrDefault();
        }
    }
}
