using AllSpice.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;
        public RecipesController(RecipesService recipesService)
        {
            _recipesService = recipesService;
        }




    }
}