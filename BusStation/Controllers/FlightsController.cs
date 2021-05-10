﻿using BLL;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsController : Controller
    {
        IUnitOfWork unitOfWork;
        private readonly ILogger<FlightsController> logger;
        public FlightsController(IUnitOfWork unitOfWork, ILogger<FlightsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        public Task<List<Flight>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Flights");
            return unitOfWork.Flight.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight([FromRoute] int id)
        {
            logger.LogInformation($"Вызов get запроса api/Flights/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var flight = await unitOfWork.Flight.Get(id);
            if (flight == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            logger.LogInformation("ОК");
            return Ok(flight);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FlightModel flight)
        {
            logger.LogInformation("Вызов post запроса api/Flights");
            int cost = 200;
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            Route route = unitOfWork.Route.GetAll().Result.Where(r => r.Number == flight.RouteNumber).ToList()[0];
            Flight flightDB = new Flight();
            flightDB.Route = route;
            flightDB.DepartureTime = flight.DepartureTime;
            flightDB.ArrivalTime = flight.ArrivalTime;
            flightDB.SeatsNumber = flight.SeatsNumber;
            flightDB.BusySeatsNumber = flight.BusySeatsNumber;
            await unitOfWork.Flight.Add(flightDB);
            unitOfWork.Save();

            Ticket ticket = new Ticket();
            ticket.Flight = flightDB;
            ticket.Cost = cost;
            await unitOfWork.Ticket.Add(ticket);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return CreatedAtAction("GetRoute", new { id = flightDB.Id }, flightDB);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FlightModel flight)
        {
            logger.LogInformation($"Вызов put запроса api/Flights/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Flight.Get(id);
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            item.ArrivalTime = flight.ArrivalTime;
            item.DepartureTime = flight.DepartureTime;
            item.BusySeatsNumber = flight.BusySeatsNumber;
            item.SeatsNumber = flight.SeatsNumber;
            unitOfWork.Flight.Update(item);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            logger.LogInformation($"Вызов delete запроса api/Flights/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Flight.Get((id));
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            unitOfWork.Flight.Delete(item.Id);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }
    }
}
