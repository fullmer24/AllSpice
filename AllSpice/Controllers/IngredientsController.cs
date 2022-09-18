using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;
        public IngredientsController(IngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }
        [HttpGet]
        public ActionResult<List<Ingredient>> GetAll()
        {
            try
            {
                List<Ingredient> ingredients = _ingredientsService.GetAll();
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}