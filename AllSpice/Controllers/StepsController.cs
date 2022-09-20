using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StepsController : ControllerBase
    {
        private readonly StepsService
        _stepsService;
        public StepsController(StepsService stepsService)
        {
            _stepsService = stepsService;
        }
        [HttpGet]
        public ActionResult<List<StepsController>> GetAll()
        {
            try
            {
                List<Steps> steps = _stepsService.GetAll();
                return Ok(steps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Steps>> Create([FromBody] Steps newStep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newStep.CreatorId = userInfo.Id;
                Steps steps = _stepsService.Create(newStep);
                steps.Creator = userInfo;
                return Ok(steps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Steps> GetById(int id)
        {
            try
            {
                return Ok(_stepsService.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_stepsService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Steps>> Update(int id, [FromBody] Steps update)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                update.Id = id;
                Steps steps = _stepsService.Update(update, user.Id);
                return Ok(steps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}