using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class FavoritesService
    {
        private readonly FavoritesRepository _favoritesRepo;
        private readonly RecipesService _recipesService;
        public FavoritesService(FavoritesRepository favoritesRepo, RecipesService recipesService)
        {
            _favoritesRepo = favoritesRepo;
            _recipesService = recipesService;
        }
        internal List<FavoritesVM> GetFavorites(string accountId)
        {
            // _recipesService.GetById(recipeId);
            return _favoritesRepo.GetFavorites(accountId);
        }



    }
}