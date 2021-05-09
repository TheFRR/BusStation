using BLL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Controllers
{
    [Route("api/BoughtTickets")]
    [ApiController]
    public class BoughtTicketsController : Controller
    {
        IUnitOfWork unitOfWork;
        public BoughtTicketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<List<BoughtTicket>> GetAll()
        {
            return unitOfWork.BoughtTicket.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoughtTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ticket = await unitOfWork.BoughtTicket.Get(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoughtTicket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = ticket.User;
            var _ticket = ticket.Ticket;
            var flight = ticket.Ticket.Flight;
            var route = ticket.Ticket.Flight.Route;

            var userDB = unitOfWork.User.GetAll().Result.Where(u => u.Id == user.Id).ToList()[0];
            var ticketDB = await unitOfWork.Ticket.Get(_ticket.Id);
            var flightDB = await unitOfWork.Flight.Get(flight.Id);
            var routeDB = await unitOfWork.Route.Get(route.Id);

            ticket.User = userDB;
            ticket.Ticket = ticketDB;
            ticket.Ticket.Flight = flightDB;
            ticket.Ticket.Flight.Route = routeDB;

            await unitOfWork.BoughtTicket.Add(ticket);
            unitOfWork.Save();
            return CreatedAtAction("GetRoute", new { id = ticket.Id }, ticket);
        }


        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BoughtTicket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.BoughtTicket.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            item.IsPaid = ticket.IsPaid;
            unitOfWork.BoughtTicket.Update(item);
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
            var item = await unitOfWork.BoughtTicket.Get((id));
            if (item == null)
            {
                return NotFound();
            }
            //await unitOfWork.Route.Delete(item.Id);
            unitOfWork.BoughtTicket.Delete(item.Id);
            unitOfWork.Save();
            return NoContent();
        }
    }
}
