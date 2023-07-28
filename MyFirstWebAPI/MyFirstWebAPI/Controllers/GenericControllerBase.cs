using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Domain;
using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Controllers
{
    /// <summary>
    /// Reference: https://dev.to/guivern/build-a-generic-crud-api-with-asp-net-core-adf
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ApiController]
    [Route("api/[controller]")]
    public class GenericControllerBase<T> : ControllerBase where T : EntityBase
    {
        protected readonly MyFirstWebAPIDbContext _context;

        public GenericControllerBase(MyFirstWebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var entities = await _context.Set<T>().ToListAsync();

            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Detail(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {
            entity.DateCreated = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Detail", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, T entity)
        {
            if (id != entity.Id)
                return BadRequest();

            if (!await _context.Set<T>().AnyAsync(e => e.Id == id))
                return NotFound();

            entity.DateCreated = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return NotFound();

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
