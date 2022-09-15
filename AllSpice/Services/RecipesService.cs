using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _recipesRepo;
        public RecipesService(RecipesRepository recipesRepo)
        {
            _recipesRepo = recipesRepo;
        }





    }
}