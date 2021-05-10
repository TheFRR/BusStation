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
    /// Контроллер, отвечающий за api, связанную с оценками рейсов
    /// </summary>
    [Route("api/Ratings")]
    [ApiController]
    public class RatingsController : Controller
    {
        IUnitOfWork unitOfWork;
        private readonly ILogger<RatingsController> logger;

        public RatingsController(IUnitOfWork unitOfWork, ILogger<RatingsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <returns>Список оценок</returns>
        [HttpGet]
        public Task<List<Rating>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Ratings");
            return unitOfWork.Rating.GetAll();
        }

        /// <summary>
        /// Получение оценки рейса по Id
        /// </summary>
        /// <param name="id">Id оценки</param>
        /// <returns>Оценки</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
        {
            logger.LogInformation($"Вызов get запроса api/Ratings/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var rating = await unitOfWork.Rating.Get(id);
            if (rating == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            logger.LogInformation($"Оценка с id={id} успешно получена");
            return Ok(rating);
        }

        /// <summary>
        /// Добавление новой оценки
        /// </summary>
        /// <param name="rating">Данные оценки</param>
        /// <returns>Информация о добавленной оценке</returns>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rating rating)
        {
            logger.LogInformation("Вызов post запроса api/Ratings");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }

            var user = rating.User;
            var userDB = unitOfWork.User.GetAll().Result.Where(u => u.Id == user.Id).ToList()[0];

            var route = rating.Route;
            var routeDB = await unitOfWork.Route.Get(route.Id);

            rating.Route = routeDB;
            rating.User = userDB;

            try
            {
                await unitOfWork.Rating.Add(rating);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation("Оценка успешно добавлена");
            return CreatedAtAction("GetRoute", new { id = rating.Id }, rating);
        }

        /// <summary>
        /// Обновление оценки по Id
        /// </summary>
        /// <param name="id">Id оценки</param>
        /// <param name="rating">Данные оценки</param>
        /// <returns>Информация об обновлённой оценке</returns>
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Rating rating)
        {
            logger.LogInformation($"Вызов put запроса api/Ratings/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Rating.Get(id);
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }
            item.Mark = rating.Mark;

            try
            {
                unitOfWork.Rating.Update(item);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Оценка с id={id} успешно обновлена");
            return NoContent();
        }

        /// <summary>
        /// Удаление оценки
        /// </summary>
        /// <param name="id">Id оценки</param>
        /// <returns>Информация об удалённой оценке</returns>
        [Authorize(Roles = "user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            logger.LogInformation($"Вызов delete запроса api/Ratings/{id}");
            if (!ModelState.IsValid)
            {
                logger.LogError($"BadRequest: {ModelState}");
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Rating.Get((id));
            if (item == null)
            {
                logger.LogError("NotFound");
                return NotFound();
            }

            try
            {
                unitOfWork.Rating.Delete(item.Id);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
            }

            logger.LogInformation($"Оценка с id={id} успешно удалена");
            return NoContent();
        }
    }
}
