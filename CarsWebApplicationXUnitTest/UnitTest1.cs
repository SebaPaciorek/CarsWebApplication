using CarsWebApplication.Controllers;
using CarsWebApplicationXUnitTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using FluentAssertions;
using Xunit;
using CarsWebApplication.Model;
using System.Collections.Generic;

namespace CarsWebApplicationXUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetCarById_ShouldReturnStatusOK()
        {
            var mock = new Mock<ICarsRepository>();
            mock.Setup(c => c.GetCars()).Returns(new List<Car>() { new Car() { Id = 1, Brand = "Brand1", Model = "Model1" }, new Car() { Id = 2, Brand = "Brand2", Model = "Model2" } });

            var carController = new CarController(mock.Object);

            var result = carController.GetCar(1);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCarById_ShouldReturnCorrectedItem()
        {
            var mock = new Mock<ICarsRepository>();
            mock.Setup(c => c.GetCars()).Returns(new List<Car>() { new Car() { Id = 1, Brand = "Brand1", Model = "Model1" }, new Car() { Id = 2, Brand = "Brand2", Model = "Model2" } });

            var carController = new CarController(mock.Object);

            var result = carController.GetCar(1);

            var car =  (Car)((OkObjectResult)result).Value;
            
            Assert.Equal("Brand1", car.Brand);
        }

        [Fact]
        public void GetAllCars_ShouldReturnAllCars()
        {
            var mock = new Mock<ICarsRepository>();
            //mock.Setup(c => c.GetCars()).Returns(new List<Car>() { new Car() { Id = 1, Brand = "Brand1", Model = "Model1" }, new Car() { Id = 2, Brand = "Brand2", Model = "Model2" } });

            mock.Setup(c => c.AddCar(new Car() { Id = 1, Brand = "Brand1", Model = "Model1" })).Returns(new Car() { Id = 1, Brand = "Brand1", Model = "Model1" });
            
            var carController = new CarController(mock.Object);

            var car = carController.AddCar(new Car() { Id = 1, Brand = "Brand1", Model = "Model1" });
            var car2 =carController.AddCar(new Car() { Id = 2, Brand = "Brand2", Model = "Model2" });
            var car3 = carController.AddCar(new Car() { Id = 3, Brand = "Brand3", Model = "Model3" });
            var car4 = carController.AddCar(new Car() { Id = 4, Brand = "Brand4", Model = "Model4" });


            var result = carController.GetCars();

            var cars = (List<Car>) ((OkObjectResult)result).Value;

            Assert.Equal(4, cars.Count);
        }
    }
}
