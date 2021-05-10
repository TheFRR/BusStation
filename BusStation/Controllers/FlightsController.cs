using BLL;
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
    /// <summary>
    /// Контроллер, отвечающий за api, связанную с данными о рейсах
    /// </summary>
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

        /// <summary>
        /// Получение списка рейсов
        /// </summary>
        /// <returns>Список рейсов</returns>
        [HttpGet]
        public Task<List<Flight>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Flights");
            return unitOfWork.Flight.GetAll();
        }

        /// <summary>
        /// Получение рейса по Id
        /// </summary>
        /// <param name="id">Id рейса</param>
        /// <returns>Рейс</returns>
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
            logger.LogInformation($"Рейс с id={id} успешно получен");
            return Ok(flight);
        }

        /// <summary>
        /// Добавление нового рейса и билета, связанного с этим рейсом
        /// </summary>
        /// <param name="flight">Модель рейса, содержащая номер маршрута вместо полноценного объекта</param>
        /// <returns>Информация о добавленном рейса</returns>
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

            try
            {
                await unitOfWork.Flight.Add(flightDB);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            Ticket ticket = new Ticket();
            ticket.Flight = flightDB;
            ticket.Cost = cost;

            try 
            { 
                await unitOfWork.Ticket.Add(ticket);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation("Рейс успешно создан");
            return CreatedAtAction("GetRoute", new { id = flightDB.Id }, flightDB);
        }

        /// <summary>
        /// Обновление данных о рейсе
        /// </summary>
        /// <param name="id">Id рейса</param>
        /// <param name="flight"></param>
        /// <returns></returns>
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

            try
            {
                unitOfWork.Flight.Update(item);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Рейс с id={id} успешно обновлён");
            return NoContent();
        }

        /// <summary>
        /// Удаление рейса
        /// </summary>
        /// <param name="id">Id рейса</param>
        /// <returns>Информация об удалённом рейса</returns>
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

            try
            {
                unitOfWork.Flight.Delete(item.Id);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Рейс с id={id} успешно удалён");
            return NoContent();
        }
    }
}
