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
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;
        private readonly IngredientsService _ingredientsService;

        public RecipesController(RecipesService recipesService, IngredientsService ingredientsService)
        {
            _recipesService = recipesService;
            _ingredientsService = ingredientsService;
        }
        [HttpGet]
        public ActionResult<List<Recipe>> GetAll()
        {
            try
            {
                List<Recipe> recipes = _recipesService.GetAll();
                return Ok(recipes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newRecipe.creatorId = userInfo.Id;
                Recipe recipe = _recipesService.Create(newRecipe);
                recipe.Creator = userInfo;
                return Ok(recipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetById(int id)
        {
            try
            {
                return Ok(_recipesService.GetById(id));
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
                return Ok(_recipesService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Recipe> Update(int id, [FromBody] Recipe update)
        {
            try
            {
                update.id = id;
                return Ok(_recipesService.Update(update));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientByRecipeId(int id)
        {
            try
            {
                List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(id);
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}