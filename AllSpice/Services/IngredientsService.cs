using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
    public class IngredientsService
    {
        private readonly IngredientsRepository _ingredientsRepo;
        public IngredientsService(IngredientsRepository ingredientsRepo)
        {
            _ingredientsRepo = ingredientsRepo;
        }
        internal List<Ingredient> GetAll()
        {
            return _ingredientsRepo.GetAll();
        }
        internal Ingredient Create(Ingredient newIngredient)
        {
            return _ingredientsRepo.Create(newIngredient);
        }
        internal Ingredient GetById(int id)
        {
            Ingredient ingredient = _ingredientsRepo.GetById(id);
            if (ingredient == null)
            {
                throw new Exception("no ingredient by that Id");
            }
            return ingredient;
        }
        internal string Delete(int id)
        {
            Ingredient ingredient = GetById(id);
            _ingredientsRepo.Delete(id);
            return $"Deleted {ingredient.Name}";
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int id)
        {
            List<Ingredient> ingredient = _ingredientsRepo.GetIngredientByRecipeId(id);
            if (ingredient == null)
            {
                throw new Exception("no ingredient by that Id");
            }
            return ingredient;
        }
        internal Ingredient Update(Ingredient update, string userId)
        {
            Ingredient original = GetById(update.Id);
            if (original.creatorId != userId)
            {
                throw new Exception("You can not edit that ingredient");
            }
            original.Name = update.Name ?? original.Name;
            original.Quantity = update.Quantity ?? original.Quantity;
            return _ingredientsRepo.Update(original);
        }
    }
}