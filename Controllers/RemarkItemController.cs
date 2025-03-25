using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemarkItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RemarkItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemarkItem>>> GetRemarkItems()
        {
            return await _context.RemarkItems
                .Include(r => r.Machine)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RemarkItem>> GetRemarkItem(int id)
        {
            var remarkItem = await _context.RemarkItems
                .Include(r => r.Machine)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (remarkItem == null)
            {
                return NotFound();
            }

            return remarkItem;
        }
    }
}