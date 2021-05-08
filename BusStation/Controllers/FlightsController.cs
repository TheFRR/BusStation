using BLL;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public FlightsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<List<Flight>> GetAll()
        {
            return unitOfWork.Flight.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var flight = await unitOfWork.Flight.Get(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FlightModel flight)
        {
            int cost = 200;
            if (!ModelState.IsValid)
            {
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

            return CreatedAtAction("GetRoute", new { id = flightDB.Id }, flightDB);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FlightModel flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Flight.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            item.ArrivalTime = flight.ArrivalTime;
            item.DepartureTime = flight.DepartureTime;
            item.SeatsNumber = flight.SeatsNumber;
            unitOfWork.Flight.Update(item);
            unitOfWork.Save();
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Flight.Get((id));
            if (item == null)
            {
                return NotFound();
            }
            //await unitOfWork.Route.Delete(item.Id);
            unitOfWork.Flight.Delete(item.Id);
            unitOfWork.Save();
            return NoContent();
        }
    }
}
