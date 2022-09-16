using System;
using System.Collections.Generic;
using AllSpice.Models;
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

        internal List<Recipe> GetAll()
        {
            return _recipesRepo.GetAll();
        }

        internal Recipe Create(Recipe newRecipe)
        {
            return _recipesRepo.Create(newRecipe);
        }

        internal Recipe GetById(int id)
        {
            Recipe recipe = _recipesRepo.GetById(id);
            if (recipe == null)
            {
                throw new Exception("no recipe by that Id");
            }
            return recipe;
        }

        internal string Delete(int id)
        {
            Recipe recipe = GetById(id);
            _recipesRepo.Delete(id);
            return $"Deleted {recipe.title}";
        }
    }
}