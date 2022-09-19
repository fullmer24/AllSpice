using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;
        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Ingredient> GetAll()
        {
            string sql = @"
            SELECT
           i.*,
           a.*
           FROM ingredients i
           JOIN accounts a ON a.id = i.creatorId;
            ";
            List<Ingredient> ingredients = (List<Ingredient>)_db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
            {
                ingredient.Creator = account;
                return ingredient;
            }).ToList();
            return ingredients;
        }
        internal Ingredient GetById(int id)
        {
            string sql = @"
            SELECT * FROM ingredients
            WHERE id = @id;
            ";
            Ingredient ingredient = _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
            return ingredient;
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM ingredients WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }

        internal List<Ingredient> GetIngredientByRecipeId(int id)
        {
            string sql = @"
            SELECT 
            i.*,
            r.id
            
            FROM ingredients i
            JOIN recipes r ON r.id = i.recipeId
            WHERE i.recipeId = @id;
            ";
            List<Ingredient> ingredient = _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
            {
                ingredient.RecipeId = recipe.id;
                return ingredient;
            }, new { id }).ToList();
            return ingredient;
        }

        internal Ingredient Create(Ingredient newIngredient)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId, CreatorId)
            VALUES
            (@name, @quantity, @recipeId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newIngredient);
            newIngredient.Id = id;
            return newIngredient;
        }




    }
}