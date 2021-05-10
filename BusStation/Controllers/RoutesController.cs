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

        [HttpGet]
        public Task<List<Route>> GetAll()
        {
            logger.LogInformation("Вызов get запроса api/Routes");
            return unitOfWork.Route.GetAll();
        }

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
            logger.LogInformation("ОК");
            return Ok(route);
        }

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
            await unitOfWork.Route.Add(route);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return CreatedAtAction("GetRoute", new { id = route.Id }, route);
        }

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
            unitOfWork.Route.Update(item);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }

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
            unitOfWork.Route.Delete(item.Id);
            unitOfWork.Save();
            logger.LogInformation("ОК");
            return NoContent();
        }
    }
}

