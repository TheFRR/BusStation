using BLL;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        IUnitOfWork unitOfWork;

        public RoutesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<List<Route>> GetAll()
        {
            return unitOfWork.Route.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var route = await unitOfWork.Route.Get(id);
            if (route == null)
            {
                return NotFound();
            }
            return Ok(route);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await unitOfWork.Route.Add(route);
            unitOfWork.Save();
            return CreatedAtAction("GetRoute", new { id = route.Id },
            route);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = unitOfWork.Route.Get(id).Result;
            if (item == null)
            {
                return NotFound();
            }
            await unitOfWork.Route.Update(item);
            unitOfWork.Save();
            return NoContent();
        }

        ///Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await unitOfWork.Route.Get((id));
            if (item == null)
            {
                return NotFound();
            }
            //await unitOfWork.Route.Delete(item.Id);
            unitOfWork.Route.Delete(item.Id);
            unitOfWork.Save();
            return NoContent();
        }
    }
}

