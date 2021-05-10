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
    /// Контроллер, отвечающий за api, связанную с общей корзиной, содержащей данные о покупках и оплате всех юзеров
    /// </summary>
    [Route("api/BoughtTickets")]
    [ApiController]
    public class BoughtTicketsController : Controller
    {
        IUnitOfWork unitOfWork;
        private readonly ILogger<BoughtTicketsController> logger;

        public BoughtTicketsController(IUnitOfWork unitOfWork, ILogger<BoughtTicketsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Получение списка добавленных в корзину билетов
        /// </summary>
        /// <returns>Список билетов</returns>
        [HttpGet]
        public Task<List<BoughtTicket>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/BoughtTickets");
            return unitOfWork.BoughtTicket.GetAll();
        }

        /// <summary>
        /// Получение билета по id
        /// </summary>
        /// <param name="id">Id билета</param>
        /// <returns>Билет</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoughtTicket([FromRoute] int id)
        {
            logger.LogInformation($"Вызов get запроса api/BoughtTickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var ticket = await unitOfWork.BoughtTicket.Get(id);
            if (ticket == null)
            {
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
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoughtTicket ticket)
        {
            logger.LogInformation("Вызов post запроса api/BoughtTickets");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
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

            try
            {
                await unitOfWork.BoughtTicket.Add(ticket);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation("Билет успешно добавлен в корзину");
            return CreatedAtAction("GetRoute", new { id = ticket.Id }, ticket);
        }

        /// <summary>
        /// Обновление данных об оплате билета
        /// </summary>
        /// <param name="id">Id билета</param>
        /// <param name="ticket">Данные билета</param>
        /// <returns>Информация о обновлённом билете</returns>
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BoughtTicket ticket)
        {
            logger.LogInformation($"Вызов put запроса api/BoughtTickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.BoughtTicket.Get(id);
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            item.IsPaid = ticket.IsPaid;

            try
            {
                unitOfWork.BoughtTicket.Update(item);
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
            logger.LogInformation($"Вызов delete запроса api/BoughtTickets/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.BoughtTicket.Get((id));
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }

            try
            {
                unitOfWork.BoughtTicket.Delete(item.Id);
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
