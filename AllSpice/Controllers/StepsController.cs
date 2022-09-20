using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Services;
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



    }
}