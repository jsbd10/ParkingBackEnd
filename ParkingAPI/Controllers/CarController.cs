using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.models;
using ParkingAPI.Services;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public CarServices _CarServices;

        public CarController(CarServices carServices)
        {
            _CarServices = carServices;
        }
        
        [Route("getCars")]
        [HttpGet]
        public ActionResult<List<car>> Get()
        {
            return _CarServices.Get();
        }

        [Route("createCar")]
        [HttpPost]
        public ActionResult<car> Create(car cars)
        {
            _CarServices.Create(cars);
            return Ok(cars);
        }

        [Route("price")]
        [HttpPost]
        public ActionResult getPrice(car cars)
        {
            var car = _CarServices.getPrice(cars.placa);
            return Ok(car);
        } 
        
        [HttpPut]
        public ActionResult Update(car cars)
        {
            _CarServices.Update(cars.id, cars);
            return Ok();
        }
    }
}