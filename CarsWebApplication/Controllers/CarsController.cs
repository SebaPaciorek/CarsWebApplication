using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsWebApplication.Database;
using CarsWebApplication.Model;
using EasyCaching.Core;

namespace CarsWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarContext _context;
        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;

        public CarsController(CarContext context, IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _context = context;
            _easyCachingProviderFactory = easyCachingProviderFactory;
        }

        // GET: api/Cars
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        //{
        //    return await _context.Cars.ToListAsync();
        //}

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var cache = _easyCachingProviderFactory.GetCachingProvider("default");

            var car = await cache.GetAsync($"car{id}", async () => await _context.Cars.FindAsync(id), TimeSpan.FromSeconds(60));

            //var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsPaging([FromQuery] CarPagingParameters carPagingParameters)
        {
            var cache = _easyCachingProviderFactory.GetCachingProvider("default");

            var cars = await cache.GetAsync(carPagingParameters.PageIndex.ToString(), async () => await PaginatedList<Car>.CreateAsync(_context.Cars.AsNoTracking(), carPagingParameters.PageIndex, carPagingParameters.PageSize), TimeSpan.FromSeconds(60));

            return Ok(cars);
            
          //  return await PaginatedList<Car>.CreateAsync(_context.Cars.AsNoTracking(), carPagingParameters.PageIndex, carPagingParameters.PageSize);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
