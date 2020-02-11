using CarsWebApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsWebApplicationXUnitTest.Repositories
{
    public interface ICarsRepository
    {
        Car GetCarById(int id);
        List<Car> GetCars();
        Car AddCar(Car car);
    }
    class FakeCarsRepository : ICarsRepository
    {
        readonly List<Car> cars = new List<Car>();
        public Car AddCar(Car car)
        {
            cars.Add(car);

            return GetCarById(car.Id);
        }

        public Car GetCarById(int id)
        {
            return GetCars().Find(x => x.Id == id);
        }

        public List<Car> GetCars()
        {
            // return new List<Car>() { new Car() { Id = 1, Brand = "Brand1", Model = "Model1" }, new Car() {Id= 2, Brand = "Brand2", Model = "Model2" } };
            return cars;
        
        }
    }
}
