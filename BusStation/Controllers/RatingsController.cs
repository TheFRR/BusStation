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

        [HttpGet]
        public Task<List<Rating>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Ratings");
            return unitOfWork.Rating.GetAll();
        }

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
            logger.LogInformation("ОК");
            return Ok(rating);
        }

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

            await unitOfWork.Rating.Add(rating);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return CreatedAtAction("GetRoute", new { id = rating.Id }, rating);
        }

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
            unitOfWork.Rating.Update(item);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }

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
            unitOfWork.Rating.Delete(item.Id);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }
    }
}
