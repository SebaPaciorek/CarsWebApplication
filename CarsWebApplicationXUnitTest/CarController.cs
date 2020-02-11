using CarsWebApplication.Model;
using CarsWebApplicationXUnitTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsWebApplicationXUnitTest
{
    [ApiController, Route("[controller]")]
    class CarController : ControllerBase
    {
        private readonly ICarsRepository carsRepository;
        public CarController(ICarsRepository carsRepository)
        {
            this.carsRepository = carsRepository;
        }

        [HttpGet("{id}")]
        public ActionResult GetCar(int id)
        {
            var car = this.carsRepository.GetCars().Find(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult AddCar(Car car)
        {
            this.carsRepository.AddCar(car);

            var a = this.carsRepository.GetCars();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpGet]
        public ActionResult GetCars()
        {
            var cars = this.carsRepository.GetCars();

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }
    }
}
