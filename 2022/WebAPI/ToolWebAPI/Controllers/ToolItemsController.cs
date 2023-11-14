using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolWebAPI.Models;

namespace ToolWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolItemsController : ControllerBase
    {
        private readonly ToolContext _context;

        public ToolItemsController(ToolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolItem>>> GetToolItems()
        {
            return await _context.ToolItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToolItem>> GetToolItem(int id)
        {
            var toolItem = await _context.ToolItems.FindAsync(id);

            if (toolItem == null)
            {
                return NotFound();
            }

            return toolItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutToolItem(int id, ToolItem toolItem)
        {
            if (id != toolItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(toolItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolItemExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ToolItem>> PostToolItem(ToolItem toolItem)
        {
            _context.ToolItems.Add(toolItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetToolItem", new { id = toolItem.Id }, toolItem);
            return CreatedAtAction(nameof(GetToolItem), new { id = toolItem.Id }, toolItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToolItem(int id)
        {
            var toolItem = await _context.ToolItems.FindAsync(id);
            if (toolItem == null)
            {
                return NotFound();
            }

            _context.ToolItems.Remove(toolItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToolItemExists(int id)
        {
            return _context.ToolItems.Any(e => e.Id == id);
        }
    }
}
