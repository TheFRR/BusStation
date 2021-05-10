﻿using BLL;
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
    [Route("api/Tickets")]
    [ApiController]
    public class TicketsController : Controller
    {
        IUnitOfWork unitOfWork;
        private readonly ILogger<TicketsController> logger;
        public TicketsController(IUnitOfWork unitOfWork, ILogger<TicketsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        public Task<List<Ticket>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Tickets");
            return unitOfWork.Ticket.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            logger.LogInformation($"Вызов get запроса api/Tickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var ticket = await unitOfWork.Ticket.Get(id);
            if (ticket == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            logger.LogInformation("ОК");
            return Ok(ticket);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            logger.LogInformation("Вызов post запроса api/Tickets");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            await unitOfWork.Ticket.Add(ticket);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return CreatedAtAction("GetRoute", new { id = ticket.Id }, ticket);
        }
    

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Ticket ticket)
        {
            logger.LogInformation($"Вызов put запроса api/Tickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Ticket.Get(id);
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            item.Flight = ticket.Flight;
            item.Cost = ticket.Cost;
            unitOfWork.Ticket.Update(item);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            logger.LogInformation($"Вызов delete запроса api/Tickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Ticket.Get((id));
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            unitOfWork.Ticket.Delete(item.Id);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }
    }
}
