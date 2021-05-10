using BLL;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за api, связанную с маршрутами
    /// </summary>
    [Route("api/Routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        IUnitOfWork unitOfWork;
        private readonly ILogger<RoutesController> logger;

        public RoutesController(IUnitOfWork unitOfWork, ILogger<RoutesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Получение списка маршрутов
        /// </summary>
        /// <returns>Список маршрутов</returns>
        [HttpGet]
        public Task<List<Route>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Routes");
            return unitOfWork.Route.GetAll();
        }

        /// <summary>
        /// Получение маршрута по Id
        /// </summary>
        /// <param name="id">Id маршрута</param>
        /// <returns>Маршрут</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
        {
            logger.LogInformation($"Вызов get запроса api/Routes/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var route = await unitOfWork.Route.Get(id);
            if (route == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            logger.LogInformation($"Маршрут с id={id} успешно получен");
            return Ok(route);
        }

        /// <summary>
        /// Создание нового маршрута
        /// </summary>
        /// <param name="route">Данные маршрута</param>
        /// <returns>Информация о добавленном маршруте</returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Route route)
        {
            logger.LogInformation("Вызов post запроса api/Routes");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }

            try
            {
                await unitOfWork.Route.Add(route);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation("Маршрут успешно добавлен");
            return CreatedAtAction("GetRoute", new { id = route.Id }, route);
        }

        /// <summary>
        /// Обновление маршрута
        /// </summary>
        /// <param name="id">Id маршрута</param>
        /// <param name="route">Данные маршрута</param>
        /// <returns>Информация об обновлённом маршруте</returns>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Route route)
        {
            logger.LogInformation($"Вызов put запроса api/Routes/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Route.Get(id);
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            item.Number = route.Number;
            item.Arrival = route.Arrival;
            item.Departure = route.Departure;

            try
            {
                unitOfWork.Route.Update(item);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Маршрут с id={id} успешно обновлён");
            return NoContent();
        }

        /// <summary>
        /// Удаление маршрута
        /// </summary>
        /// <param name="id">Id маршрута</param>
        /// <returns>Данные об удалённом маршруте</returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            logger.LogInformation($"Вызов delete запроса api/Routes/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Route.Get((id));
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }

            try
            {
                unitOfWork.Route.Delete(item.Id);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Маршрут с id={id} успешно удалён");
            return NoContent();
        }
    }
}

