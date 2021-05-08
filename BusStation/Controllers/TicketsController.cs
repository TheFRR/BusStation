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
    [Route("api/Tickets")]
    [ApiController]
    public class TicketsController : Controller
    {
        IUnitOfWork unitOfWork;
        public TicketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<List<Ticket>> GetAll()
        {
            return unitOfWork.Ticket.GetAll();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ticket = await unitOfWork.Ticket.Get(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await unitOfWork.Ticket.Add(ticket);
            unitOfWork.Save();
            return CreatedAtAction("GetRoute", new { id = ticket.Id }, ticket);
        }
    

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Ticket.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Flight = ticket.Flight;
            item.Cost = ticket.Cost;
            unitOfWork.Ticket.Update(item);
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
            var item = await unitOfWork.Ticket.Get((id));
            if (item == null)
            {
                return NotFound();
            }
            //await unitOfWork.Route.Delete(item.Id);
            unitOfWork.Ticket.Delete(item.Id);
            unitOfWork.Save();
            return NoContent();
        }
    }
}
