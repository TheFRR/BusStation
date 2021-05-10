using BLL;
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
    /// <summary>
    /// Контроллер, отвечающий за api, связанную с билетами
    /// </summary>
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

        /// <summary>
        /// Получение списка билетов
        /// </summary>
        /// <returns>Список билетов</returns>
        [HttpGet]
        public Task<List<Ticket>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Tickets");
            return unitOfWork.Ticket.GetAll();
        }

        /// <summary>
        /// Получение билета по Id
        /// </summary>
        /// <param name="id">Id билета</param>
        /// <returns>Билет</returns>
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
            logger.LogInformation($"Билет с id={id} успешно получен");
            return Ok(ticket);
        }

        /// <summary>
        /// Добавление нового билета
        /// </summary>
        /// <param name="ticket">Данные билета</param>
        /// <returns>Информация о добавленном билете</returns>
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

            try
            {
                await unitOfWork.Ticket.Add(ticket);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation("Билет успешно добавлен");
            return CreatedAtAction("GetRoute", new { id = ticket.Id }, ticket);
        }
    
        /// <summary>
        /// Обновление билета
        /// </summary>
        /// <param name="id">Id билета</param>
        /// <param name="ticket">Данные билета</param>
        /// <returns>Информация об обновлённом билете</returns>
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

            try
            {
                unitOfWork.Ticket.Update(item);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Билет с id={id} успешно обновлён");
            return NoContent();
        }

        /// <summary>
        /// Удаление билета
        /// </summary>
        /// <param name="id">Id билета</param>
        /// <returns>Информация об удалённом билете</returns>
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

            try
            {
                unitOfWork.Ticket.Delete(item.Id);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Билет с id={id} успешно удалён");
            return NoContent();
        }
    }
}
